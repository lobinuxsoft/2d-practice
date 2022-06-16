using System;

public interface IDamageable
{
    int Health { get; set; }
    int MaxHealth { get; set; }

    void SetDamage(int value);

    event Action<int> healthChanged;
    event Action<int> maxHealthChanged;
}