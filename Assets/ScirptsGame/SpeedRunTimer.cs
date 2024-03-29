using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpeedRunTimer : MonoBehaviour
{
    private static SpeedRunTimer instance;
    public static SpeedRunTimer Instance
    {
        get { return instance; }
    }

    private Text timerText;
    private float elapsedTime = 0f;
    private bool isPaused = false;

    public bool isActivate;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            timerText = GameObject.Find("SpeedRunTextTimer").GetComponent<Text>();
            UpdateTimerText();
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex < 1) return;
        if (timerText == null)
        {
            
                timerText = GameObject.Find("SpeedRunTextTimer").GetComponent<Text>();
                if (isActivate)
                {
                    timerText.gameObject.SetActive(true);
                }
                else
                {
                    timerText.gameObject.SetActive(false);
                }
        }
        else if (!isPaused && isActivate)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
    }

    public void StartTimer()
    {
        isPaused = false;
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerText();
    }

    public float GetElapsedMinutes()
    {
        return elapsedTime / 60f;
    }

    private void UpdateTimerText()
    {
        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime - hours * 3600f) / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime - hours * 3600f - minutes * 60f);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}