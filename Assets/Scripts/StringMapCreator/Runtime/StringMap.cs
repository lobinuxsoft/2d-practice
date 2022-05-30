using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class StringMap : MonoBehaviour
{
    [SerializeField, Min(0)] int index = 0;
    [SerializeField] List<TextAsset> maps;
    [SerializeField] List<CellData> cellsData;

    public int Index
    {
        get => index;
        set => index = value;
    }

    public void OnValidate()
    {
        if(maps == null)
            maps = new List<TextAsset>();
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

    public void GenerateMap()
    {
        DeleteMapObj();

        string[] lines = Regex.Split(maps[index].text, "\n");

        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                if(lines[row][col] != ' ')
                {
                    var celldata = cellsData.Find((x)=>x.key.Equals(lines[row][col]));
                

                    if (celldata.prefab != null)
                    {
                        GameObject go = Instantiate(celldata.prefab, this.transform);
                        go.transform.localPosition = new Vector3(col, 0f, -row);
                    
                        go.name = $"({row},{col})";
                    }
                    else
                    {
                        Debug.LogError($"No se encontro cell data en la key: {lines[row][col]}");
                    }
                }
            }
        }
    }

    public void DeleteMapObj()
    {
        while(transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}

[System.Serializable]
public struct CellData
{
    public char key;
    public GameObject prefab;
}