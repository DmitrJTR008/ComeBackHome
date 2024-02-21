using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageHandler : MonoBehaviour
{
    public List<Text> GameText;
    public List<string> RULang, ENLang, TRLang;

    public void ChangeLang(int index)
    {
        List<string> TargetLang;
        switch (index)
        {
            case 0:
                TargetLang = RULang;
                break;
            case 1:
                TargetLang = ENLang;
                break;
            case 2:
                TargetLang = TRLang;
                break;
            default:
                TargetLang = RULang;
                break;
        }

        int count = 0;
        GameText.ForEach(x => { x.text = TargetLang[count]; count++; });
    }
}
    
