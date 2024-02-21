using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
public class MainMenuModel
{
    private List<PaidMaterialDataSO> GameMaterialsList;
    private MainMenuPresenter _presenter;
    private MainMenuView _view;

    private ActorSaveData _saveData;
    private SettingsSaveData _saveSettingData;
    
    public Action<ActorSaveData> OnDataChange;
    public Action<SettingsSaveData> OnSettingChange;
    public Action OnShopInit;

    private int cachedSelectedMaterialID = -1;

    private bool firstInit = true;
    public MainMenuModel(MainMenuPresenter presenter, MainMenuView view)
    {
        _presenter = presenter;
        _view = view;
        GameMaterialsList = Resources.Load<PainComponentListSO>("PaidListSO").MaterialList;
        
    }

    public void InitGameData(ActorSaveData gameData)
    { 
        _saveData = gameData;
        _view.UpdateTemplateMaterial(GetTargetMaterial(_saveData.CurrentSkinID));
        
        InitSettings();
        InitShop();
        InitCarrerUI();
        _view.UpdateActorUI(GetMoney());
        
    }

    
    void InitSettings()
    {
        _saveSettingData = new SettingsSaveData();
        _saveSettingData = SaveDataHandler.LoadGameDataLocal(_saveSettingData) as SettingsSaveData;
        _view.InitSettings(_saveSettingData);
        _view.UpdateLanguageUI(_saveSettingData.IDLang);
        _view.InitQualityUI(_saveSettingData.QualityLevel);
        _presenter.ChangeQuality(_saveSettingData.QualityLevel);
    }
    void InitShop()
    {
        List<int> playerInventory = _saveData.Inventory;
        for (int i = 0; i < GameMaterialsList.Count(); i++)
        {
            if (!playerInventory.Contains(GameMaterialsList[i].ID))
                _view.InitShop(GameMaterialsList[i]);
            else
                _view.InitInventory(GameMaterialsList[i]);
        }
        OnShopInit?.Invoke();
    }

    void InitCarrerUI()
    {
        _view.InitCarrerUI(GetCompleteLVL());
    }

    public int GetCompleteLVL()
    {
        return _saveData.CompleteLvl;
    }
    public int GetMoney()
    {
        return _saveData.money;
    }

    public Material GetTargetMaterial(int index)
    {
        
        Material target = GameMaterialsList[0].PaidMaterial;
        foreach (var i in GameMaterialsList)
        {
            if (i.ID == index)
                target = i.PaidMaterial;
        }

        
        if (!firstInit && cachedSelectedMaterialID != index)
        {
            cachedSelectedMaterialID = index;
            SaveChoise(index);
        }
        
        firstInit = false;
        return target;
    }

    public void SavePurchase(int index, int money) // after Buy;
    {
        _saveData.Inventory.Add(index);
        _saveData.money -= money; 
        OnDataChange?.Invoke(_saveData);
    }

    public void SaveMoney(int money)
    {
        _saveData.money += money;
        OnDataChange?.Invoke(_saveData);
    }
    public void SaveChoise(int index)
    {
        _saveData.CurrentSkinID = index;
        OnDataChange?.Invoke(_saveData);
    }

    public void SaveDataMusic(float value)
    {
        _saveSettingData.MusicVolume = value;
        SaveDataHandler.SaveGameLocal(_saveSettingData);
    }

    public void SaveDataSound(float value)
    {
        _saveSettingData.SoundVolume = value;
        SaveDataHandler.SaveGameLocal(_saveSettingData);
    }

    public void SaveLanguage(int index)
    {
        _saveSettingData.IDLang = index;
        SaveDataHandler.SaveGameLocal(_saveSettingData);
    }

    public void SaveGraphicChange(int index)
    {
        _saveSettingData.QualityLevel = index;
        SaveDataHandler.SaveGameLocal(_saveSettingData);
    }
}