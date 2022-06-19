using UnityEngine;
using UnityEngine.UIElements;

public class HeartsUI : HealthUI
{
    private VisualElement hearts;

    protected override void OnEnable()
    {
        base.OnEnable();
        hearts = document.rootVisualElement.Q("panel");
        OnMaxHealthChanged(damageable.Health);
        OnHealthChanged(damageable.MaxHealth);
    }

    protected override void OnHealthChanged(int health)
    {
        for (int i = 0; i < hearts.childCount; i++)
        {
            if (i < health)
            {
                hearts[i][0].style.visibility = Visibility.Visible;
            }
            else
            {
                hearts[i][0].style.visibility = Visibility.Hidden;
            }
        }
    }

    protected override void OnMaxHealthChanged(int maxHealth)
    {
        for (int i = 0; i < hearts.childCount; i++)
        {
            if (i < maxHealth)
            {
                hearts[i].style.visibility = Visibility.Visible;
            }
            else
            {
                hearts[i].style.visibility = Visibility.Hidden;
            }
        }
    }

    protected override void OnValidate()
    {
        base.OnValidate();

        if (!owner)
        {
            if (owner.GetComponent<IDamageable>().MaxHealth > 20)
            {
                Debug.LogWarning("The maximum health cannot exceed 20!");
                owner = null;
            }
        }
    }
}
