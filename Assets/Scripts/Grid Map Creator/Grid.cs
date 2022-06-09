#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private float width = 32.0f;
    [SerializeField] private float height = 32.0f;
    [SerializeField] private GridData data;

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

    public GridData Data
    {
        get => data;
        set => data = value;
    }

#if UNITY_EDITOR
    public Color gridColor = Color.blue;

    //private void OnDrawGizmos()
    //{
    //    Vector3 pos = Camera.current.transform.position;

    //    Handles.color = gridColor;

    //    for (float z = pos.z - 800.0f; z < pos.z + 800.0f; z += height)
    //    {
    //        Handles.DrawDottedLine(new Vector3(-1000000.0f, 0.0f, Mathf.Floor(z / height) * height),
    //                        new Vector3(1000000.0f, 0.0f, Mathf.Floor(z / height) * height), 1);
    //    }

    //    for (float x = pos.x - 1200.0f; x < pos.x + 1200.0f; x += width)
    //    {
    //        Handles.DrawDottedLine(new Vector3(Mathf.Floor(x / width) * width, 0.0f, -1000000.0f),
    //                        new Vector3(Mathf.Floor(x / width) * width, 0.0f, 1000000.0f), 1);
    //    }
    //}
#endif
}
