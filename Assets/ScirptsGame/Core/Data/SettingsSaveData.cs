using UnityEngine;

[System.Serializable]
public class SettingsSaveData : IDataLocalSaveable
{
    public float MusicVolume = .5f;
    public float SoundVolume = .5f;
    public int IDLang = 1;
    public int QualityLevel = 1;
    
    public string GetKey()
    {
        return "GameSettingSaveKey";
    }

    public IDataLocalSaveable GetClass()
    {
        return this;
    }
}
