using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerComponentUI : MonoBehaviour
{
    public Text RootText, CountText, RewardText;
    public void UpdateText(string label, float timeCount)
    {
        RootText.text = label;
        CountText.text = timeCount.ToString("0");
    }
}
