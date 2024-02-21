using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBackgroundRadioHandler : MonoBehaviour
{
    public static GameBackgroundRadioHandler Singleton;
    private AudioSource _as;

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            _as = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeVolume(float value)
    {
        _as.volume = value;
    }
}
