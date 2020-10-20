using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommandPattern; //namespace from Doug's code


public class EnemyBehaviour : MonoBehaviour
{
    //Test github
    private EnemyState enemyState = EnemyState.patrol;
    private GameObject player;

    public Transform enemy;
    public Rigidbody2D enemyRB;

    public enum EnemyState
    {
        patrol,
        chase,
        rest,
        die  //Do we need this state?
    }

    [SerializeField] private float speed = 100;

    [SerializeField] private float maxPatrolSlot = 3;
    [SerializeField] private float patrolSlot;
    [SerializeField] private float maxChaseSlot = 3;
    private float chaseSlot;

    [SerializeField] private float chaseCooldown = 3;
    [SerializeField] private float patrolCooldown = 6;
    [SerializeField] private float restBuffer = 3;
    [SerializeField] private float timeBetweenCommand = 0.1f;

    private bool canChase = true;


    #region NPC Command
    //List of command from command from static NPCBehaviorCommand
    private Command moveLeft, moveRight, idle, jump, moveToLocation;    
    
    private List<Command> patrolingBehaviorCommands = new List<Command>();
    //private List<Command> chaseBehaviorCommands = new List<Command>();

    #endregion


    public static event Action<EnemyBehaviour> OnDie; //Send message to game manager, might be sent from player

    void Start() //or Awake()? or OnEnable()?
    {
        player = GameObject.FindWithTag("Player");
        enemyRB = GetComponent<Rigidbody2D>();
        moveLeft = new moveLeft();
        moveRight = new moveRight();
        idle = new idle();
        jump = new jump();
        moveToLocation = new moveToLocation();
        //add behavior to command list            
        patrolingBehaviorCommands.Add(moveLeft);
        patrolingBehaviorCommands.Add(moveLeft);
        patrolingBehaviorCommands.Add(moveLeft);
        patrolingBehaviorCommands.Add(moveLeft);
        patrolingBehaviorCommands.Add(moveLeft);        
        patrolingBehaviorCommands.Add(moveRight);
        patrolingBehaviorCommands.Add(moveRight);
        patrolingBehaviorCommands.Add(moveRight);
        patrolingBehaviorCommands.Add(moveRight);
        patrolingBehaviorCommands.Add(moveRight);
        
        //chaseBehaviorCommands.Add(moveToLocation);

        patrolSlot = maxPatrolSlot;
        chaseSlot = maxChaseSlot;
        Invoke ("EnemyPatrol", UnityEngine.Random.Range(0.2f, 2f)); //Cheat to make the goombas behave not in unison
    }

    //PATROL BEHAVIOUR
    private void EnemyPatrol() //Go to rest state if patrolSlot <= 0
    {

        if (patrolSlot > 0 && enemyState != EnemyState.chase)
        {
            StartCoroutine("EnemyPatroling");
        }

        if(patrolSlot == 0 && enemyState != EnemyState.chase) //EXIT from patrol to rest
        {
            enemyState = EnemyState.rest;
            DoRest();
        }

        //refresh patrolSlot before start a new patrol from other state -> done at rest!
    }

    IEnumerator EnemyPatroling()
    {
        patrolSlot -= 1;
        Debug.Log("I'm patrolling, patrol slot remaining is " + patrolSlot);

        #region Patrolling Command Lists
        //execute patrolling command list(s) here
        int i = 0;

        while ( i <= patrolingBehaviorCommands.Count - 1 && enemyState == EnemyState.patrol)
        {
            patrolingBehaviorCommands[i].Execute(enemy, patrolingBehaviorCommands[i]);
            yield return new WaitForSeconds(timeBetweenCommand);
            i++;
        }

        yield return new WaitUntil(() => i >= patrolingBehaviorCommands.Count - 1);
        
        #endregion

        //Startover
        StopCoroutine("EnemyPatroling");
        EnemyPatrol();

    }

    //Chase condition
    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "Player" && canChase == true)
        {
            Debug.Log("Mario detected!");
            enemyState = EnemyState.chase;
            DoChase();
            StopCoroutine("EnemyPatroling");
        }
    }

    //CHASE BEHAVIOUR//
    private void DoChase()
    {
        //Check if the enemy is tired? (add variable)
        
        while (enemyState == EnemyState.chase)
        {
            if (chaseSlot > 0)
            {
                Debug.Log("Start chase");
                StartCoroutine(ChasingPlayer());
            }

            if (chaseSlot <= 0)
            {
                Debug.Log("Exit from chase");
                if(patrolSlot > 0)
                {
                    enemyState = EnemyState.patrol;
                    EnemyPatrol();
                    chaseSlot = maxChaseSlot;
                }

                if(patrolSlot <= 0)
                {
                    enemyState = EnemyState.rest;
                    DoRest();
                    chaseSlot = maxChaseSlot;
                }
            }
        }
               
    }

    IEnumerator ChasingPlayer()
    {        
        chaseSlot -= 1;

        #region Chase Command Lists
        //execute chasing command list

        Vector2 destination = new Vector2(player.transform.position.x, player.transform.position.y);
        
        Vector3 enemyDes = destination;
        moveToLocation = new moveToLocation();
        moveToLocation.Execute(enemy, destination, speed, moveLeft);

        Debug.Log("Moving from " + enemy.position + "to " + enemyDes + " with speed " + speed + " chase slot = " + chaseSlot);
        yield return new WaitUntil(() => enemy.position == enemyDes);

        //yield return from the last action on the list
        #endregion

        //yield return new WaitForSeconds(3f); //temp

        StopCoroutine(ChasingPlayer()); //Not sure if we need to stop coroutine here

        DoChase(); //Start over the chase 
    }


    //REST STATE//
    private void DoRest()
    {
        StartCoroutine("EnemyResting");
        Debug.Log("Start rest");
        jump.Execute(enemy, enemyRB, jump);
    }

    IEnumerator EnemyResting()
    {
        canChase = false;
        yield return new WaitForSeconds(chaseCooldown); //During this time, can't enter chase state
        canChase = true;
        yield return new WaitForSeconds(restBuffer);

        patrolSlot = maxPatrolSlot; //refresh patrol slot here, so that the enemy can move to patrol state only from here

        if(enemyState != EnemyState.chase) //EXIT from patrol to rest
        {
            enemyState = EnemyState.patrol;
            Debug.Log("Back to patrol");
            StopCoroutine("EnemyResting");
            EnemyPatrol();
        }

    }

    public void Dies()
    {
        //Die function
        enemyState = EnemyState.die;

        if(OnDie != null)
        {
            OnDie(this);
        }
    }
    

}
