using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class StringMapCreator : EditorWindow
{
    List<TextAsset> maps;
    Rect rect;
    string mapString = "";
    GUIStyle textAreaStyle;
    StringMap stringMap;

    [MenuItem("Crying Onion Tools/String Map Creator")]
    private static void OpenWindow()
    {
        StringMapCreator window = GetWindow<StringMapCreator>();
        window.titleContent = new GUIContent("String Map Creator");
    }

    private void OnEnable()
    {
        textAreaStyle = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Scene).textArea;
        textAreaStyle.font = Resources.Load<Font>("StringMap");

        maps = new List<TextAsset>();
        var temp = Resources.LoadAll<TextAsset>("Maps");

        foreach (var map in temp)
        {
            maps.Add(map);
        }

        stringMap = FindObjectOfType<StringMap>();

        if (!stringMap)
        {
            stringMap = new GameObject("String Map").AddComponent<StringMap>();
        }
    }

    private void OnGUI()
    {
        rect = new Rect(0, 20, position.width, position.height);

        mapString = GUI.TextArea(rect, mapString, Mathf.RoundToInt(rect.width * rect.height), textAreaStyle);

        DrawMenuBar();

        if (GUI.changed) Repaint();
    }

    private void DrawMenuBar()
    {
        Rect menuBar = new Rect(0, 0, position.width, 20);
        GUILayout.BeginArea(menuBar, EditorStyles.toolbar);

        GUILayout.BeginHorizontal();

        if(maps != null && maps.Count > 0)
        {
            foreach (var map in maps)
            {
                if (GUILayout.Toggle(mapString.Equals(map.text), new GUIContent(map.name), EditorStyles.toolbarButton))
                {
                    mapString = map.text;
                }
            }
        }

        if(GUILayout.Button("Save Map", EditorStyles.toolbarButton))
        {
            TextAsset map = new TextAsset(mapString);
            map.name = $"map{maps.Count}";
            var path = EditorUtility.SaveFilePanel("Save Map", Application.dataPath, map.name, "txt");

            if (!string.IsNullOrEmpty(path)) 
            {
                maps.Add(map);
                File.WriteAllText(path, map.text);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
}
