using UnityEngine;

public class StyleManager : MonoBehaviour
{
    [SerializeField] StyleManagerData data;

    public StyleManagerData Data
    {
        get => data;
        set => data = value;
    }
}
