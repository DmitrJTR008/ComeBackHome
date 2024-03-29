using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LobbyHandler : MonoBehaviour
{

    public GameObject GoldCup, WhiteCup;
    
    public Text SpeedRunLastRecord;
    
    public GameObject SpeedRunPanel;
    
    public GameObject AdsConsume;
    private LobbyGameDataHandler _lobbyData;
    private ActorSaveData _saveData;
    [SerializeField] private MainMenuView _view;
    
    private MainMenuModel _model;
    

    public void OurGames()
    {
    }


    private void Start()
    {
        _lobbyData = new LobbyGameDataHandler(this, InitGame);
        try
        {
            RefreshRate mainDisplay = Screen.currentResolution.refreshRateRatio;

            int maxRefreshRate = Mathf.RoundToInt(mainDisplay.numerator);
            Application.targetFrameRate = maxRefreshRate;
        }
        catch
        {
            
        }
    }
    
    void InitGame(IDataSaveable data)
    {
        _saveData = data as ActorSaveData;
        _model = _view._presenter._model;
        _model.OnDataChange += SaveGame;
        _model.InitGameData(_saveData);
        SpeedRunLastRecord.text = FormatSpeedRunText(_saveData.SpeedRunTimer);
        
        if(_saveData.IsCarrerComplete)
            GoldCup.SetActive(true);
        if(_saveData.IsSpeedRunComplete)
            WhiteCup.SetActive(true);
        
        if(SpeedRunTimer.Instance.isActivate)
            SpeedRunTimer.Instance.isActivate = false;

        
        
    }

    string FormatSpeedRunText(float totalMinutes)
    {
        int hours = Mathf.FloorToInt(totalMinutes / 60);
        int minutes = Mathf.FloorToInt(totalMinutes % 60);
        int seconds = Mathf.FloorToInt((totalMinutes * 60) % 60);

        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
    /*
    void CunsomeBuffer(string purch)
    {
        switch (purch)
        {
            case "0":
                AdsConsume.SetActive(false);
                _saveData.AdsPurchase = 1;
                YandexGame.savesData.ActorDataHolder = JsonUtility.ToJson(_saveData);
                YandexGame.SaveProgress();
                text.text += purch;
                break;
            case "1":
                _saveData.money += 50000;
                YandexGame.savesData.ActorDataHolder = JsonUtility.ToJson(_saveData);
                YandexGame.SaveProgress();
                _view.UpdateActorUI(_saveData.money);
                text.text += purch;
                break;
                
        }
        text.text += purch;
        */
        
    //}

    void SaveGame(ActorSaveData newData)
    {
        SaveDataHandler.SaveGameServer(newData);
    }

    public void HandlePanelSpeedRun()
    {
        SpeedRunPanel.SetActive(!SpeedRunPanel.activeSelf);
    }
    
    public void StartSpeedRun()
    {

        SpeedRunTimer.Instance.isActivate = true;
        SpeedRunTimer.Instance.StartTimer();
        SceneManager.LoadScene(1);
    }

    /*public void PurchaseSuccess(string id)
    {
        switch (id)
        {
            case "0":
                AdsConsume.SetActive(false);
                _saveData.AdsPurchase = 1;
                YandexGame.savesData.ActorDataHolder = JsonUtility.ToJson(_saveData);
                YandexGame.SaveProgress();
                YandexGame.StickyAdActivity(false);
                
                break;
            case "1":
                _saveData.money += 50000;
                
                YandexGame.savesData.ActorDataHolder = JsonUtility.ToJson(_saveData);
                YandexGame.SaveProgress();
                _view.UpdateActorUI(_saveData.money);
                SaveGame(_saveData);
                break;
        }
    }*/
    
    public void ErrorADS()
    {
        SceneManager.LoadScene(0);
    }
    
    
    
}
