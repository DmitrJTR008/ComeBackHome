using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ActorSaveData : IDataSaveable
{
    public int money = 0;
    public int CompleteLvl = 0;
    public int CurrentSkinID = 0;
    public float SpeedRunTimer = 0f;
    public List<int> Inventory = new List<int>() { 0 };
    public bool IsWasSpeedRun;
    public bool IsCarrerComplete, IsSpeedRunComplete;
    public IDataSaveable GetClass()
    {
        return this;
    }
}