using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class SaveDataHandler 
{

    public static void SaveGameServer(IDataSaveable data)
    {
        
        string dataText = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("localActor",dataText);
        PlayerPrefs.Save();
    }

    public static void LoadGameDataServer(Action<IDataSaveable> callBack, IDataSaveable data)
    {
        IDataSaveable result = data.GetClass();

        
            if (!PlayerPrefs.HasKey("localActor"))
            {
               
                SaveGameServer(data);
            }

            string dataText = PlayerPrefs.GetString("localActor");
            JsonUtility.FromJsonOverwrite(dataText, result);
            callBack?.Invoke(result);
        
    }

// Вспомогательная функция для получения CompleteLvl из строки JSON

    
    
    public static void SaveGameLocal(IDataLocalSaveable data, bool isFirst = false)
    {
        SettingsSaveData result = data as SettingsSaveData;

        

        string saveData = JsonUtility.ToJson(result);
        PlayerPrefs.SetString("SettingsGame", saveData);
        PlayerPrefs.Save();
    }

    public static IDataLocalSaveable LoadGameDataLocal(IDataLocalSaveable data)
    {
        IDataLocalSaveable result = data.GetClass();
        string dataText = PlayerPrefs.GetString("SettingsGame", "");

        if (string.IsNullOrEmpty(dataText))
        {
            SaveGameLocal(data, true);
            dataText = PlayerPrefs.GetString("SettingsGame", "");
        }

        JsonUtility.FromJsonOverwrite(dataText, result);
        return result;
    }

}
    
   /*public static void SaveGame(GameDataHolder gameData)
    {
        GameDataHolder gd = gameData;
        string result = JsonUtility.ToJson(gd);
        YandexGame.savesData.GameDataHolder = result;
        YandexGame.SaveProgress();
    }

    public static GameDataHolder LoadGame()
    {
        if(YandexGame.savesData.GameDataHolder == null)
            SaveGame(new GameDataHolder());

        GameDataHolder result = JsonUtility.FromJson<GameDataHolder>(YandexGame.savesData.GameDataHolder);
        Debug.Log(result.CompleteLVL);
        return result;
    }*/

