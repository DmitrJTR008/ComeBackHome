using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGParalax : MonoBehaviour
{
    private RawImage raw;
    
    private void Awake()
    {
        raw = GetComponent<RawImage>();
    }

    void Update()
    {
        raw.uvRect = new Rect(raw.uvRect.position + new Vector2(0, 0.15f) * Time.deltaTime,raw.uvRect.size) ;
    }
}
