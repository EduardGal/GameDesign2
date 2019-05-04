﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : MonoBehaviour
{
    [SerializeField] float throwHeight;
    [SerializeField] float gravity;
    [SerializeField] Transform target;
    [SerializeField] GameObject unarmed;
    [SerializeField] GameObject rockTargetCursor;
    [SerializeField] LayerMask layer;
    [SerializeField] Rigidbody rock;
    public Material enemiesDefaultMaterial;

    public bool debugPath;

    private Animator anim;
    private Camera cam;
    private Color color;

    LineRenderer lineRenderer;

    GameObject[] Enemies;
    Renderer[] enemyRenderer;
    RockBehaviour rockBehaviour;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
        rockTargetCursor.SetActive(false);

        lineRenderer = GetComponent<LineRenderer>();
        color = new Color(101f, 255f, 0f, 0.5f);

        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Update()
    {
        if (Player.instance.itemInHand.name == "Rock(Clone)")
        {
            rock = Player.instance.itemInHand.GetComponent<Rigidbody>();
        }
        else
        {
            rock = null;
        }

        if (rock != null && Input.GetKey(KeyCode.Mouse0))
        {
            // The aim was, if the spherecollider attached to the rockTarget component was true, then the drones material could change to the highlighted material when in range
            // If the collider is false, the drones material resets to default. This is still a bit buggy.
            rockTargetCursor.GetComponent<SphereCollider>().enabled = true;

            // Instantiate rock target
            CalculateDistanceToTarget();

            lineRenderer.enabled = true;

            if (debugPath)
            {
                target = rockTargetCursor.transform;
                DrawPath();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //play throw animation with the launch animation event
                anim.SetBool("throwRock", true);
            }
        }
        else
        {
            // Here, when the spherecollider is false, the drone's material should be reset to default. This works 90% of the time.
            rockTargetCursor.GetComponent<SphereCollider>().enabled = false;
            rockTargetCursor.transform.position = new Vector3(1000, 1000, 1000);
            //cursor.SetActive(false);
            lineRenderer.enabled = false;
        }
    }

    void CalculateDistanceToTarget()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(camRay, out hit, 100.0f, layer))
        {
            rockTargetCursor.SetActive(true);
            rockTargetCursor.transform.position = hit.point + Vector3.up * 0.1f;
        }
    }

    LaunchTrajectory CalculateLaunchTrajectory()
    {
        float displacementY = target.position.y - rock.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - rock.position.x, 0, target.position.z - rock.position.z);

        float time = Mathf.Sqrt(-2 * throwHeight / gravity) + Mathf.Sqrt(2 * (displacementY - throwHeight) / gravity);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * throwHeight);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchTrajectory (velocityXZ + velocityY, time);
    }

    struct LaunchTrajectory
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchTrajectory(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }

    void DrawPath()
    {
        LaunchTrajectory launchTrajectory = CalculateLaunchTrajectory();
        Vector3 previousDrawPoint = rock.position;

        

        int resolution = 30;
        for(int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchTrajectory.timeToTarget;
            Vector3 displacement = launchTrajectory.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2.0f;
            Vector3 drawPoint = rock.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);

            lineRenderer.material.SetColor("LineRenderer_mat", color);
            lineRenderer.positionCount = resolution + 1;
            lineRenderer.SetPosition(0, rock.transform.position);
            lineRenderer.SetPosition(i, drawPoint);

            previousDrawPoint = drawPoint;
        }
    }


    public void LaunchRock()
    {
        Physics.gravity = Vector3.up * gravity;

        rock.useGravity = true;
        rock.constraints = RigidbodyConstraints.None;
        rock.velocity = CalculateLaunchTrajectory().initialVelocity;

        //make cursor invisible
        //cursor.SetActive(false);
        rockTargetCursor.transform.position = new Vector3(1000, 1000, 1000);

        //Detach Rock from player's item in hand
        rock.transform.parent = null;

        //set the players item in hand to unarmed
        Player.instance.itemInHand = unarmed;

        //stop throwing animation
        anim.SetBool("throwRock", false);
        
    }
}
