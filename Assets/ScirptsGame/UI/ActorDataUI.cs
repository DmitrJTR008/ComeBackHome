using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorDataUI : MonoBehaviour
{
    public Text MoneyText;
    

    public void UpdateActorDataUI(int money)
    {
        MoneyText.text = money.ToString("0000000");
    }
}
