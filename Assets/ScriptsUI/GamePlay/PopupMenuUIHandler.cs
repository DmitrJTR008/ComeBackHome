using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PopupMenuUIHandler : HUD
{
    public float currentTransition;
    public float openSpeed;
    Coroutine handleCoroutine;
    void Start()
    {
        openSpeed = 1500f;
        InitBase();
    }

    public override void HandleMenu()
    {
        base.HandleMenu();
        if (HUDManager.ActiveMenu)
            if (HUDManager.ActiveMenu != this) 
                return;

        switch (openType)
        {
            case OpenType.Smooth:
                HandleSmooth();
                break;
            case OpenType.Moment:
                HandleMoment();
                break;
        }
    }
    void HandleMoment()
    {
        rectObj.anchoredPosition = targetDirection;
        isOpen = !isOpen;

    }
    void HandleSmooth()
    {
       if (handleCoroutine != null) return;
        handleCoroutine = StartCoroutine(MenuTransition());
    }


    IEnumerator MenuTransition()
    {

        while (rectObj.anchoredPosition != targetDirection)
        {
            rectObj.anchoredPosition = Vector2.MoveTowards(rectObj.anchoredPosition, targetDirection, openSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        isOpen = !isOpen;
        handleCoroutine = null;
    }
}
