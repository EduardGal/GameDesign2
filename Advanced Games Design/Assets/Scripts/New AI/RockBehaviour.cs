using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
    Rigidbody rb;
    EnemyCount enemyCount;
    EnemyAI enemyAI;
    private PlayersLastLocation playersLastLocation;

    public bool rockHasBeenThrown;
    bool rockHitGround;
    private float waitTimer;
    private float enemyWaitTime = 5.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rockHitGround = false;
        rockHasBeenThrown = false;
        playersLastLocation = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayersLastLocation>();
    }

    private void Start()
    {
        enemyCount = EnemyCount.instance;
    }

    private void Update()
    {
        if(this.rockHitGround)
        {
            RockHasHitGround();
        }
        else
        {
            this.rockHitGround = false;
            this.waitTimer = 0.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            this.rb.constraints = RigidbodyConstraints.FreezeAll;
            this.rockHitGround = true;
        }
    }

    void RockHasHitGround()
    {
        // If any of the players have been spotted, throwing a rock shouldn't distract the drones
        // This has not been tested. If it doesn't work as intended, this needs to be improved

        if (playersLastLocation.playerOnePosition == playersLastLocation.resetPosition && playersLastLocation.playerTwoPosition == playersLastLocation.resetPosition)
        {
            // Once the rock collides with the ground, check the array of enemies to see if any are in range
            for (int i = 0; i < enemyCount.numberOfEnemies.Length; i++)
            {
                // check the distance between each enemy within the array and the rock which was thrown
                var distanceBetweenRockAndEnemy = Vector3.Distance(enemyCount.numberOfEnemies[i].transform.position, this.transform.position);

                // if that distance is less than 20 (testing distances) execute this if statement
                if (distanceBetweenRockAndEnemy < 7)
                {
                    // Find the rocks direction so the drone knows where to look at
                    // The problem is when the rock is thrown directly under the drone, or close by, the drone doesn't look directly at it, just in its general direction

                    //Vector3 targetDir = this.transform.position - enemyCount.numberOfEnemies[i].transform.position;
                    //enemyCount.numberOfEnemies[i].transform.rotation = Quaternion.RotateTowards(enemyCount.numberOfEnemies[i].transform.rotation,
                    //Quaternion.LookRotation(targetDir), Time.deltaTime * 100.0f);

                    var tarPos = Quaternion.LookRotation(this.transform.position - enemyCount.numberOfEnemies[i].transform.position);
                    enemyCount.numberOfEnemies[i].transform.rotation = Quaternion.RotateTowards(enemyCount.numberOfEnemies[i].transform.rotation, tarPos, 100.0f * Time.deltaTime);

                    //Vector3 targetPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    //enemyCount.numberOfEnemies[i].transform.LookAt(targetPosition);

                    // move the drone to the rocks position, I wanted to create an offset so the drones light is on the rock but it's not working too well
                    enemyCount.numberOfEnemies[i].GetComponent<EnemyAI>().navMeshAgent.destination = this.gameObject.transform.position;
                    enemyCount.numberOfEnemies[i].GetComponent<EnemyAI>().navMeshAgent.stoppingDistance = 7.0f;

                    // when at the rocks position, begin wait timer
                    this.waitTimer += Time.deltaTime;

                    // if wait timer hits X amount of seconds, the drone should go back to patrolling
                    if (this.waitTimer >= enemyWaitTime)
                    {
                        enemyCount.numberOfEnemies[i].GetComponent<EnemyAI>().PatrolWaypoints();
                        this.waitTimer = 0.0f;
                        this.rockHitGround = false;
                    }
                }
                else
                {
                    this.rockHitGround = false;
                    this.waitTimer = 0.0f;
                }
            }
        }
    }
}
