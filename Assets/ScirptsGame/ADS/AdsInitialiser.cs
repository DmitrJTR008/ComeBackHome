using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    //public static  AdsInitializer Instance;
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;
    public RewardedAd rew;
    public InterstitialAdExample interstitial;
    public BannerAd bannerAd;

    void Awake()
    {
        /*if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);*/

        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
            _gameId = _androidGameId;
#elif UNITY_EDITOR
        _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
            Debug.Log("Init first");
            
        }
        else if(Advertisement.isInitialized && Advertisement.isSupported)
        {
            Debug.Log("InitAgain");
            OnInitializationComplete();
        }
    }


    public void OnInitializationComplete()
    {
        if (rew != null)
            rew.StartReward();
        interstitial.StartReward();
        if(bannerAd != null)
            bannerAd.StartReward();
        if(SceneManager.GetActiveScene().buildIndex > 0 && !SpeedRunTimer.Instance.isActivate)
        {
            ShowFullScreen();
        }
       
        
    }

    public void CloseBanner()
    {
        bannerAd.HideBannerAd();
    }

    public void ShowBanner()
    {
        bannerAd.ShowBannerAd();
    }

    public void ShowFullScreen()
    {
        interstitial.ShowAd();
    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}