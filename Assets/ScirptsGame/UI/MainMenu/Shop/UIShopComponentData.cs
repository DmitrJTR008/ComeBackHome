using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIShopComponentData : MonoBehaviour
{
    public PaidMaterialDataSO _componentData { get; private set; }
    [SerializeField] private Image _componentImageICO;
    [SerializeField] private Text _componentTextPrice;
    [SerializeField] public Button _componentButtonShow;

    public void InitComponent(PaidMaterialDataSO _paidProp)
    {
        _componentData = _paidProp;
        _componentImageICO.sprite = _componentData.ICO;
        _componentTextPrice.text = _componentData.Price.ToString();
    }
    
}
