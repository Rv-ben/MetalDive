using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider slider;

    /// <summary>
    /// Setter for health max value and character's starting value.
    /// </summary>
    /// <param name="max">Health max value in integer.</param>
    public void SetMax(int max) {
        slider.maxValue = max;
        slider.value = max;
    }

    /// <summary>
    /// Setter for character's current health value.
    /// </summary>
    /// <param name="current">Current health value in integer.</param>
    public void SetHealth(int current) {
        slider.value = current;
    }
}
