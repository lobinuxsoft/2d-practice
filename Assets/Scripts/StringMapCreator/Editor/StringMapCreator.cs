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
    GUIStyle toolbarButton;
    int fontSize;
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
        toolbarButton = new GUIStyle(EditorStyles.toolbarButton);
        fontSize = toolbarButton.fontSize;

        LoadMapFromText();
    }

    private void LoadMapFromText()
    {
        if (maps == null) 
        {
            maps = new List<TextAsset>();
        }
        else
        {
            maps.Clear();
            Resources.UnloadUnusedAssets();
        }

        var temp = Resources.LoadAll<TextAsset>("Maps");

        foreach (var map in temp)
        {
            maps.Add(map);
        }
    }

    private void OnGUI()
    {
        if (!stringMap || stringMap.gameObject != Selection.activeGameObject)
        {
            if (Selection.activeGameObject && Selection.activeGameObject.TryGetComponent<StringMap>(out stringMap))
            {
                stringMap.OnValidate();
                mapString = maps[stringMap.Index].text;
            }
            else
            {
                EditorGUILayout.HelpBox($"The object selected is no a {nameof(StringMap)}", MessageType.Warning);
                if (GUILayout.Button($"Create a {nameof(StringMap)} in the scene."))
                {
                    stringMap = new GameObject("String Map").AddComponent<StringMap>();
                    Selection.activeGameObject = stringMap.gameObject;
                    stringMap.OnValidate();
                }
            }
        }
        else
        {
            rect = new Rect(10, 50, position.width - 20, position.height - 60);

            mapString = GUI.TextArea(rect, mapString, Mathf.RoundToInt(rect.width * rect.height), textAreaStyle);

            DrawMenuBar();
        }

        if (GUI.changed) Repaint();
    }

    private void DrawMenuBar()
    {
        Rect menuBar = new Rect(0, 0, position.width, 40);

        GUILayout.BeginArea(menuBar, EditorStyles.toolbar);

        GUILayout.BeginHorizontal();

        if(maps != null && maps.Count > 0)
        {
            for (int i = 0; i < maps.Count; i++)
            {
                toolbarButton.fontStyle = stringMap.Index == i ? FontStyle.Bold : FontStyle.Normal;
                toolbarButton.fontSize = stringMap.Index == i ? Mathf.CeilToInt(fontSize *1.1f) : fontSize;
                toolbarButton.normal.textColor = stringMap.Index == i ? Color.green : Color.white;
                toolbarButton.hover.textColor = stringMap.Index == i ? Color.green : Color.white;
                toolbarButton.active.textColor = stringMap.Index == i ? Color.green : Color.white;

                if (GUILayout.Button(maps[i].name, toolbarButton))
                {
                    mapString = maps[i].text;
                    stringMap.Index = i;
                    stringMap.GenerateMap();
                }
            }
        }

        

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Save Map", EditorStyles.toolbarButton))
        {
            TextAsset map = new TextAsset(mapString);
            map.name = $"map{maps.Count}";
            var path = EditorUtility.SaveFilePanel("Save Map", Application.dataPath, map.name, "txt");

            if (!string.IsNullOrEmpty(path))
            {
                File.WriteAllText(path, map.text);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            LoadMapFromText();
            stringMap.OnValidate();
            stringMap.GenerateMap();
        }

        if (stringMap.transform.childCount > 0)
        {
            if (GUILayout.Button("Delete Generated Map", EditorStyles.toolbarButton))
            {
                stringMap.DeleteMapObj();
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
}
