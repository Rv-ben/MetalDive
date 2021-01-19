using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    private void OnCollisionEnter(Collision collision)
    {
        // Instantiate the Hit Effect at the given position of the bullet without rotation.
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy the effect after 5 seconds.
        Destroy(effect, 5f);
        // Destroy the bullet.
        Destroy(gameObject);
    }
}
