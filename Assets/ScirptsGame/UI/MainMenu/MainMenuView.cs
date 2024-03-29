using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    
    
    
    [SerializeField] private UIDissolveHandler DissolveEffect;
    [SerializeField] private GameObject UIMainMenuROOT;
    public MainMenuPresenter _presenter { get; private set; }
    public AdsInitializer AdsObject;

    int x;
    #region UIHierarchy

    [SerializeField] private ActorBillBoardLobby _actorBillBoard;
    public UIShopMenu ShopMenuUI;
    public UIInventoryMenu InventoryMenuUI;
    public ActorDataUI ActorDataMenu;
    public RewardUI UIReward;
    public UISettingMenu SettingMenu;
    public LanguageHandler GameLanguage;
    public UICarrerMenu CarrerMenu;
    #endregion
    
    
    //GRAP PRESETS
    public List<UniversalRenderPipelineAsset> Qualit;
    
    #region UISingle
    [SerializeField] private Button StartGame;
    #endregion

    //Debug

    public void ResetALl()
    {
        
        PlayerPrefs.DeleteAll();
        if(File.Exists(Path.Combine(Application.persistentDataPath,"SettingsGame.json")))
        {
            File.Delete(Path.Combine(Application.persistentDataPath,"SettingsGame.json"));
        }
    }
    // -------
    
    // -- GRAPHIC--
    void GraphicChange(int value)
    {
        
    }
    
    private void Awake()
    {
        _presenter = new MainMenuPresenter(this,Qualit,AdsObject);
    }

    private void OnEnable()
    {
        //ShopMenuUI.OnShopLoad += CallBackShopInit;
        
        DissolveEffect.OnEffectOpen  += StartUI;
        DissolveEffect.OnEffectClose += LoadScene;
        
        _presenter._model.OnShopInit += ShopSub;
        
        //UIReward.RewardBtn.onClick.AddListener(()=>_presenter.ShowRewardAdd(0));
        
        //YandexGame.RewardVideoEvent += _presenter.GetReward;
        
        SubscribeSettings(true);

        AdsObject.rew.OnRewardDone += AddBucks;

        SettingMenu.QualitySettingSlider.onValueChanged.AddListener((value) => { _presenter.ChangeQuality((int)value); });
    }

    private void OnDisable()
    {
        DissolveEffect.OnEffectOpen  -= StartUI;
        DissolveEffect.OnEffectClose -= LoadScene;
        _presenter._model.OnShopInit -= ShopSub;
        ShopSubscribe(false);

        // UIReward.RewardBtn.onClick.RemoveAllListeners();
        // YandexGame.RewardVideoEvent -= _presenter.GetReward;
        SubscribeSettings(false);
        SubscribeUICarrer(false);

        AdsObject.rew.OnRewardDone -= AddBucks;
    }

    public void InitCarrerUI(int count)
    {
        CarrerMenu.InitCarrerUI(count);
        SubscribeUICarrer(true);
    }

    public void InitQualityUI(int value)
    {
        SettingMenu.QualitySettingSlider.value = value;
    }
    
    //REMOVE
    public void AddBucks()
    {
        _presenter.GetReward();
    }
    
    private void SubscribeUICarrer(bool toSub)
    {
        if(toSub)
        {
            int offset = 1;
            int carrerOffset = _presenter._model.GetCompleteLVL() + offset;
            StartGame.onClick.AddListener(()=>StartGameProcess());
            
            StartGame.onClick.AddListener(()=>_presenter.SetTargetScene(carrerOffset));
        

            for (int i = 0; i < CarrerMenu.ButtonList.Count; i++) 
            {
                int index = i + offset;
                CarrerMenu.ButtonList[i].onClick.AddListener(()=>StartGameProcess());
                CarrerMenu.ButtonList[i].onClick.AddListener(()=>_presenter.SetTargetScene(index));
            }
        
        }
        else
        {
            StartGame.onClick.RemoveAllListeners();
            CarrerMenu.ButtonList.ForEach(x => { x.onClick.RemoveAllListeners(); });
        }
    }
    
    private void SubscribeSettings(bool toSub)
    {
        if (toSub)
        {
            SettingMenu.MusicVolumeSlider.onValueChanged.AddListener((value)=>_presenter.SetMusicVolume(value));
            SettingMenu.SoundVolumeSlider.onValueChanged.AddListener((value)=>_presenter.SetSoundVolume(value));
            SettingMenu.RuBtn.onClick.AddListener(()=>_presenter.SetLanguage(0));
            SettingMenu.EnBtn.onClick.AddListener(()=>_presenter.SetLanguage(1));
            SettingMenu.TRBtn.onClick.AddListener(()=>_presenter.SetLanguage(2));
        }
        else
        {
            SettingMenu.MusicVolumeSlider.onValueChanged.RemoveAllListeners();
            SettingMenu.SoundVolumeSlider.onValueChanged.RemoveAllListeners();
            SettingMenu.RuBtn.onClick.RemoveAllListeners();
            SettingMenu.EnBtn.onClick.RemoveAllListeners();
            SettingMenu.TRBtn.onClick.RemoveAllListeners();
        }
    }

    private void ShopSubscribe(bool toSub)
    {
        if (toSub)
        {
            foreach (var i in ShopMenuUI.ActiveButtonList)
            {
                UIShopComponentData shopComponent = i.GetComponentInParent<UIShopComponentData>();
                i.onClick.AddListener(() => _presenter.BuyNewMaterial(shopComponent));
            }
            InventorySubscribe(true);
        }
        else
            ShopMenuUI.ActiveButtonList.ForEach(x => {x.onClick.RemoveAllListeners();});
    }

    private void InventorySubscribe(bool toSub)
    {
        if (toSub)
        {
            foreach (var i in InventoryMenuUI.ActiveButtonList)
            {
                UIInventoryComponentData inventoryComponentData = i.GetComponentInParent<UIInventoryComponentData>();
                int index = inventoryComponentData._componentData.ID;
                inventoryComponentData._componentButtonShow.onClick.AddListener(() =>
                {
                    _presenter.SetTargetMaterial(index);
                });
            }
        }
        else
            InventoryMenuUI.ActiveButtonList.ForEach(x => {x.onClick.RemoveAllListeners();});
    }

    private void ShopSub()
    {
        ShopSubscribe(true);
    }

    public void InitShop(PaidMaterialDataSO data)
    {
        ShopMenuUI.AddNewComponent(data);
    }

    public void InitInventory(PaidMaterialDataSO data)
    {
        InventoryMenuUI.AddNewComponent(data);
    } 

    public void UpdateActorUI(int money)
    {
        ActorDataMenu.UpdateActorDataUI(money);
    }

    public void UpdateLanguageUI(int index)
    {
        GameLanguage.ChangeLang(index);
    }
    public void CompletePurchase(UIShopComponentData data)
    {
        data._componentButtonShow.onClick.RemoveAllListeners();
        PaidMaterialDataSO Item = data._componentData;
        Destroy(data.gameObject);
        InitInventory(Item);
        InventorySubscribe(false);
        InventorySubscribe(true);
        UpdateActorUI(_presenter._model.GetMoney());
        
    }
    
    public void UpdateTemplateMaterial(Material materialData)
    {
        _actorBillBoard.SetNewMaterial(materialData);
    }
    
    void Start()
    {
        DissolveEffect.StartDissolve(true);
    }
    
    void StartUI()
    {
        UIMainMenuROOT.SetActive(true);
    }

    public void InitSettings(SettingsSaveData saveData)
    {
        GameBackgroundRadioHandler.Singleton.ChangeVolume(saveData.MusicVolume);
        SettingMenu.MusicVolumeSlider.value = saveData.MusicVolume;
        SettingMenu.SoundVolumeSlider.value = saveData.SoundVolume;
    }
    
    void StartGameProcess()
    {
        UIMainMenuROOT.SetActive(false);
        DissolveEffect.StartDissolve(false);
    }
    //--------
    void LoadScene()
    {
        _presenter.OpenScene();
    }

  

}
