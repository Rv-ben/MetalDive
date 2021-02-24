using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Where the bullet comes out.
    public Transform ejector;
    // Float that determines when the Player is able to shoot again.
    public float timeToShoot;

    // How fast the bullet goes by default.
    public float bulletSpeed;

    public bool guitar;

    // The stored bullet
    public GameObject bullet;

    /// <summary>
    /// Checks if the Player is able to shoot their weapon yet.
    /// </summary>
    /// <returns>True or False, depending on if the Player is able to shoot yet.</returns>
    public bool shotReady() => Time.time >= timeToShoot;

    public void setGuitar(bool set)
    {
        this.guitar = set;
    }

    public bool getGuitar()
    {
        return guitar;
    }

    public void setBullet(GameObject bullet)
    {
        this.bullet = bullet;
    }

    public void Shoot(Animator animator, float spread, float shotDelay, int spreadNumber, GameObject bullet)
    {
        playAnim(animator);
        // Determines the next time you're able to shoot.
        timeToShoot = Time.time + shotDelay;
        for (int i = 0; i < spreadNumber; i++)
        {
            // Instantiates a pre-chosen bullet at specified ejector facing the same way as the ejector.
            GameObject bull = Instantiate(bullet, ejector.position, ejector.rotation);
            if (guitar)
            {
                bull.transform.rotation = new Quaternion(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
                ScaleToTarget(bull, new Vector3(2.5f, 2.5f, 7.5f), 2.5f);
            }
            else
            {
                Vector3 direction = transform.forward + new Vector3(0, 0, Random.Range(-spread, spread));
                // Tells the bullet where to go and how fast it needs to go.
                bull.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
            }
        }
    }

    public void playAnim(Animator animator)
    {
        if (animator.GetBool("Unarmed"))
        {
            if (guitar)
            {
                animator.SetBool("PlayingGuitar", true);
            }
            else
            {
                // Tell the animator to play the "DropKick" animation.
                animator.SetTrigger("DropKick");
            }
        }
        else if (animator.GetBool("PistolEquipped"))
        {
            // Tell the animator to play the "Firing" animation.
            animator.SetTrigger("FiringPistol");
        }
        else if (animator.GetBool("LongGunEquipped"))
        {
            // Tell the animator to play the "Firing" animation.
            animator.SetTrigger("FiringLongarm");
        }
    }


    public void ScaleToTarget(GameObject bullet, Vector3 targetScale, float duration)
    {
        StartCoroutine(ScaleToTargetCoroutine(bullet, targetScale, duration));
    }

    private IEnumerator ScaleToTargetCoroutine(GameObject bullet, Vector3 targetScale, float duration)
    {
        Vector3 startScale = bullet.transform.localScale;
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            //smoother step algorithm
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            bullet.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        yield return null;
    }
}
