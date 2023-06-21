using UnityEngine;

// Добавьте библиотеку GamePush
using GamePush;

public class AdsManager : MonoBehaviour
{
    public float timeToShow = 6;
    float time;

    private void Update()
    {
        if (time <= timeToShow)
            time += Time.deltaTime;
        else
        {
            AnalyticsManager.InterstitialAd();

            time = 0;
            GP_Ads.ShowFullscreen();
        }
    }



}