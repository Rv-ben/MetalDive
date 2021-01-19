using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Where the bullet comes out.
    public Transform firePoint;
    // What bullet gets fired.
    public GameObject bulletPrefab;
    // The force of the bullet.
    public float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        // Should be mapped to Left Click for now.
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        // Instantiate a Bullet prefab.
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Get the rigidbody of the bullet.
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        // Add instand force in the direction of the firePoint, using bulletForce as a base.
        rb.AddForce(firePoint.up * bulletForce, ForceMode.Impulse);
    }
}
