using System;
using System.Collections;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class InterstitialAdController : MonoBehaviour
{
    InterstitialAd interstitialAd;

    public static InterstitialAdController instance;
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Destroying duplicate InterstitialAd object - only one is allowed per scene!");
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
        
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
            LoadInterstitialAd();
        }); 
    }
    
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += this.ShowInterstitalAdOnStart;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= this.ShowInterstitalAdOnStart;
    }

    private void ShowInterstitalAdOnStart(Scene from, Scene to)
    {
        if(Globals.instance.removedAd) return;
        if(to.name == "LoadingScene") return;
        Globals.instance.levelPlayedPerSesion++;
        if(Globals.instance.levelPlayedPerSesion <= 1) return;
        if(to.name == "TutorialScene") return;
        StartCoroutine(ShowAd());
    }

    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(Keys.interstitialId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogWarning("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
                RegisterEventHandlers(interstitialAd);
            });
    }
    
    [ContextMenu("ShowInterstitialAd")]
    private IEnumerator ShowAd()
    {
        yield return new WaitForSeconds(.55f);
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            GAEvents.ShowInterstitialAd();
            interstitialAd.Show();
        }
        else
        {
            Debug.LogWarning("Interstitial ad is not ready yet.");
        }
    }
    
    private void RegisterEventHandlers(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
            LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogWarning("Interstitial ad failed to open full screen content " +
                             "with error : " + error);
            LoadInterstitialAd();
        };
    }
}
