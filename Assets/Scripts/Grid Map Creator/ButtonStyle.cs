using UnityEngine;

[System.Serializable]
public struct ButtonStyle
{
    [SerializeField] private Texture2D icon;
    [SerializeField] private string text;
    [SerializeField] private PartScripts part;

    private GUIStyle style;

    public Texture2D Icon
    {
        get => icon;
        set => icon = value;
    }

    public string Text
    {
        get => text;
        set => text = value;
    }

    public PartScripts Part
    {
        get => part;
        set => part = value;
    }

    public GUIStyle Style
    { 
        get 
        {
            if(style == null)
            {
                style = new GUIStyle();
            }

            style.normal.background = icon;
            return style; 
        }
        set => style = value;
    }
}