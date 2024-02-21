using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using YG;

public class MainMenuPresenter
{
    public MainMenuView _view { get; private set; }
    public MainMenuModel _model { get; private set; }
    private List<UniversalRenderPipelineAsset> Qualities;

    private int nextScene;
    
    public MainMenuPresenter(MainMenuView view, List <UniversalRenderPipelineAsset> qualit)
    {
        _view = view;
        Qualities = qualit;
        _model = new MainMenuModel(this, _view);
    }

    public void BuyNewMaterial(UIShopComponentData data)
    {
        
        if (_model.GetMoney() < data._componentData.Price) return;
        _model.SavePurchase(data._componentData.ID,data._componentData.Price);
        _view.CompletePurchase(data);
    }

    public void SetTargetMaterial(int ID)
    {
        _view.UpdateTemplateMaterial(_model.GetTargetMaterial(ID));
    }

    public void ShowRewardAdd(int ID)
    {
        YandexGame.RewVideoShow(ID);
    }

    public void GetReward(int ID)
    {
        
        _model.SaveMoney(1000);
        _view.UpdateActorUI(_model.GetMoney());
    }

    
    public void SetMusicVolume(float value)
    {
        GameBackgroundRadioHandler.Singleton.ChangeVolume(value);
        _model.SaveDataMusic(value);
    }

    public void SetSoundVolume(float value)
    {
        _model.SaveDataSound(value);
    }

    public void SetLanguage(int index) 
    {
        _model.SaveLanguage(index);
        _view.UpdateLanguageUI(index);
    }

    public void SetTargetScene(int index)
    {
        nextScene = index;
    }

    public void OpenScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void ChangeQuality(int value)
    {
        switch (value)
        {
            case 0:
                GraphicsSettings.renderPipelineAsset = Qualities[0];
                QualitySettings.SetQualityLevel(0);
                break;
            case 1:
                GraphicsSettings.renderPipelineAsset = Qualities[1];
                
                QualitySettings.SetQualityLevel(1);
                break;
            
            case 2:
                GraphicsSettings.renderPipelineAsset = Qualities[2];
                QualitySettings.SetQualityLevel(2);
                break;
        }
        _model.SaveGraphicChange(value);
    }
    
}
