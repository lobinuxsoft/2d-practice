using UnityEngine;
using UnityEditor;

public class GridWindow : EditorWindow
{
    Grid grid;

    [MenuItem("Crying Onion Tools/Grid")]
    private static void OpenWindow()
    {
        GridWindow window = GetWindow<GridWindow>();
        window.titleContent = new GUIContent("Grid");
    }

    private void OnEnable() => grid = Selection.activeGameObject.GetComponent<Grid>();

    private void OnDisable() => grid = null;

    void OnGUI()
    {
        if (!grid)
        {
            if (GUILayout.Button("Create Grid Object")) CreateGrid();
        }
        else
        {
            EditorGUILayout.BeginVertical();
            grid.Width = EditorGUILayout.FloatField("Grid Width", grid.Width);
            grid.Height = EditorGUILayout.FloatField("Grid Height", grid.Height);
            grid.gridColor = EditorGUILayout.ColorField("Grid Color", grid.gridColor);
            EditorGUILayout.EndVertical();
        }
    }

    private void CreateGrid()
    {
        grid = new GameObject("Grid").AddComponent<Grid>();
        Selection.activeGameObject = grid.gameObject;
    }
}