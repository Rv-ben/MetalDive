using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This Class handles generic bullets.
/// </summary>
public class Bullet : MonoBehaviour
{
    public float spread;
    public float shotDelay;
    public int spreadNumber;
    public float bulletSpeed;

    public float lifeTime;

    public Bullet(float spread, float shotDelay, int spreadNumber, float bulletSpeed)
    {
        this.spread = spread;
        this.shotDelay = shotDelay;
        this.spreadNumber = spreadNumber;
        this.bulletSpeed = bulletSpeed;
    }

    public void setSpread(float spread)
    {
        this.spread = spread;
    }

    public float getSpread()
    {
        return spread;
    }

    public void setShotDelay(float shotDelay)
    {
        this.shotDelay = shotDelay;
    }

    public float getShotDelay()
    {
        return shotDelay;
    }

    public void setSpreadNumber(int spreadNumber)
    {
        this.spreadNumber = spreadNumber;
    }

    public int getSpreadNumber()
    {
        return spreadNumber;
    }

    public void setBulletSpeed(float bulletSpeed)
    {
        this.bulletSpeed = bulletSpeed;
    }

    public float getBulletSpeed()
    {
        return bulletSpeed;
    }


    /// <summary>
    /// If the Bullet hits something, it'll be deleted.
    /// </summary>
    /// <param name="collision">A "collision" object implicitly tied to the GameObject that records when two Rigidbody Colliders collide.  </param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == this.gameObject.tag)
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
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
        // Destroys the bullet after 1 second.
        Destroy(gameObject, lifeTime);
    }
}
