using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PoliceAI : MonoBehaviour
{

    public GameObject player;
    Rigidbody2D rb;
    LineRenderer lineRenderer;

    Vector2 playerPosition;
    //Vector2 selfPosition;

    float distanceToPlayer;
    Vector2 dirToPlayer; // direction to player

    //Ray ray = new Ray(rb.position, transform.forward);
    RaycastHit hit;

    [Tooltip("Lower Number = Higher Accuracy    ")]
    public int navigationAccuracy = 10;
    int pointsOnLine;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();

        ///////////// Line Renderer For debugging //////////////
        lineRenderer.positionCount = 2; // will need to be changed when obstacles are added
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.useWorldSpace = true; 
        ////////////////////////////////////////////////////////
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerLocation();
        DrawLineToPlayer();
    }

    void FixedUpdate()
    {
        lineRenderer.positionCount = (int)(distanceToPlayer / navigationAccuracy); // draw more points for higher accuracy
        pointsOnLine = lineRenderer.positionCount;
    }

    private void CheckPlayerLocation()
    {
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;

        playerPosition = player.transform.position;

        distanceToPlayer = Vector2.Distance(rb.position, playerPosition);
        dirToPlayer = playerPosition - rb.position;
    }

    void DrawLineToPlayer()
    {

        //////////// For scene view ////////////////
        Vector3 origin = new Vector3(rb.position.x, rb.position.y, 0);
        Vector3 direction = new Vector3(dirToPlayer.x, dirToPlayer.y, 0);

        // Draw a line from the enemy to the player
        Debug.DrawRay(origin, direction/10, Color.red);
        ////////////////////////////////////////////

        //////////// For Game View ////////////////
        ///
        /*
        for (int i = 0; i <= pointsOnLine; i++)
        {
            if (Physics.Raycast2D(ray, out hit, 100f))



                lineRenderer.SetPosition(i, rb.position + );


            //lineRenderer.SetPosition(, playerPosition);
        }
        
        ///////////////////////////////////////////
        ///


        // 1. get direction to player
        // 2. draw raycast short length in direction to player
        // 3. check if the end of raycast collides with obstacle
        // 4. if not, repeat step 1 from the end of previous raycast
        // 5. if does, point ray to right slightly
            // if works go back to step one
            // if not check left side
        
        // end when last raycast reaches player
        
        */

    }

}
