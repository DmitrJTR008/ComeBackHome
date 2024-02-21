using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Windows;
using YG;
using File = System.IO.File;

public static class SaveDataHandler 
{

    public static void SaveGameServer(IDataSaveable data)
    {
        
        
        
        IDataSaveable result = data.GetClass();

        string dataSave = JsonUtility.ToJson(result);
        

        if (YandexGame.auth)
        {
            YandexGame.savesData.ActorDataHolder = dataSave;
            YandexGame.SaveProgress();
        }
        else
        {
            string dataText = JsonUtility.ToJson(data);
            PlayerPrefs.SetString("localActor",dataText);
            PlayerPrefs.Save();
        }
    }

    public static void LoadGameDataServer(Action<IDataSaveable> callBack, IDataSaveable data)
    {
        IDataSaveable result = data.GetClass();

        if (YandexGame.auth )
        {
            if (PlayerPrefs.HasKey("localActor") && string.IsNullOrWhiteSpace(YandexGame.savesData.ActorDataHolder))
            {
                if (!string.IsNullOrEmpty(PlayerPrefs.GetString("localActor")))
                {
                    string localDataText = PlayerPrefs.GetString("localActor");
                    JsonUtility.FromJsonOverwrite(localDataText, result);
                    Debug.Log("ПЕРЕЗАПИСЬ");
                    // Оставляем только загрузку локальных данных
                    PlayerPrefs.DeleteKey("localActor");
                    PlayerPrefs.Save();
                }
            }
            else if (string.IsNullOrWhiteSpace(YandexGame.savesData.ActorDataHolder))
            {
                SaveGameServer(data);
            }

            JsonUtility.FromJsonOverwrite(YandexGame.savesData.ActorDataHolder, result);
            callBack?.Invoke(result);
        }
        else
        {
            if (!PlayerPrefs.HasKey("localActor"))
            {
                switch (YandexGame.EnvironmentData.language)
                {
                    case "ru":
                        Debug.Log("RU!");
                        break;
                    case "en":
                        Debug.Log("EN");
                        break;
                    case "tr":
                        Debug.Log("TR");
                        break;
                    
                }
                
                SaveGameServer(data);
            }

            string dataText = PlayerPrefs.GetString("localActor");
            JsonUtility.FromJsonOverwrite(dataText, result);
            callBack?.Invoke(result);
        }
    }

// Вспомогательная функция для получения CompleteLvl из строки JSON

    
    
    public static void SaveGameLocal(IDataLocalSaveable data, bool isFirst = false)
    {
        SettingsSaveData result = data as SettingsSaveData;

        if (isFirst)
        {
            switch (YandexGame.EnvironmentData.language)
            {
                case "ru":
                    result.IDLang = 0;
                    break;
                case "en":
                    result.IDLang = 1;
                    break;
                case "tr":
                    result.IDLang = 2;
                    break;
                default:
                    result.IDLang = 1;
                    break;
            }
        }

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

