using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LevelHandler : MonoBehaviour
{

    private Action<IDataSaveable> OnDataLoad;
    private ActorSaveData _actorData;
    private SettingsSaveData _settingsSave;
    
    private List<PaidMaterialDataSO> GameMaterialsList;
    
    
    [SerializeField] private UIDissolveHandler DissolveGame;
    
    public RocketController MainActor;
    
    public GameUIView gameUIView;
    public GameUIModel gameUIModel;
    private RocketCurrentState rocketState;
    
    Coroutine finishTimerCoroutine;

    private int RewardForLevel;
    
    private void Awake()
    {
        GameMaterialsList = Resources.Load<PainComponentListSO>("PaidListSO").MaterialList;
        _actorData = new ActorSaveData();
        _settingsSave = new SettingsSaveData();
        RewardForLevel = 200;
    }

    void InitResources(IDataSaveable _data)
    {
        _actorData = _data as ActorSaveData;
        
        for (int i = 0; i < GameMaterialsList.Count; i++)
        {
            if (GameMaterialsList[i].ID == _actorData.CurrentSkinID)
            {
                MainActor.SetSkit(GameMaterialsList[i].PaidMaterial);
            }
        }
    }

    private void Start()
    {
        DissolveGame.StartDissolve(true);   
        
        MainActor._rocketAudioComponent.SetVolume(_settingsSave.SoundVolume);
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

    private void OnEnable()
    {

        OnDataLoad += InitResources;
        _settingsSave = SaveDataHandler.LoadGameDataLocal(_settingsSave) as SettingsSaveData;
        SaveDataHandler.LoadGameDataServer(OnDataLoad,_actorData);
        MainActor.OnRocketGameComplete += StartFinishGameCount;
    }
    private void OnDisable()
    {
        MainActor.OnRocketGameComplete -= StartFinishGameCount;
        
        MainActor.OnRocketGameComplete -= StartFinishGameCount;
    }

    private void StartFinishGameCount(RocketCurrentState currentState)
    {
        
        rocketState = currentState;
        if (finishTimerCoroutine != null) return;
        finishTimerCoroutine = StartCoroutine(TimerEnumerator());
    }

    IEnumerator TimerEnumerator()
    {
        float sec = 3f;
        while (sec > 0)
        {
            sec -= Time.deltaTime;
            string text;
            switch (_settingsSave.IDLang)
            {
                case 0:
                    text = "ОТСЧЁТ: ";
                    break;
                case 1:
                    text = "COUNTDOWN: ";
                    break;
                case 2:
                    text = "GERİ SAYIM: ";
                    break;
                default:
                    text = "ОТСЧЁТ: ";
                    break;
            }
            gameUIView.UpdateUITimer(text,sec);
            yield return new WaitForEndOfFrame();
        }
        
        switch (rocketState)
        {
            case RocketCurrentState.Win:
                gameUIView.UpdateUITimer(GetTextResult(true), 0);
                Invoke("LoadNextScene", 1f);
                break;
            case RocketCurrentState.Lose:
                gameUIView.UpdateUITimer(GetTextResult(false), 0);
                
                Invoke("TimeToReloadGame", 1f);
                break;
        }
        DissolveGame.StartDissolve(false);
        
        switch (rocketState)
        {
            case RocketCurrentState.Win:
                break;
            case RocketCurrentState.Lose:
                break;
        }
        
    }

    string GetTextResult(bool isWin)
    {
        int langIndex = _settingsSave.IDLang;
        gameUIView.ShutDownTimer();
        string result = "УСПЕШНО";
        switch (isWin)
        {
            case true:
                switch (langIndex)
                {
                    case 0:
                        result = $"УСПЕШНО: ";
                        gameUIView.UpdateRewardText($"НАГРАДА: {RewardForLevel}$");
                        break;
                    case 1:
                        result = $"SUCCESS: ";
                        gameUIView.UpdateRewardText($"REWARD: {RewardForLevel}$");
                        break;
                    case 2:
                        result = $"BAŞARILI: ";
                        gameUIView.UpdateRewardText($"ÖDÜL {RewardForLevel}$");
                        break;
                }
                break;
            case false:
                switch (langIndex)
                {
                    case 0:
                        result = "ПРОВАЛ: ";
                        break;
                    case 1:
                        result = "FAIL: ";
                        break;
                    case 2:
                        result = "BAŞARISIZLIK: ";
                        break;
                }
                break;
        }

        return result;
    }
    void TimeToReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextScene()
    {
        int totalScene = SceneManager.sceneCountInBuildSettings;
        int target_scene;
        
        if (SceneManager.GetActiveScene().buildIndex == totalScene-1)
        {
            target_scene = 0;
        }
        else
        {
            target_scene = SceneManager.GetActiveScene().buildIndex + 1;
        }
        
        SaveData();
        SceneManager.LoadScene(target_scene);
    }

    private void SaveData()
    {
        _actorData.money += RewardForLevel;
        if (_actorData.CompleteLvl <= SceneManager.GetActiveScene().buildIndex)
        {
            if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
            {
                _actorData.CompleteLvl = SceneManager.GetActiveScene().buildIndex ;
            }
        }
        else
        {
            Debug.Log("NotSave");
        }
        SaveDataHandler.SaveGameServer(_actorData);
    }

}
