using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class PoliceAI : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    private LineRenderer lineRenderer;

    Vector2 playerPosition;
    float distanceToPlayer;
    Vector2 dirToPlayer; // direction to player
    

    [Tooltip("Lower Number = Higher Accuracy")]
    public int rayCastDistance = 1;
    int pointsOnLine;
    [Tooltip("Radius of circle that police AI will check for collisions")]
    public float checkRadius = 0.5f;

    // Colliders to avoid
    private Collider2D playerCollider;
    private Collider2D selfCollider;

    // TESTING
    public GameObject waypointCircle;
    private List<GameObject> waypointObjects = new List<GameObject>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        
        // Colliders to avoid
        playerCollider = player.GetComponent<Collider2D>();
        selfCollider = GetComponent<Collider2D>();

        
    }
    void Update()
    {
        CheckPlayerLocation();
        DeleteAllWaypoints();
        Pathfind();
    }

    void FixedUpdate()
    {
        lineRenderer.positionCount = (int)(distanceToPlayer / rayCastDistance); // draw more points for higher accuracy
        pointsOnLine = lineRenderer.positionCount;
      

        //DrawLineToPlayer();
    }

    private void CheckPlayerLocation()
    {
        playerPosition = player.transform.position;

        distanceToPlayer = Vector2.Distance(rb.position, playerPosition);
        dirToPlayer = (playerPosition - rb.position).normalized;
    }
    private void Pathfind()
    {
        ArrayList wayPoints = new ArrayList();  // List of all points on path

        int wayPointsNeeded = (int)(distanceToPlayer / rayCastDistance); // Number of waypoints needed
        Vector2 rayCastEndpoint = rb.position + dirToPlayer * rayCastDistance; // Start position
        wayPoints.Add(rayCastEndpoint); // Add start position to the list

        float angleStep = 10.0f; // The angle to add after a failed collision check

        for (int i = 0; i < wayPointsNeeded; i++)
        {
            CheckPlayerLocation();

            bool foundClearPath = false;
            float angleOffsetLeft = 0.0f;
            float angleOffsetRight = 0.0f;

            while (!foundClearPath)
            {
                Vector2 previousPoint = rayCastEndpoint; // store current endpoint if need to reverse

                // Calculate possible endpoints by rotating left and right
                Vector2 possibleLeftTurn = previousPoint + (Vector2)(Quaternion.Euler(0, 0, angleOffsetLeft) * dirToPlayer) * rayCastDistance;
                Vector2 possibleRightTurn = previousPoint + (Vector2)(Quaternion.Euler(0, 0, angleOffsetRight) * dirToPlayer) * rayCastDistance;

                // Check for collisions for both possible directions
                Collider2D hitLeft = Physics2D.OverlapCircle(possibleLeftTurn, checkRadius);
                Collider2D hitRight = Physics2D.OverlapCircle(possibleRightTurn, checkRadius);

                if (hitLeft && hitLeft != playerCollider && hitLeft != selfCollider)
                {
                    // If there's a collision on the left, increment the left angle offset
                    angleOffsetLeft += angleStep;
                }
                else if (hitRight && hitRight != playerCollider && hitRight != selfCollider)
                {
                    // If there's a collision on the right, decrement the right angle offset
                    angleOffsetRight -= angleStep;
                }

                // Decide which side to turn based off which side wouldnt collide
                if (!hitLeft && !hitRight)
                {
                    // chose closest direction if both good
                    if (Mathf.Abs(angleOffsetLeft) <= Mathf.Abs(angleOffsetRight))
                    {
                        rayCastEndpoint = possibleLeftTurn;
                    }
                    else
                    {
                        rayCastEndpoint = possibleRightTurn;
                    }
                    foundClearPath = true;
                }
                else if (!hitLeft)
                {
                    // Only the left direction is clear
                    rayCastEndpoint = possibleLeftTurn;
                    foundClearPath = true;
                }
                else if (!hitRight)
                {
                    // Only the right direction is clear
                    rayCastEndpoint = possibleRightTurn;
                    foundClearPath = true;
                }
                else
                {
                    // If both directions are blocked, continue adjusting angles
                    angleOffsetLeft += angleStep;
                    angleOffsetRight -= angleStep;
                }



                // FOR VISUAL TESTING //
                if (foundClearPath) 
                {
                    wayPoints.Add(rayCastEndpoint);
                    GameObject waypoint = Instantiate(waypointCircle, rayCastEndpoint, Quaternion.identity);
                    waypointObjects.Add(waypoint);
                }
            }
        }
    }

    ///////////////////// UNUSED, could be used if way points are farther apart ////////////////////
    bool CheckCollisionBetweenPoints(Vector2 start, Vector2 end) // check for collision in between two points
    {
        Vector2 direction = (end - start).normalized; 
        float distance = Vector2.Distance(start, end); 

        // Perform the raycast
        RaycastHit2D hit = Physics2D.Raycast(start, direction, distance);

        // Check if the raycast hit anything
        return hit.collider != null;
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////



    // for visual testing, delete visual way points so game doesnt crash instantly
    private void DeleteAllWaypoints()
    {
        foreach (GameObject waypoint in waypointObjects)
        {
            Destroy(waypoint);
        }

        waypointObjects.Clear();
    }

}
