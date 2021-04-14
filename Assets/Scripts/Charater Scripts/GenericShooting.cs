using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericShooting : MonoBehaviour
{
    // Where the bullet comes out.
    public Transform ejector;
    // Float that determines when the Player is able to shoot again.
    public float timeToShoot;
    // Checks if the weapon projectiles should grow around the Player like a bubble.
    public bool grows;
    public Animator animator;
    // The stored bullet object.
    public GameObject bullet;

    /// <summary>
    /// Checks if the Player is able to shoot their weapon yet.
    /// </summary>
    /// <returns>True or False, depending on if the Player is able to shoot yet.</returns>
    public bool shotReady() => Time.time >= timeToShoot;

    public void setGrows(bool set)
    {
        this.grows = set;
    }

    public bool getGrows()
    {
        return grows;
    }

    public void setBullet(GameObject bullet)
    {
        this.bullet = bullet;
    }

    public void Update()
    {
        if (shotReady() && Input.GetMouseButton(0))
        {
            Shoot(bullet);
        }
    }

    /// <summary>
    /// Generic Shoot Function that handles bullet spawning and behavior.
    /// </summary>
    /// <param name="animator"> The passed-in Animator that determines what animations play. </param>
    /// <param name="bullet"> The passed-in Bullet object. </param>
    public void Shoot(GameObject bullet)
    {
        // Plays a specific animation based on the weapon equipped.
        playAnim(animator);
        // Determines the next time you're able to shoot off the equipped bullet's shot delay.
        timeToShoot = Time.time + bullet.GetComponent<Bullet>().getShotDelay();
        // Basically spawns multiple bullets, up to the bullet's inherent spread number.
        for (int i = 0; i < bullet.GetComponent<Bullet>().getSpreadNumber(); i++)
        {
            // Instantiates a pre-chosen bullet at specified ejector facing the same way as the ejector.
            GameObject bull = Instantiate(bullet, ejector.position, ejector.rotation);
            // If the weapon's bullet should grow aroudn the Player.
            if (grows)
            {
                // Creates a special randomized rotation (to make the thing look trippy)
                bull.transform.rotation = new Quaternion(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
                // Scales it up to 10 times it's original size in 0.9 seconds.
                ScaleToTarget(bull, new Vector3(10f, 10f, 10f), 0.9f);
            }
            // For all other normal guns
            else
            {
                // Tells the bullet where to go and how fast it needs to go.
                // Factors in the spread of the Bullet.
                bull.GetComponent<Rigidbody>().velocity = transform.forward + new Vector3(0, 0, Random.Range(-bullet.GetComponent<Bullet>().getSpread(), bullet.GetComponent<Bullet>().getSpread())) * bullet.GetComponent<Bullet>().getBulletSpeed();
            }
        }
    }

    /// <summary>
    /// Plays animations based on the equipped weapon.
    /// </summary>
    /// <param name="animator"> The animator that deals with whatever object this script is attached to. </param>
    public void playAnim(Animator animator)
    {
        // If it's unarmed:
        if (animator.GetBool("Unarmed"))
        {
            // If it's a growing weapon
            if (grows)
            {
                // Play the guitar anims (only grower so far)
                animator.SetBool("PlayingGuitar", true);
            }
            // Otherwise
            else
            {
                // Tell the animator to play the "DropKick" animation.
                animator.SetTrigger("DropKick");
            }
        }
        // Checks if the pistol is equipped
        else if (animator.GetBool("PistolEquipped"))
        {
            // Tell the animator to play the "Firing" animation.
            animator.SetTrigger("FiringPistol");
        }
        // NOTE: LONGRIFLE FIRING ANIMATION HAS BEEN CUT - IT DOESN'T LOOK GOOD.
    }


    /// <summary>
    /// Scales the bullet to a given target scale over a certain period of time.
    /// </summary>
    /// <param name="bullet"> The object to grow.  </param>
    /// <param name="targetScale"> The scale to which it will grow. </param>
    /// <param name="duration"> The time it will take to reach the scale. </param>
    public void ScaleToTarget(GameObject bullet, Vector3 targetScale, float duration)
    {
        // Run this in the background.
        StartCoroutine(ScaleToTargetCoroutine(bullet, targetScale, duration));
    }

    /// <summary>
    /// Handles the actual growing.
    /// </summary>
    /// <param name="bullet"> The thing to grow. </param>
    /// <param name="targetScale"> The scale to which it will grow. </param>
    /// <param name="duration"> How long it will take to grow. </param>
    /// <returns></returns>
    private IEnumerator ScaleToTargetCoroutine(GameObject bullet, Vector3 targetScale, float duration)
    {
        // Starting Scale of the Bullet before it's modified.
        Vector3 startScale = bullet.transform.localScale;
        // Create a timer.
        float timer = 0.0f;
        // While we got time:
        while (timer < duration)
        {
            // Increase the time
            timer += Time.deltaTime;
            // Divide it by the duration.
            float t = timer / duration;
            //smoother step algorithm
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            // Update the object's scale.
            bullet.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            // Don't return anything.
            yield return null;
        }
        // Seriously.  Don't return anything.
        yield return null;
    }
}
