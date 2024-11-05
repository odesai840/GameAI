using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    private PlayerHealth playerRef;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>())
        {
            playerRef = collision.gameObject.GetComponent<PlayerHealth>();
            playerRef.TakeDamage(damage);
        }
    }
}
