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
    /// <param name="value">Health changing amount in integer. Can be negative or positive.</param>
    public void SetHealth(int value) {

        // compute pickup health operation if the value is positive.
        if (value > 0) 
        {
            // if current value is less than max value, then proceed updating health value.
            if (slider.value < slider.maxValue)
            {
                // if the difference between max and current health value is less than changing value, then set to max value to not exceed the health bar.
                if (slider.maxValue - slider.value < value)
                {
                    slider.value = slider.maxValue;
                }
                else
                    slider.value += value;
            }
        }
        // compute lose health operation if the value is negative.
        else if (value < 0)
        {
            if (slider.value - value < 0)
            {
                slider.value = 0;
            }
            else
                slider.value -= value;
        }


        
    }
}
