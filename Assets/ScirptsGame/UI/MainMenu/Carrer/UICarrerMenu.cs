using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UICarrerMenu : MonoBehaviour
{
    public Transform Root;
    public Transform ComponentPrefab;
    int totalLevel = 20;
    int offset = 1;
    public List<Button> ButtonList;
    public void InitCarrerUI(int levelComplete)
    {
        for(int i = 0 ; i < totalLevel; i++)
        {
            Transform clone = Instantiate(ComponentPrefab, Root.transform);
            Button sub = clone.GetChild(0).GetComponent<Button>();
            ButtonList.Add(sub);
            clone.GetChild(1).GetComponent<Text>().text = (i+1).ToString();
            if (i <= levelComplete)
            {
                sub.interactable = true;
            }
            else
                sub.interactable = false;
        }
        
       ComponentPrefab.gameObject.SetActive(false);
    }
    
}
