using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class handles generic bullets.
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// If the Bullet hits something, it'll be deleted.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        // It'll destroy itself immediately if it hits the Environment.
        if (collision.gameObject.tag == "Environment")
        {
            // This destroys the object the script is attached to; thus, the Bullet.
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Update runs every frame, so after 5 seconds the bullet will automatically delete itself.
    /// </summary>
    void Update()
    {
        // Destroys the bullet after 5 seconds.
        Destroy(gameObject, 5f);
    }
}
