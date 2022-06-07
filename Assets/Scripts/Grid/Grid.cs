#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class Grid : MonoBehaviour
{
    private float width = 32.0f;
    private float height = 32.0f;

    public float Width
    {
        get => width;
        set => width = value;
    }

    public float Height
    {
        get => height;
        set => height = value;
    }

#if UNITY_EDITOR
    public Color gridColor = Color.blue;

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = Camera.current.transform.position;

        Handles.color = gridColor;

        for (float z = pos.z - 800.0f; z < pos.z + 800.0f; z += height)
        {
            Handles.DrawDottedLine(new Vector3(-1000000.0f, 0.0f, Mathf.Floor(z / height) * height),
                            new Vector3(1000000.0f, 0.0f, Mathf.Floor(z / height) * height), 1);
        }

        for (float x = pos.x - 1200.0f; x < pos.x + 1200.0f; x += width)
        {
            Handles.DrawDottedLine(new Vector3(Mathf.Floor(x / width) * width, 0.0f, -1000000.0f),
                            new Vector3(Mathf.Floor(x / width) * width, 0.0f, 1000000.0f), 1);
        }
    }
#endif
}
