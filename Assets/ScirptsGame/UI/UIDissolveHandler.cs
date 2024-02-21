using System.Collections;
using UnityEngine;

public class UIDissolveHandler : MonoBehaviour
{
    public System.Action OnEffectOpen;
    public System.Action OnEffectClose;

    private Material dissolveShader;

    private void Awake()
    {
        dissolveShader = GetComponent<MeshRenderer>().material;
    }

    public void StartDissolve(bool toOpen)
    {
        StartCoroutine(DissolveHandler(toOpen));
    }

    IEnumerator DissolveHandler(bool toOpen)
    {
        float targetValue = toOpen ? 1f : 0f;
        float countTime = 0;
        float startValue = dissolveShader.GetFloat("_Dissolve");

        while (countTime < 1f)
        {
            countTime += Time.deltaTime;
            float value = Mathf.Lerp(startValue, targetValue, countTime );
            dissolveShader.SetFloat("_Dissolve", value);
            yield return null;
        }
        
        dissolveShader.SetFloat("_Dissolve", targetValue);

        if (toOpen)
            OnEffectOpen?.Invoke();
        else 
            OnEffectClose?.Invoke();
        
    }
}