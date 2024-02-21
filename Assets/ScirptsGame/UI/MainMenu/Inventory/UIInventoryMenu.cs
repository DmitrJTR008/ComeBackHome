using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryMenu : MonoBehaviour
{
    [SerializeField] private GameObject ShopRoot;
    private Transform ShopComponentPrefab;
    public List<Button> ActiveButtonList;
    
    private void Awake()
    {
        ShopComponentPrefab = ShopRoot.transform.GetChild(0);
        ShopComponentPrefab.gameObject.SetActive(false);
    }

    public void AddNewComponent(PaidMaterialDataSO data)
    {
        Transform clone = Instantiate(ShopComponentPrefab, ShopRoot.transform);
        clone.gameObject.SetActive(true);
        UIInventoryComponentData dataComponentClone = clone.GetComponent<UIInventoryComponentData>();
        ActiveButtonList.Add(dataComponentClone._componentButtonShow);
        dataComponentClone.InitComponent(data);
    }
}
