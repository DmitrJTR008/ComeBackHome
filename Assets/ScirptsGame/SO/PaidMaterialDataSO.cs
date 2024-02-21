using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PaidMaterialSO", menuName = "SO/PaidMaterial")]
public class PaidMaterialDataSO : ScriptableObject
{
    public int ID;
    public int Price;
    public Sprite ICO;
    public Material PaidMaterial;
    
}
