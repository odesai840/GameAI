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
    Vector2 selfPosition;

    float distanceToPlayer;
    Vector2 dirToPlayer; // direction to player

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
        Debug.DrawRay(origin, direction, Color.red);
        ////////////////////////////////////////////

        //////////// For Game View ////////////////
        lineRenderer.SetPosition(0, rb.position);
        lineRenderer.SetPosition(1, playerPosition);
        ///////////////////////////////////////////
    }

}
