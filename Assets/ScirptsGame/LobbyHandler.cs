using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class LobbyHandler : MonoBehaviour
{
    private LobbyGameDataHandler _lobbyData;
    private ActorSaveData _saveData;
    [SerializeField] private MainMenuView _view;
    
    private MainMenuModel _model;

    private void Start()
    {
        _lobbyData = new LobbyGameDataHandler(this, InitGame);
        /*try
        {
            RefreshRate mainDisplay = Screen.currentResolution.refreshRateRatio;

            int maxRefreshRate = Mathf.RoundToInt(mainDisplay.numerator);
            Application.targetFrameRate = maxRefreshRate;
        }
        catch
        {
            
        }*/
    }
    
    void InitGame(IDataSaveable data)
    {
        _saveData = data as ActorSaveData;
        _model = _view._presenter._model;
        _model.OnDataChange += SaveGame;
        _model.InitGameData(_saveData);
    }

    void SaveGame(ActorSaveData newData)
    {
        SaveDataHandler.SaveGameServer(newData);
    }

    public void ErrorADS()
    {
        SceneManager.LoadScene(0);
    }
    
    
    
    
}
