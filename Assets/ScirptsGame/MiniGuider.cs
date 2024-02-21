using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class MiniGuider : MonoBehaviour
{
    public RocketController rocket;
    
    public GameObject PanelGuide;
    public GameObject PanelDesktop;
    private string prefs = "guidegame";
    private string prefsDesktop = "gidegamedesk";
    private void Awake()
    {
        if (YandexGame.EnvironmentData.isDesktop)
        {
            ChoiseTarget(prefsDesktop, PanelDesktop);
        }
        else if (YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet)
        {
            ChoiseTarget(prefs,PanelGuide);
        }
        
    }

    void ChoiseTarget(string targetPrefs, GameObject target)
    {
        if (!PlayerPrefs.HasKey(targetPrefs))
        {
            PlayerPrefs.SetInt(targetPrefs,0);
        }

        if (PlayerPrefs.GetInt(targetPrefs) == 0)
        {
            target.SetActive(true);
            
            rocket.enabled = false;
        }
    }

    public void CloseGuide(int  id)
    {
        switch (id)
        {
            case 0:
                
                PlayerPrefs.SetInt(prefsDesktop,1);
                PlayerPrefs.Save();
                PanelDesktop.SetActive(false);
                break;
            case 1:
                PlayerPrefs.SetInt(prefs,1);
                PlayerPrefs.Save();
                PanelGuide.SetActive(false);
                break;
        }
        
        rocket.enabled = true;
    }
}
