using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image image;
    public int healthValue;

    // ADD LOGIC FOR UPDATING HEALTH VALUE


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
    }

    /// <summary>
    /// Setter for character's current health value.
    /// </summary>
    /// <param name="value">Health changing amount in integer. Can be negative or positive.</param>
    public void SetHealth(int value) 
    {
        slider.value = value;
        image.color = gradient.Evaluate(slider.normalizedValue);
        this.healthValue = value;
    }
}

public enum DamageEnum
{
    Pistol = -10,
    AR = -5,
    Minigun = -2,
    ShotPellet = -5,
    Rocket = -30,
    Field = -10,
    Healthkit = 10
}

