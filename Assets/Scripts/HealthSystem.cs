using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [Header("Health")]
    public float MaxHealth;

    [Header("Events")]
    public UnityEvent OnHeal;
    public UnityEvent OnDamage;
    public UnityEvent OnDeath;

    private float health = 0;

    private void Start()
    {
        health = MaxHealth;
    }
    public void Heal(float amount)
    {
        health += amount;
        OnHeal.Invoke();
    }
    public void Damage(float amount)
    {
        health -= amount;
        OnDamage.Invoke();

        if(health <= 0)
        {
            OnDeath.Invoke();
            health = 0;
        }
    }
}
