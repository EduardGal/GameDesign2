using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnalyse : Photon.MonoBehaviour {

    [SerializeField] float analyseTimer = 5.0f;
    [SerializeField] float warningDuration = 3.5f;
    [SerializeField] float followSpeedWhileAnalysing = 1.0f;
    //[SerializeField] AudioClip analyseSound;
    [SerializeField] Light spotlight;
    [SerializeField] float viewDistance, viewAngle;
    [SerializeField] LayerMask layerMask;

    private PlayersLastLocation playersLastLocation;

    bool needReset;
    Color startingSpotlightColour;
    NavMeshAgent navMeshAgent;
    private Animator anim;
    private BoxCollider boxCollider;
    private Transform playerOne, playerTwo;
    private bool analysingPlayer;
    public float playerOneWarningTimer, playerTwoWarningTimer;
    public Vector3 Startpos;
    
    GameObject spotlightPosition;
    public Vector3 spotlightOffset;

    private void Awake()
    {
        transform.position = Startpos;
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerOne = GameObject.FindGameObjectWithTag("PlayerOne").transform;
        playerTwo = GameObject.FindGameObjectWithTag("PlayerTwo").transform;
        startingSpotlightColour = spotlight.color;
        playersLastLocation = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayersLastLocation>();
        //viewAngle = spotlight.spotAngle;

        //spotlightPosition = GameObject.Find("SpotlightPosition");
        //spotlightOffset = spotlightPosition.transform.position - transform.position;
    }

    private void Update()
    {
        
        if(playerTwoWarningTimer >= 5 || playerOneWarningTimer >= 5)
        {
            GameObject.FindGameObjectWithTag("PlayerNetwork").GetComponent<playerNetwork>().GameOver(playerOne.gameObject, playerTwo.gameObject);
            GameObject[] bots = GameObject.FindGameObjectsWithTag("Enemy");
            
            foreach(GameObject go in bots)
            {
                go.GetComponent<EnemyAnalyse>().OnGameOver();
            }
        }
        // If player one is in range, analyse them. (Change drone lighting colour, after X amounts of seconds the game should end, This hasnt been implemented yet)
        if (PlayerOneInRange())
        {
            Debug.Log("PlayerOneInRange");
            this.playerOneWarningTimer += Time.deltaTime;
            AnalysePlayerOne();

            this.playerOneWarningTimer = Mathf.Clamp(playerOneWarningTimer, 0, warningDuration);
            this.spotlight.color = Color.Lerp(Color.yellow, Color.red, playerOneWarningTimer / warningDuration);

        }
        else
        {
            this.playerOneWarningTimer -= Time.deltaTime;
        }

        if (this.playerOneWarningTimer <= 0)
        {
            Debug.Log("out of range");
            playerOneWarningTimer = 0;
            this.spotlight.color = startingSpotlightColour;
        }

        // Analyse Player Two - THIS HAS NOT BEEN TESTED, MAY NEEED TO BE REVISTED. 
        // It works for player one, so I copied and pasted for player two. Not sure what happens if both players are in range
        // or if the timer resets when the closest player to the drone changes, or does the timer continue?? 
        if (PlayerTwoInRange())
        {
            this.playerTwoWarningTimer += Time.deltaTime;
            AnalysePlayerTwo();
            Debug.Log("PlayerTwoInRange");
            this.playerTwoWarningTimer = Mathf.Clamp(playerTwoWarningTimer, 0, warningDuration);
            this.spotlight.color = Color.Lerp(Color.yellow, Color.red, playerTwoWarningTimer / warningDuration);
        }
        else
        {
            this.playerTwoWarningTimer -= Time.deltaTime;
        }

        if (this.playerTwoWarningTimer <= 0)
        {
            Debug.Log("out of range");
            playerTwoWarningTimer = 0;
            this.spotlight.color = startingSpotlightColour;
        }
    }

    bool PlayerOneInRange()
    {
        if (Vector3.Distance(transform.position, playerOne.position) < viewDistance)
        {
            Vector3 directionToPlayer = (playerOne.position - transform.position).normalized;
            float angleBetweenBotAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleBetweenBotAndPlayer < viewAngle / 2.0f)
            {
                if (!Physics.Linecast(transform.position, playerOne.position, layerMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool PlayerTwoInRange()
    {
        if (Vector3.Distance(transform.position, playerTwo.position) < viewDistance)
        {
            Vector3 directionToPlayer = (playerTwo.position - transform.position).normalized;
            float angleBetweenBotAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleBetweenBotAndPlayer < viewAngle / 2.0f)
            {
                if (!Physics.Linecast(transform.position, playerTwo.position, layerMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    void AnalysePlayerOne()
    {
        playersLastLocation.playerOnePosition = playerOne.transform.position;
        navMeshAgent.destination = playerOne.transform.position;
        navMeshAgent.stoppingDistance = 7.0f;
        navMeshAgent.speed = followSpeedWhileAnalysing;
    }

    void AnalysePlayerTwo()
    {
        playersLastLocation.playerTwoPosition = playerTwo.transform.position;
        navMeshAgent.destination = playerTwo.transform.position;
        navMeshAgent.stoppingDistance = 7.0f;
        navMeshAgent.speed = followSpeedWhileAnalysing;
    }



    public void OnGameOver()
    {
        needReset = true;
        if (needReset == true)
        {
            transform.position = Startpos;
        }
        needReset = false;
    }
}
