using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridMapCreator : EditorWindow
{
    int cellSize = 30;
    Vector2 offset;
    Vector2 drag;
    bool isErasing;
    Rect menuBar;

    List<List<Node>> nodes;
    List<List<PartScripts>> parts;

    Vector2 nodePos;
    StyleManager styleManager;
    ButtonStyle currentStyle;
    GameObject theMap;

    [MenuItem("Crying Onion Tools/Grid Map Creator")]
    private static void OpenWindow()
    {
        GridMapCreator window = GetWindow<GridMapCreator>();
        window.titleContent = new GUIContent("Grid Map Creator");
    }

    private void OnEnable()
    {
        SetupStyles();
        SetUpNodesAndParts();
        SetupMap();
    }

    private void SetupMap()
    {
        try
        {
            theMap = GameObject.Find("Map");
            RestoreMap(theMap);
        }
        catch (Exception error) { Debug.LogException(error); }
        
        if(!theMap) theMap = new GameObject("Map");
    }

    private void RestoreMap(GameObject theMap)
    {
        if(theMap.transform.childCount > 0)
        {
            var cells = theMap.GetComponentsInChildren<PartScripts>();

            foreach (PartScripts cell in cells)
            {
                nodes[cell.Row][cell.Col].Style = cell.Style;
                parts[cell.Row][cell.Col] = cell;
            }
        }
    }

    private void SetupStyles()
    {
        try
        {
            styleManager = GameObject.FindObjectOfType<StyleManager>();

            if (!styleManager)
            {
                styleManager = new GameObject("Style Manager").AddComponent<StyleManager>();
            }
        }
        catch (Exception error) { Debug.LogException(error); }

        currentStyle = styleManager.Data.buttonStyles[0];
    }

    private void SetUpNodesAndParts()
    {
        nodes = new List<List<Node>>();
        parts = new List<List<PartScripts>>();

        for (int i = 0; i < 20; i++)
        {
            nodes.Add(new List<Node>());
            parts.Add(new List<PartScripts>());

            for (int j = 0; j < 10; j++)
            {
                nodePos.Set(i * cellSize, j * cellSize);
                nodes[i].Add(new Node(nodePos, cellSize, cellSize));
                parts[i].Add(null);
            }
        }
    }

    private void OnGUI()
    {
        DrawGrid();
        DrawNodes();
        DrawMenuBar();
        ProcessNodes(Event.current);
        ProcessGrid(Event.current);

        if (GUI.changed)
        {
            Repaint();
        }
    }

    private void DrawMenuBar()
    {
        menuBar = new Rect(0, 0, position.width, 20);
        GUILayout.BeginArea(menuBar, EditorStyles.toolbar);

        GUILayout.BeginHorizontal();

        foreach (var item in styleManager.Data.buttonStyles)
        {
            if (GUILayout.Toggle(currentStyle.Equals(item), new GUIContent(item.Text), EditorStyles.toolbarButton))
            {
                currentStyle = item;
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    private void ProcessNodes(Event e)
    {
        int row = (int)((e.mousePosition.x - offset.x) / cellSize);
        int col = (int)((e.mousePosition.y - offset.y) / cellSize);

        if (e.type == EventType.MouseDown && e.button == 0)
        {
            if (nodes[row][col].Style.name == "Empty")
            {
                isErasing = false;
            }
            else
            {
                isErasing = true;
            }

            PaintNodes(row, col);
        }

        if (e.type == EventType.MouseDrag && e.button == 0)
        {
            PaintNodes(row, col);
            e.Use();
        }
    }

    private void PaintNodes(int row, int col)
    {
        if (isErasing)
        {
            if (parts[row][col])
            { 
                nodes[row][col].Style = null;
                DestroyImmediate(parts[row][col].gameObject);
                parts[row][col] = null;
                GUI.changed = true;
            }
        }
        else
        {
            if (!parts[row][col])
            {
                nodes[row][col].Style = currentStyle.Style;
                PartScripts part = Instantiate(currentStyle.Part, theMap.transform);
                part.name = currentStyle.Text;
                part.transform.position = new Vector3(col * 10, 0, row * 10) + Vector3.forward * 5 + Vector3.right * 5;
                part.Style = currentStyle.Style;
                part.Row = row;
                part.Col = col;
                parts[row][col] = part;
                GUI.changed = true;
            }
        }
    }

    private void DrawNodes()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                nodes[i][j].Draw();
            }
        }
    }

    private void ProcessGrid(Event e)
    {
        drag = Vector2.zero;

        switch (e.type)
        {
            case EventType.MouseDrag:

                if(e.button == 1)
                {
                    OnMouseDrag(e.delta);
                }

                break;
        }
    }

    private void OnMouseDrag(Vector2 delta)
    {
        drag = delta;

        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                nodes[i][j].Drag(delta);
            }
        }

        GUI.changed = true;
    }

    private void DrawGrid()
    {
        int widthDivider = Mathf.CeilToInt(position.width / cellSize);
        int heightDivider = Mathf.CeilToInt(position.height / cellSize);

        Handles.BeginGUI();
        Handles.color = Color.cyan;

        offset += drag;

        Vector3 newOffset = new Vector3(offset.x % cellSize, offset.y % cellSize, 0);

        for (int i = 0; i < widthDivider; i++)
        {
            Handles.DrawLine(new Vector3(cellSize * i, -cellSize, 0) + newOffset, new Vector3(cellSize * i, position.height, 0) + newOffset);
        }

        for (int i = 0; i < heightDivider; i++)
        {
            Handles.DrawLine(new Vector3(-cellSize, cellSize * i, 0) + newOffset, new Vector3(position.width, cellSize * i, 0) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }
}