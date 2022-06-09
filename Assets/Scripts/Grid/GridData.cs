using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Grid Data", menuName = "Crying Onion Tools/Grid/Grid Data")]
public class GridData : ScriptableObject
{
    [SerializeField] private List<CellData> cellDatas = new List<CellData>();

    public List<CellData> CellDatas => cellDatas;
}
