using UnityEngine;
using UnityEngine.UI;


public class HealthBarscript_Player : MonoBehaviour
{

public Slider slider;
public Gradient gradient;
public Image fill;

  public void SetMaxHealth(float health)
{
    slider.maxValue = health;
    slider.value = health;

    fill.color =
        gradient.Evaluate(
            slider.normalizedValue);
}
    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        
    }
  
}
