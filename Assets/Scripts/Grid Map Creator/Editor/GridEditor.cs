using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    Grid grid;

    private void OnEnable() => grid = (Grid)target;

    public override void OnInspectorGUI()
    {
        grid.Width = EditorGUILayout.FloatField(" Grid Width ", grid.Width);
        grid.Height = EditorGUILayout.FloatField(" Grid Height ", grid.Height);
        grid.Data = (GridData)EditorGUILayout.ObjectField("Grid Data", grid.Data, typeof(GridData), false);

        SceneView.RepaintAll();
    }
}
