using UnityEngine.UIElements;

public class HealthBarUI : HealthUI
{
    private ProgressBar bar;
    private int health;
    private int maxHealth;

    protected override void OnEnable()
    {
        base.OnEnable();
        bar = document.rootVisualElement.Q<ProgressBar>();

        health = damageable.Health;
        maxHealth = damageable.MaxHealth;

        bar.value = (float)health / maxHealth;
        bar.title = $"{health}/{maxHealth}";
    }

    protected override void OnHealthChanged(int health)
    {
        this.health = health;
        bar.value = (float)this.health / this.maxHealth;
        bar.title = $"{this.health}/{this.maxHealth}";

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

    protected override void OnMaxHealthChanged(int maxHealth)
    {
        this.maxHealth = maxHealth;
        bar.value = (float)this.health / this.maxHealth;
        bar.title = $"{this.health}/{this.maxHealth}";
    }
}