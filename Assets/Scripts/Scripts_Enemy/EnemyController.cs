using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimation enemyAnimation;
    private NavMeshAgent navAgent;
    private EnemyState enemyState;
    private Transform target; //the player

    public GameObject attackPoint;

    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;
    public float chaseDistance = 8f; //if player's distance is less than distanceChase than start chasing the player
    public float attackDistance = 2.2f;
    public float chaseDistanceAfterAttack = 2f; //put some distance between player and enemy before chasing the player(if player is running away)
    public float patrolRadiusMin = 20f, patrolRadiusMax = 60f;
    public float patrolForTime = 15f; //how long will enemy patrol before setting a new destination
    public float waitBeforeAttack = 2f;
    
    private float currentChaseDistance;
    private float patrolTimer;
    private float attackTimer;


    void Awake() {
        enemyAnimation = GetComponent<EnemyAnimation>();
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.PATROL;
        patrolTimer = patrolForTime;
        attackTimer = waitBeforeAttack; //attack immediately when the enemy gets to the player
        currentChaseDistance = chaseDistance; //memorize the value of chaseDistance
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyState == EnemyState.PATROL) {
            patrol();
        }

        if(enemyState == EnemyState.CHASE) {
            chase();
        }

        if(enemyState == EnemyState.ATTACK) {
            attack();
        }
    }

    void patrol() {
        //tell navAgent that he can move
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;

        //add to the patrolTimer
        patrolTimer += Time.deltaTime;
        if(patrolTimer > patrolForTime) {
            setRandomDestination();
            patrolTimer = 0f;
        }

        if(navAgent.velocity.sqrMagnitude > 0) {
            enemyAnimation.walk(true);
        } else {
            enemyAnimation.walk(false);
        }

        //test distance between player and enemy
        if(Vector3.Distance(transform.position, target.position) <= chaseDistance) {
            enemyAnimation.walk(false);
            enemyState = EnemyState.CHASE;

            /*play audio*/
        }
    }

    void chase() {
        //enable agent to move again
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;

        //set player's position as the destination(we are chasing the player)
        navAgent.SetDestination(target.position);

        if(navAgent.velocity.sqrMagnitude > 0) {
            enemyAnimation.run(true);
        } else {
            enemyAnimation.run(false);
        }

        //if distance between player and enemy is less than attackDistance
        if(Vector3.Distance(transform.position, target.position) <= attackDistance) {
            //stop the animations
            enemyAnimation.run(false);
            enemyAnimation.walk(false);
            enemyState = EnemyState.ATTACK;

            //reset chaseDistance to previous value
            if(chaseDistance != currentChaseDistance) {
                chaseDistance = currentChaseDistance;
            }

        //player ran away from enemy
        } else if(Vector3.Distance(transform.position, target.position) > chaseDistance) {
            //stop running
            enemyAnimation.run(false);

            //change state to patrol
            enemyState = EnemyState.PATROL;

            //reset patrolTimer to get new patrol destination immediately
            patrolTimer = patrolForTime;

            //reset chaseDistance to previous value
            if(chaseDistance != currentChaseDistance) {
                chaseDistance = currentChaseDistance;
            }
        }
    }

    void attack() {
        //stop enemy
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        
        attackTimer += Time.deltaTime;

        if(attackTimer > waitBeforeAttack) {
            enemyAnimation.attack();
            attackTimer = 0f;

            /*play audio*/
        }

        if(Vector3.Distance(transform.position, target.position) > attackDistance + chaseDistanceAfterAttack) {
            enemyState = EnemyState.CHASE;
        }
    }

    void setRandomDestination() {
        float randomRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);

        //generate new random position
        Vector3 randomDirection = Random.insideUnitSphere * randomRadius;
        randomDirection += transform.position;

        //calculate a position that is within the play-map (stores that position into navHit)
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, randomRadius, -1);

        //set the new position
        navAgent.SetDestination(navHit.position);
    }

    //copied from WeaponHandler script for the animation
    void turnOnAttackPoint() {
        attackPoint.SetActive(true);
    }

    void turnOffAttackPoint() {
        if(attackPoint.activeInHierarchy) {
            attackPoint.SetActive(false);
        }
    }
}
