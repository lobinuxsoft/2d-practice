using UnityEngine;

public class Node
{
    Rect rect;
    GUIStyle style;

    public GUIStyle Style
    {
        get => style;

        set
        {
            if (value == null) value = CreateEmptyStyle();

            style = value;
        }
    }

    public void Drag(Vector2 delta) => rect.position += delta;

    public Node(Vector2 position, float width, float hight, GUIStyle style = null)
    {
        rect = new Rect(position.x, position.y, width, hight);

        if(style == null) style = CreateEmptyStyle();

        this.style = style;
    }

    public void Draw() => GUI.Box(rect, "", style);

    private GUIStyle CreateEmptyStyle()
    {
        var style = new GUIStyle();
        var icon = new Texture2D(1, 1);
        icon.SetPixel(0, 0, Color.black);
        style.normal.background = icon;
        style.name = "Empty";
        return style;
    }
}
