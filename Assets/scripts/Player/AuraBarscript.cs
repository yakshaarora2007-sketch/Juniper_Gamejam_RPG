using UnityEngine;
using UnityEngine.UI;


public class AuraBarscript_Player : MonoBehaviour
{

public Slider Aurabar;

    public void SetMaxHealth(int Aura)
    {
        Aurabar.maxValue = Aura;
        Aurabar.value = Aura;
    }
    public void SetHealth(int health)
    {
        Aurabar.value = health;
    }
  
}