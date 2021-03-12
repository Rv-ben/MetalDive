using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This Class handles generic bullets.
/// </summary>
public class Bullet : MonoBehaviour
{
    // Determines spread accuracy of bullet. 0 = Dead-On, > 0 = Max Random Angle
    public float spread;
    // Delay between shots.
    public float shotDelay;
    // Number of bullets to shoot at a time.  Useful for shotguns.
    public int spreadNumber;
    // Speed of the bullet.
    public float bulletSpeed;
    // How long the bullet "lives" before being deleted.
    public float lifeTime;

    /// <summary>
    /// Constructor for a Bullet
    /// </summary>
    /// <param name="spread"> The accuracy of the bullet.  </param>
    /// <param name="shotDelay"> How long between shots. </param>
    /// <param name="spreadNumber"> How many bullets get spawned per shot. </param>
    /// <param name="bulletSpeed"> How fast the bullet travels. </param>
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
        // If the bullet hits itself (surprisingly necessary)
        if (collision.gameObject.tag == this.gameObject.tag)
        {
            // Do nothing.
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        // It'll destroy itself immediately if it hits the Environment.
        if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
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
        // Destroys the bullet after lifetime seconds.
        Destroy(gameObject, lifeTime);
    }
}
