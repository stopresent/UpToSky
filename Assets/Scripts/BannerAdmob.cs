using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAdmob : MonoBehaviour
{
    private BannerView bannerView;
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStaus => { });

        this.RequestBanner();
    }
    private void RequestBanner()
    {
        // 안드로이드 광고 id??
        // ca-app-pub-1536666317304059~7180163006

        //string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        string adUnitId = "ca-app-pub-1536666317304059/4696663337";

        // Clean up banner ad before creating a new one
        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }

        AdSize adpatSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        this.bannerView = new BannerView(adUnitId, adpatSize, AdPosition.Bottom);

        // Create an empty ad request
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request
        this.bannerView.LoadAd(request);
    }
}