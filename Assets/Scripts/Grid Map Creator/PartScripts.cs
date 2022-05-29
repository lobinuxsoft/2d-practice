using UnityEngine;

public class PartScripts : MonoBehaviour
{
    [SerializeField] int row, col;
    //[SerializeField] string partName = "Empty";
    //[SerializeField] GameObject part;
    GUIStyle style;

    public int Row
    {
        get => row;
        set => row = value;
    }

    public int Col
    {
        get => col;
        set => col = value;
    }

    //public string PartName
    //{
    //    get => partName;
    //    set => partName = value;
    //}

    //public GameObject Part
    //{
    //    get => part;
    //    set => part = value;
    //}

    public GUIStyle Style
    {
        get => style;
        set => style = value;
    }
}
