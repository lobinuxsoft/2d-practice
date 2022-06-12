using System;

public interface IDamageable
{
    int Health { get; set; }
    int MaxHealth { get; set; }

    void SetDamage(int value);

    event Action healthChanged;
    event Action maxHealthChanged;
}