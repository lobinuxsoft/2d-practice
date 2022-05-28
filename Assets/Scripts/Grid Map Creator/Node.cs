using UnityEngine;

public class Node
{
    Rect rect;
    GUIStyle style;

    public GUIStyle Style
    {
        get => style;
        set => style = value;
    }

    public void Drag(Vector2 delta) => rect.position += delta;

    public Node(Vector2 position, float width, float hight, GUIStyle style)
    {
        rect = new Rect(position.x, position.y, width, hight);
        this.style = style;
    }

    public void Draw() => GUI.Box(rect, "", style);
}
