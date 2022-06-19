using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class GridWindow : EditorWindow
{
    Grid grid;
    int selected = -1;

    Vector2 scrollPos = Vector2.zero;

    [MenuItem("Crying Onion Tools/Grid/Grid Editor")]
    private static void OpenWindow()
    {
        GridWindow window = GetWindow<GridWindow>();
        window.titleContent = new GUIContent("Grid");
    }

    private void OnEnable() 
    {
        grid = Selection.activeGameObject.GetComponent<Grid>();

        SceneView.duringSceneGui += GridUpdate;

        SceneView.lastActiveSceneView.rotation = Quaternion.LookRotation(Vector3.down);
        SceneView.lastActiveSceneView.LookAt(grid.transform.position);
        SceneView.lastActiveSceneView.orthographic = true;
    }

    private void OnDisable() 
    { 
        grid = null;

        SceneView.duringSceneGui -= GridUpdate;
        SceneView.lastActiveSceneView.orthographic = false;
    }

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
            grid.Data = (GridData)EditorGUILayout.ObjectField("Grid Data", grid.Data, typeof(GridData), false);
            grid.gridColor = EditorGUILayout.ColorField("Grid Color", grid.gridColor);

            EditorGUILayout.LabelField($"Selected: {selected}");
            EditorGUILayout.EndVertical();

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, true);
            if(grid.Data.CellDatas.Count > 0)
            {
                selected = GUILayout.SelectionGrid(selected, CreatePreviews(grid.Data.CellDatas), 2);
            }
            EditorGUILayout.EndScrollView();
        }
    }

    private void CreateGrid()
    {
        grid = new GameObject("Grid").AddComponent<Grid>();
        Selection.activeGameObject = grid.gameObject;
    }

    private Texture[] CreatePreviews(List<CellData> cellDatas)
    {
        Texture[] previews = new Texture[cellDatas.Count];

        for (int i = 0; i < cellDatas.Count; i++)
        {
            previews[i] = AssetPreview.GetAssetPreview(cellDatas[i].prefab);
        }

        return previews;
    }

    void GridUpdate(SceneView sceneview)
    {
        Event e = Event.current;

        Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
        Vector3 mousePos = r.origin;

        Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.Width) * grid.Width + grid.Width / 2.0f,
                                      0.0f,
                                      Mathf.Floor(mousePos.z / grid.Height) * grid.Height + grid.Height / 2.0f);

        string key = $"{aligned.x:0.0},{aligned.y:0.0},{aligned.z:0.0}";

        if(selected > -1)
        {
            Handles.color = Color.yellow;
            Handles.DrawWireDisc(aligned, Vector3.up, .5f);
        }

        //if (e.isKey && e.character == 'a' && selected > -1)
        if ((e.type == EventType.MouseDrag || e.type == EventType.MouseDown) && e.button == 0 && selected > -1)
        {
            GameObject obj;
            Object prefab = grid.Data.CellDatas[selected].prefab;

            if (prefab)
            {
                Undo.IncrementCurrentGroup();

                obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab, grid.transform);

                obj.name = $"{grid.Data.CellDatas[selected].key}{grid.transform.childCount}";
                obj.transform.position = aligned;
                Undo.RegisterCreatedObjectUndo(obj, $"Create {obj.name}");
            }

            e.Use();
        }
        else if (e.isKey && e.character == 'd')
        {
            Undo.IncrementCurrentGroup();
            foreach (GameObject obj in Selection.gameObjects)
            {
                Undo.DestroyObjectImmediate(obj);
            }
        }

        DrawGrid();
    }

    private void DrawGrid()
    {
        Vector3 pos = Camera.current.transform.position;

        Handles.color = grid.gridColor;

        for (float z = pos.z - 800.0f; z < pos.z + 800.0f; z += grid.Height)
        {
            Handles.DrawDottedLine(new Vector3(-1000000.0f, 0.0f, Mathf.Floor(z / grid.Height) * grid.Height),
                            new Vector3(1000000.0f, 0.0f, Mathf.Floor(z / grid.Height) * grid.Height), 1);
        }

        for (float x = pos.x - 1200.0f; x < pos.x + 1200.0f; x += grid.Width)
        {
            Handles.DrawDottedLine(new Vector3(Mathf.Floor(x / grid.Width) * grid.Width, 0.0f, -1000000.0f),
                            new Vector3(Mathf.Floor(x / grid.Width) * grid.Width, 0.0f, 1000000.0f), 1);
        }
    }
}