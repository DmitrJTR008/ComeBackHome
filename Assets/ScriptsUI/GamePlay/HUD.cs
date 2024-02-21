using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class HUD : MonoBehaviour
{

    protected enum OpenDirection
    {
        ToRight,
        ToLeft,
        ToDown,
        ToUp
    };
    protected enum OpenType
    {
        Smooth,
        Moment
    };

    protected static class HUDManager
    {
        public static HUD ActiveMenu;
        public static bool lockMenu;

    }
    [SerializeField] protected Sprite OpenSprite;
    [SerializeField] protected Sprite CloseSprite;
    [SerializeField] protected Transform HandleBtn;
    [SerializeField] protected OpenDirection openDirection;
    [SerializeField] protected OpenType openType;

    [SerializeField]protected bool isOpen;
    protected Vector2 positionOpen;
    protected Vector2 basePosition;
    [SerializeField]protected Vector2 targetDirection;
    [SerializeField]protected Canvas mainCanvas;
    [SerializeField]protected RectTransform rectObj;
    [SerializeField] protected HUD TargetHUDHandl;
    public bool isAlreadyOpen;
    
    [SerializeField] int ID;

    protected void InitBase()
    {
        targetDirection = Vector2.zero;
        mainCanvas = GetComponentInParent<Canvas>();
        rectObj = GetComponent<RectTransform>();

        basePosition = rectObj.anchoredPosition;

        Vector2 winSize = new Vector2(rectObj.rect.width, rectObj.rect.height);

        switch (openDirection)
        {
            case OpenDirection.ToRight:
                positionOpen = new Vector2(rectObj.anchoredPosition.x + winSize.x, rectObj.anchoredPosition.y);
                break;
            case OpenDirection.ToLeft:
                positionOpen = new Vector2(rectObj.anchoredPosition.x - winSize.x, rectObj.anchoredPosition.y);
                break;
            case OpenDirection.ToDown:
                positionOpen = new Vector2(rectObj.anchoredPosition.x, rectObj.anchoredPosition.y - winSize.y);
                break;
            case OpenDirection.ToUp:
                positionOpen = new Vector2(rectObj.anchoredPosition.x, rectObj.anchoredPosition.y + winSize.y);
                break;
        }
        HandleBtn = transform.Find("btnHandler");
    }
    public virtual void HandleMenu()
    {
        targetDirection = isOpen ? basePosition : positionOpen;
        if (HUDManager.ActiveMenu)
        {
            if (HUDManager.ActiveMenu.ID == ID && HUDManager.ActiveMenu.isOpen)
            {
                HUDManager.ActiveMenu = null;
                HandleBtn.GetComponent<Button>().image.sprite = OpenSprite;
            }
        }
        else
        {
            HUDManager.ActiveMenu = this;
            HandleBtn.GetComponent<Button>().image.sprite = CloseSprite;
        }
        
        transform.SetSiblingIndex(transform.parent.childCount-1);

        /*if (HUDManager.ActiveMenu)
        {
            //PopupMenuUIHandler[] list = new PopupMenuUIHandler[transform.parent.childCount];
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                PopupMenuUIHandler ui = transform.parent.GetChild(i).GetComponent<PopupMenuUIHandler>();
                if(ui.ID != ID)
                    ui.gameObject.SetActive(false);
                else
                    ui.transform.SetSiblingIndex(2);
            }
        }
        else
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                PopupMenuUIHandler ui = transform.parent.GetChild(i).GetComponent<PopupMenuUIHandler>();
                ui.gameObject.SetActive(true);
            }
        }*/
    }

}
