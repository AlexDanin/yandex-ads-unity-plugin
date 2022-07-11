/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Android (C) 2019 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class YandexMobileAdsBannerDemoScript : MonoBehaviour
{
    private readonly float ButtonWidth = 0.8f * Screen.width;
    private readonly float ButtonHeight = 0.1f * Screen.height;
    private readonly float ColumnPosition = 0.1f * Screen.width;
    private readonly float RowFirstPosition = 0.05f * Screen.height;
    private readonly float RowSecondPosition = 0.225f * Screen.height;
    private readonly int FontSize = (int) (0.035f * Screen.width);

    private Banner banner;
    
    public void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        GUI.skin.button.fontSize = FontSize;

        Rect requestBannerRect = this.CreateButton(ColumnPosition, RowFirstPosition);
        if (GUI.Button(requestBannerRect, "Request\nBanner"))
        {
            this.RequestBanner();
        }

        Rect destroyBannerRect = this.CreateButton(ColumnPosition, RowSecondPosition);
        if (GUI.Button(destroyBannerRect, "Destroy\nBanner"))
        {
            this.banner.Destroy();
        }
    }

    private void RequestBanner()
    {
        // Replace demo R-M-DEMO-320x50 with actual Ad Unit ID
        // Following demo Block IDs may be used for testing:
        // R-M-DEMO-320x50
        // R-M-DEMO-320x100
        string adUnitId = "R-M-DEMO-320x100";

        if (this.banner != null)
        {
            this.banner.Destroy();
        }

        // Set flexible banner maximum width and height
        AdSize bannerMaxSize = AdSize.FlexibleSize(GetScreenWidthDp(), 100);
        // Or set sticky banner maximum width
        //AdSize bannerMaxSize = AdSize.StickySize(GetScreenWidthDp());
        this.banner = new Banner(adUnitId, bannerMaxSize, AdPosition.BottomCenter);

        this.banner.OnAdLoaded += this.HandleAdLoaded;
        this.banner.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.banner.OnReturnedToApplication += this.HandleReturnedToApplication;
        this.banner.OnLeftApplication += this.HandleLeftApplication;
        this.banner.OnAdClicked += this.HandleAdClicked;
        this.banner.OnImpression += this.HandleImpression;

        this.banner.LoadAd(this.CreateAdRequest());
    }
    
    // Example how to get screen width for request
    private int GetScreenWidthDp()
    {
        int screenWidth = (int)Screen.safeArea.width;
        return ScreenUtils.ConvertPixelsToDp(screenWidth);
    }

    private Rect CreateButton(float positionX, float positionY)
    {
        return new Rect(
            positionX,
            positionY,
            ButtonWidth,
            ButtonHeight);
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        this.banner.Show();
    }

    public void HandleAdFailedToLoad(object sender, AdFailureEventArgs args)
    {
        MonoBehaviour.print("HandleAdFailedToLoad event received with message: " + args.Message);
    }
    
    public void HandleLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleLeftApplication event received");
    }

    public void HandleReturnedToApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleReturnedToApplication event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

    public void HandleAdClicked(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClicked event received");
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
        MonoBehaviour.print("HandleImpression event received with data: " + data);
    }

    #endregion
}
