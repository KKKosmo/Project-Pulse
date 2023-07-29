using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatusWithHealthBar : CharacterStatus
{
    public HealthBar healthBar;
    public override void Awake()
    {
        base.Awake();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        isAlive = true;
    }
}
