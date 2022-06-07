using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    Grid grid;

    private void OnEnable()
    {
        grid = (Grid)target;
        SceneView.duringSceneGui += GridUpdate;

        SceneView.lastActiveSceneView.rotation = Quaternion.LookRotation(Vector3.down);
        SceneView.lastActiveSceneView.LookAt(grid.transform.position);
        SceneView.lastActiveSceneView.orthographic = true;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= GridUpdate;
    }

    void GridUpdate(SceneView sceneview)
    {
        Event e = Event.current;

        Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
        Vector3 mousePos = r.origin;
        

        if (e.isKey && e.character == 'a')
        {
            GameObject obj;
            Object prefab = PrefabUtility.GetCorrespondingObjectFromSource(Selection.activeObject);

            if (prefab)
            {
                Undo.IncrementCurrentGroup();

                obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab, grid.transform);

                Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.Width) * grid.Width + grid.Width / 2.0f,
                                              0.0f,
                                              Mathf.Floor(mousePos.z / grid.Height) * grid.Height + grid.Height / 2.0f);


                obj.transform.position = aligned;
                Undo.RegisterCreatedObjectUndo(obj, $"Create {obj.name}");
            }
        }
        else if (e.isKey && e.character == 'd')
        {
            Undo.IncrementCurrentGroup();
            foreach (GameObject obj in Selection.gameObjects)
            {
                Undo.DestroyObjectImmediate(obj);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        grid.Width = EditorGUILayout.FloatField(" Grid Width ", grid.Width);
        grid.Height = EditorGUILayout.FloatField(" Grid Height ", grid.Height);

        SceneView.RepaintAll();
    }
}
