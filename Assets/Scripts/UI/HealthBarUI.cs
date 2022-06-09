using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarUI : HealthUI
{
    private ProgressBar bar;

    protected override void OnEnable()
    {
        base.OnEnable();
        bar = document.rootVisualElement.Q<ProgressBar>();

        bar.value = (float)damageable.Health / damageable.MaxHealth;
        bar.title = $"{damageable.Health}/{damageable.MaxHealth}";
    }

    protected override void OnHealthChanged()
    {
        bar.value = (float)damageable.Health / damageable.MaxHealth;
        bar.title = $"{damageable.Health}/{damageable.MaxHealth}";

        if (damageable.Health > 0)
        {
            if (document.rootVisualElement.style.display == DisplayStyle.None)
            {
                document.rootVisualElement.style.display = DisplayStyle.Flex;
            }
        }
        else
        {
            document.rootVisualElement.style.display = DisplayStyle.None;
        }
    }

    protected override void OnMaxHealthChanged()
    {
        bar.value = (float)damageable.Health / damageable.MaxHealth;
        bar.title = $"{damageable.Health}/{damageable.MaxHealth}";
    }
}