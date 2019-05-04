using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] float patrolSpeed = 1.0f;
    [SerializeField] float patrolWaitTime = 1.0f;
    [SerializeField] float chaseSpeed = 1.5f;
    [SerializeField] float chaseWaitTime = 5.0f;
    [SerializeField] float stoppingDistanceFromPlayers = 7.0f;
    [SerializeField] float maxDistanceFromPlayer = 20.0f;
    [SerializeField] Transform[] patrolWayPoints;
    EnemyCount enemyCount;

    private EnemySenses enemySenses;
    public NavMeshAgent navMeshAgent;
    private Transform playerOne;
    private Transform playerTwo;
    private PlayersLastLocation playersLastLocation;

    private float patrolTimer;
    public float chaseTimer;
    private int currentWaypointIndex;
    private bool isInRangeOfPlayer = false;

    private void Awake()
    {
        enemySenses = GetComponent<EnemySenses>();
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    private void Start()
    {
        enemyCount = EnemyCount.instance;
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne").transform;
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").transform;
        playersLastLocation = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayersLastLocation>();
    }

    private void Update()
    {
        if (enemySenses.isPlayerOneInSight || enemySenses.isPlayerTwoInSight)
        {
            AnalysePlayer();
        }
        else if (enemySenses.playerOneLastKnownLocation != playersLastLocation.resetPosition || enemySenses.playerTwoLastKnownLocation != playersLastLocation.resetPosition)
        {
            ChasePlayer();
        }
        else
        {
            PatrolWaypoints();
        }
    }

    void AnalysePlayer()
    {
        // EnemyAnalyse script should trigger - if the player runs out of range, the enemy should run towards their last known location before going back to patrolling

    }

    void ChasePlayer()
    {
        for (int i = 0; i < enemyCount.numberOfEnemies.Length; i++)
        {
            var enemyAIComp = this.enemyCount.numberOfEnemies[i].GetComponent<EnemyAI>();
            Vector3 distanceToPlayerOne = enemySenses.playerOneLastKnownLocation - this.enemyCount.numberOfEnemies[i].transform.position;
            Vector3 distanceToPlayerTwo = enemySenses.playerTwoLastKnownLocation - this.enemyCount.numberOfEnemies[i].transform.position;

            // This *SHOULD* check to see what player is closest to each drone, if player one is closer than this IF statement should run. Keyword *should* 
            if (Vector3.Distance(this.enemyCount.numberOfEnemies[i].transform.position, distanceToPlayerOne) < Vector3.Distance(this.enemyCount.numberOfEnemies[i].transform.position, distanceToPlayerTwo))
            {
                // If playerone is closer, check to see if they're in range of the enemy
                if (distanceToPlayerOne.magnitude < maxDistanceFromPlayer)
                {
                    //Rotate drones to look at playerone's last known location
                    var tarPos = Quaternion.LookRotation(enemySenses.playerOneLastKnownLocation - enemyCount.numberOfEnemies[i].transform.position);
                    enemyCount.numberOfEnemies[i].transform.rotation = Quaternion.RotateTowards(enemyCount.numberOfEnemies[i].transform.rotation, tarPos, 100.0f * Time.deltaTime);

                    //TODO: tinker with the chaseSpeed - perhaps slow the speed when the enemy is analysing the player to allow the player to escape in time - THIS WORKS
                    enemyCount.numberOfEnemies[i].GetComponent<EnemyAI>().navMeshAgent.speed = enemyAIComp.chaseSpeed;

                    //Move drones to playerone's last known location
                    var targetPosition = enemyAIComp.navMeshAgent.destination = enemySenses.playerOneLastKnownLocation;
                    enemyAIComp.navMeshAgent.stoppingDistance = stoppingDistanceFromPlayers;
                }
                else
                {
                    enemyAIComp.chaseTimer = 0.0f;
                }

                if (enemyAIComp.navMeshAgent.remainingDistance < enemyAIComp.navMeshAgent.stoppingDistance)
                {
                    enemyAIComp.chaseTimer += Time.deltaTime;

                    if (enemyAIComp.chaseTimer > enemyAIComp.chaseWaitTime)
                    {
                        playersLastLocation.playerOnePosition = playersLastLocation.resetPosition;
                        playersLastLocation.playerTwoPosition = playersLastLocation.resetPosition;
                        enemySenses.playerOneLastKnownLocation = playersLastLocation.resetPosition;
                        enemySenses.playerTwoLastKnownLocation = playersLastLocation.resetPosition;
                        enemyAIComp.chaseTimer = 0.0f;
                    }
                }
                else
                {
                    enemyAIComp.chaseTimer = 0.0f;
                }
            }
            // same as above but for player two
            else if (Vector3.Distance(this.enemyCount.numberOfEnemies[i].transform.position, distanceToPlayerTwo) < Vector3.Distance(this.enemyCount.numberOfEnemies[i].transform.position, distanceToPlayerOne))
            {
                if (distanceToPlayerTwo.magnitude < maxDistanceFromPlayer)
                {
                    //Rotate drones to look at player - THIS WORKS
                    var tarPos = Quaternion.LookRotation(enemySenses.playerTwoLastKnownLocation - enemyCount.numberOfEnemies[i].transform.position);
                    enemyCount.numberOfEnemies[i].transform.rotation = Quaternion.RotateTowards(enemyCount.numberOfEnemies[i].transform.rotation, tarPos, 100.0f * Time.deltaTime);

                    //TODO: tinker with the chaseSpeed - perhaps slow the speed when the enemy is analysing the player to allow the player to escape in time - THIS WORKS
                    enemyCount.numberOfEnemies[i].GetComponent<EnemyAI>().navMeshAgent.speed = enemyAIComp.chaseSpeed;

                    //Move drones to players location
                    var targetPosition = enemyAIComp.navMeshAgent.destination = enemySenses.playerTwoLastKnownLocation;
                    enemyAIComp.navMeshAgent.stoppingDistance = stoppingDistanceFromPlayers;
                }
                else
                {
                    enemyAIComp.chaseTimer = 0.0f;
                }

                if (enemyAIComp.navMeshAgent.remainingDistance < enemyAIComp.navMeshAgent.stoppingDistance)
                {
                    enemyAIComp.chaseTimer += Time.deltaTime;

                    if (enemyAIComp.chaseTimer > enemyAIComp.chaseWaitTime)
                    {
                        playersLastLocation.playerOnePosition = playersLastLocation.resetPosition;
                        playersLastLocation.playerTwoPosition = playersLastLocation.resetPosition;
                        enemySenses.playerOneLastKnownLocation = playersLastLocation.resetPosition;
                        enemySenses.playerTwoLastKnownLocation = playersLastLocation.resetPosition;
                        enemyAIComp.chaseTimer = 0.0f;
                    }
                }
                else
                {
                    enemyAIComp.chaseTimer = 0.0f;
                }
            }
        }
    }

    //If the player escapes the enemy, they should continue to patrol
    public void PatrolWaypoints()
    {
        this.navMeshAgent.speed = this.patrolSpeed;

        if (this.navMeshAgent.destination == playersLastLocation.resetPosition || this.navMeshAgent.remainingDistance < this.navMeshAgent.stoppingDistance)
        {
            this.patrolTimer += Time.deltaTime;

            if (this.patrolTimer >= this.patrolWaitTime)
            {
                if (currentWaypointIndex == patrolWayPoints.Length - 1)
                {
                    currentWaypointIndex = 0;
                }
                else
                {
                    currentWaypointIndex++;
                }

                patrolTimer = 0.0f;
            }
        }
        else
        {
            patrolTimer = 0.0f;
        }

        this.navMeshAgent.destination = patrolWayPoints[currentWaypointIndex].position;
        this.navMeshAgent.baseOffset = 0.25f;
    }

}
