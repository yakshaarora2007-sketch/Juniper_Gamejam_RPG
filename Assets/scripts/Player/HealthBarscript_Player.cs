using UnityEngine;
using UnityEngine.UI;


public class HealthBarscript_Player : MonoBehaviour
{

public Slider healthbar;

    public void SetMaxHealth(int health)
    {
        healthbar.maxValue = health;
        healthbar.value = health;
    }
    public void SetHealth(int health)
    {
        healthbar.value = health;
    }
  
}
