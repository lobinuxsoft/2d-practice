using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField, Min(0)] private int health = 5;
    [SerializeField, Min(0)] private int maxHealth = 5;

    BlinkEffect blinkEffect;
    bool waitDamageEnd = false;

    private void Awake()
    {
        blinkEffect = GetComponent<BlinkEffect>();
        healthChanged += CheckHealth;
    }

    public async void SetDamage(int value)
    {
        if (!waitDamageEnd)
        {
            waitDamageEnd = true;
            Health -= value;
            await blinkEffect.BlinkTask(5);
            waitDamageEnd = false;
        }
    }

    public int Health
    {
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
            healthChanged?.Invoke(health);
        }
    }

    public int MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
            maxHealthChanged?.Invoke(maxHealth);
        }
    }

    private void CheckHealth(int health)
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public event Action<int> healthChanged;
    public event Action<int> maxHealthChanged;
}