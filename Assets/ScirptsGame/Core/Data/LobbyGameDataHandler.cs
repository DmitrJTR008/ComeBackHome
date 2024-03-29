using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyGameDataHandler : BaseGameSceneDataHandler
{
    private LobbyHandler _lobbyHandler;

    private ActorSaveData    _actorData;
    private SettingsSaveData _gameSettings;
    public LobbyGameDataHandler(LobbyHandler lobbyHandler, Action<IDataSaveable> gameDataCallBack)
    {
        _lobbyHandler = lobbyHandler;
        _actorData = new ActorSaveData();
        _gameSettings = new SettingsSaveData();
        OnDataLoadComplete = gameDataCallBack;
        
        SaveDataHandler.LoadGameDataServer(LoadGameData,_actorData);
    }


    void LoadGameData(IDataSaveable gameData)
    {
        _actorData = gameData as ActorSaveData;
        OnDataLoadComplete?.Invoke(_actorData);
    }

    public void SaveGameData(ActorSaveData data)
    {
        SaveDataHandler.SaveGameServer(data);
    }

   
}
