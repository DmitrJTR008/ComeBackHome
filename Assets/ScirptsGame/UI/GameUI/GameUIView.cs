using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameUIView : MonoBehaviour
{
    public Transform HandlerMobile;
    public GameUIPresenter _presenter { get; private set; }
    public Action OnMvpInit;

    [SerializeField] private TimerComponentUI _timerUIComponent;
    [SerializeField] private MiniUIMenu _miniMenu;
    private void Awake()
    {
        HandlerMobile = transform.GetChild(0);
        HandlerMobile.gameObject.SetActive(false);
        if (YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet)
        {
            HandlerMobile.gameObject.SetActive(true);
        }
        _presenter = new GameUIPresenter(this);
        OnMvpInit?.Invoke();
    }

    private void OnEnable()
    {
        _miniMenu.BtnRestart.onClick.AddListener(()=>_presenter.RestartGame());
        _miniMenu.BtnHome.onClick.AddListener(()=>_presenter.GoHome());
    }


    public void UpdateUITimer(string label, float time)
    {
        _timerUIComponent.gameObject.SetActive(true);
        _timerUIComponent.RootText.text = label;
        _timerUIComponent.CountText.text = time.ToString("0");
    }

    public void ShutDownTimer()
    {
        _timerUIComponent.CountText.enabled = false;
    }

    public void UpdateRewardText(string text)
    {
        _timerUIComponent.RewardText.text = text;
    }
    

}
