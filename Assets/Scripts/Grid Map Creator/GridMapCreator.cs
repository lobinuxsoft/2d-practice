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

    List<List<Node>> nodes;

    GUIStyle emptyStyle;
    Vector2 nodePos;
    StyleManager styleManager;

    [MenuItem("Crying Onion Tools/Grid Map Creator")]
    private static void OpenWindow()
    {
        GridMapCreator window = GetWindow<GridMapCreator>();
        window.titleContent = new GUIContent("Grid Map Creator");
    }

    private void OnEnable()
    {
        SetupStyles();
        emptyStyle = new GUIStyle();
        Texture2D icon = Resources.Load<Texture2D>("Icons/Empty");
        emptyStyle.normal.background = icon;
        SetUpNodes();
    }

    private void SetupStyles()
    {
        try
        {
            styleManager = GameObject.FindObjectOfType<StyleManager>();
        }
        catch (Exception e) { }
    }

    private void SetUpNodes()
    {
        nodes = new List<List<Node>>();

        for (int i = 0; i < 20; i++)
        {
            nodes.Add(new List<Node>());

            for (int j = 0; j < 10; j++)
            {
                nodePos.Set(i * cellSize, j * cellSize);
                nodes[i].Add(new Node(nodePos, cellSize, cellSize, emptyStyle));
            }
        }
    }

    private void OnGUI()
    {
        DrawGrid();
        DrawNodes();
        ProcessNodes(Event.current);
        ProcessGrid(Event.current);

        if (GUI.changed)
        {
            Repaint();
        }
    }

    private void ProcessNodes(Event e)
    {
        int row = (int)((e.mousePosition.x - offset.x) / cellSize);
        int col = (int)((e.mousePosition.y - offset.y) / cellSize);

        if (e.type == EventType.MouseDown && e.button == 0)
        {
            if (nodes[row][col].Style.normal.background.name == "Empty")
            {
                isErasing = false;
            }
            else
            {
                isErasing = true;
            }

            if (isErasing)
            {
                nodes[row][col].Style = emptyStyle;
                GUI.changed = true;
            }
            else
            {
                nodes[row][col].Style = styleManager.Data.buttonStyles[1].Style;
                GUI.changed = true;
            }
        }

        if (e.type == EventType.MouseDrag && e.button == 0)
        {
            if (isErasing)
            {
                nodes[row][col].Style = emptyStyle;
                GUI.changed = true;
            }
            else
            {
                nodes[row][col].Style = styleManager.Data.buttonStyles[1].Style;
                GUI.changed = true;
            }
            e.Use();
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
