using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    private PlayerHealth playerRef;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>())
        {
            playerRef = other.gameObject.GetComponent<PlayerHealth>();
            playerRef.TakeDamage(damage);
        }
    }
}
