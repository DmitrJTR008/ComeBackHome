using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PaidListSO",menuName = "SO/PaidListSO")]
public class PainComponentListSO : ScriptableObject
{
    public List<PaidMaterialDataSO> MaterialList;
}
