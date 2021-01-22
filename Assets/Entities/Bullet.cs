using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    private void OnCollisionEnter(Collision collision) {
        // Instantiate the Hit Effect at the given position and roation of it's parent.
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy the bullet.
        Destroy(gameObject);
        // Destroy the effect after 5 seconds.
        Destroy(effect, 5f);
    }
}
