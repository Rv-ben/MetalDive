using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthSlider : MonoBehaviour
{
    private GameObject enemyCanvas;
    private Vector3 rotation;

    public Slider slider;
    public Gradient gradient;
    public Image image;
    public int healthValue;
    public int maxHealth;

    /// <summary>
    /// Initialize all the variables with objects upon game starts.
    /// </summary>
    void Start()
    {
        if (gameObject.tag == "Enemy")
        {
            enemyCanvas = transform.GetChild(3).gameObject;

            // Getting enemyCanvas's rotation to use in Update().
            rotation = enemyCanvas.transform.localRotation.eulerAngles;
        }
    }

    /// <summary>
    /// This Update Function will run all code within every frame of the game.
    /// For Enemy: This function will keep updating the orientation of Enemy health bar to stay horizontal on the screen.
    /// </summary>
    void Update()
    {
        if (gameObject.tag == "Enemy")
        {
            // Get enemy's rotation.
            Vector3 enemyQuaternion = gameObject.transform.localRotation.eulerAngles;

            // Use localRotation to rotate only the enemyCanvas, not the Enemy(parent). 
            // Original enemyCanvas's rotation - enemy's rotation will keep the health bar horizontal on the screen.
            enemyCanvas.transform.localRotation = Quaternion.Euler(rotation - enemyQuaternion);
        }
    }

    /// <summary>
    /// Setter for health max value and character's starting value.
    /// </summary>
    /// <param name="max">Health max value in integer.</param>
    public void SetMax(int max)
    {
        slider.maxValue = max;
        slider.value = max;
        image.color = gradient.Evaluate(1f);
        this.healthValue = max;
        this.maxHealth = max;
    }

    /// <summary>
    /// Setter for character's current health value.
    /// </summary>
    /// <param name="value">Health changing amount in integer. Can be negative or positive.</param>
    public void SetHealth(int value)
    {
        if (this.healthValue + value < 0)
        {
            this.healthValue = 0;
        }
        else if (this.healthValue + value > maxHealth)
        {
            this.healthValue = maxHealth;
        }
        else
            this.healthValue += value;

        slider.value = this.healthValue;
        image.color = gradient.Evaluate(slider.normalizedValue);
    }
    /// <summary>
    /// This function will be called when a GameObject collides.
    /// </summary>
    /// <param name="other">Object that touched enemy collider.</param>
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Environment"))
        {
            if (Enum.IsDefined(typeof(HealthEnum), other.tag))
            {
                HealthEnum enum_ = (HealthEnum)Enum.Parse(typeof(HealthEnum), other.tag);
                if (enum_.ToString() == "Healthkit" || gameObject.tag == "Enemy")
                {
                    this.SetHealth((int)enum_);
                    Destroy(other);
                }
            }
        }
    }
}

public enum HealthEnum
{
    Pistol = -10,
    AR = -5,
    Minigun = -2,
    ShotPellet = -5,
    Rocket = -30,
    Field = -10,
    Healthkit = 10
}

