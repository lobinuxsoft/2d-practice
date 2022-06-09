using System;

public interface IDamageable
{
    int Health { get; set; }
    int MaxHealth { get; set; }

    event Action healthChanged;
    event Action maxHealthChanged;
}
