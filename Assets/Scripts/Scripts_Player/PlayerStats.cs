using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Image healthStats, staminaStats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayHealthStats(float health) {
        health /= 100f;
        healthStats.fillAmount = health;
    }

    public void displayStaminaStats(float stamina) {
        stamina /= 100f;
        staminaStats.fillAmount = stamina;
    }
}
