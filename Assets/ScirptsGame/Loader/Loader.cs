using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    // Start is called before the first frame update
    
    List<string> phraseRU = new List<string>() {
        "ПРОГРАММИРУЕМ РАКЕТУ",
        "ВИЗУАЛИЗИРУЕМ МИРЫ",
        "ПОЧТИ ГОТОВО",
        "ХОРОШЕГО ПОЛЕТА"
    };

    List<string> phraseEN = new List<string>() {
        "PROGRAMMING THE ROCKET",
        "VISUALIZING WORLDS",
        "ALMOST READY",
        "HAVE A GREAT FLIGHT"
    };

    List<string> phraseTR = new List<string>() {
        "ROKETİ PROGRAMLIYORUZ",
        "DÜNYALARI GÖRÜNTÜLÜYORUZ",
        "Neredeyse Hazır",
        "İYİ UÇUŞLAR"
    };
    
    public Slider LoaderSlider;
    public Text TimeText;
    public Text RU, EN, TR;
    void Start()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        float sec = 0f;
        while (sec < 4)
        {
            sec += Time.deltaTime;
            int x = Mathf.RoundToInt(sec / 2);
            UpdateText(x);
            Debug.Log(x);
            LoaderSlider.value = sec / 4;
            TimeText.text = (LoaderSlider.value * 100f).ToString("00");
            yield return null;
        }
        SceneManager.LoadScene(1);
    }

    void UpdateText(int id)
    {
        RU.text = phraseRU[id];
        EN.text = phraseEN[id];
        TR.text = phraseTR[id];
    }
}
