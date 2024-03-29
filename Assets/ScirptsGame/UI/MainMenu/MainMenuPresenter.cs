using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
public class MainMenuPresenter
{
    public MainMenuView _view { get; private set; }
    public MainMenuModel _model { get; private set; }
    private List<UniversalRenderPipelineAsset> Qualities;
    private AdsInitializer AdsObject;
    private int nextScene;
    
    public MainMenuPresenter(MainMenuView view, List <UniversalRenderPipelineAsset> qualit, AdsInitializer adsObject)
    {
        _view = view;
        Qualities = qualit;
        _model = new MainMenuModel(this, _view);
        AdsObject = adsObject;
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

    public void ShowRewardAdd()
    {
        AdsObject.rew.ShowAd();
    }

    public void GetReward()
    {
        Debug.Log("CALL");
        _model.SaveMoney(500);
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
        if(nextScene < 1)
        {
            AdsObject.ShowBanner();
        }
        else
        {
            AdsObject.CloseBanner();
        }
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
