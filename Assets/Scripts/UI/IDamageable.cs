using System;

public interface IDamageable
{
    event Action<int> healthChanged;
    event Action<int> maxHealthChanged;

    int Health { get; set; }
    int MaxHealth { get; set; }

    void SetDamage(int value);
}