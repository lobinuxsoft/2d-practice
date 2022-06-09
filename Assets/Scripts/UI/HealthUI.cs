using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public abstract class HealthUI : MonoBehaviour
{
    [SerializeField] protected GameObject owner;
    protected UIDocument document;
    protected IDamageable damageable;

    protected void Awake()
    {
        document = GetComponent<UIDocument>();
        damageable = owner.GetComponent<IDamageable>();
    }

    protected virtual void OnEnable()
    {
        damageable.healthChanged += OnHealthChanged;
        damageable.maxHealthChanged += OnMaxHealthChanged;
    }

    protected virtual void OnDisable()
    {
        damageable.healthChanged -= OnHealthChanged;
        damageable.maxHealthChanged -= OnMaxHealthChanged;
    }

    protected virtual void OnValidate()
    {
        if (owner != null)
        {
            if (owner.GetComponent<IDamageable>() == null)
            {
                Debug.LogWarning("The owner must implement the IDamageable interface!");
                owner = null;
            }
        }
    }

    protected abstract void OnHealthChanged();

    protected abstract void OnMaxHealthChanged();
}