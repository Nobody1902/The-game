using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [Header("Health")]
    public float MaxHealth;
    public bool RecoverHealth = false;
    public float RecoverSpeed = 0.0f;
    public float RecoverTime = 0.0f;

    [Header("Events")]
    public UnityEvent OnDamage;

    private float health = 0;

    private void Start()
    {
        health = MaxHealth;

        if(RecoverHealth)
        {
            InvokeRepeating(nameof())
        }
    }
    public void Heal(float health)
    {

    }
}
