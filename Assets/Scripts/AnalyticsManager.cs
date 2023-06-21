using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public static class AnalyticsManager
{

    public static void StartGame()
    {
        Analytics.CustomEvent("OpenGame", new Dictionary<string, object>
        {
            {"CountOpenGame", PlayerData.instance.CountOpenGame}
        });



    }

    public static void Menu()
    {

        if (PlayerData.instance.highscores.Count != 0)
        {
            Analytics.CustomEvent("OpenMenu", new Dictionary<string, object>
            {
                {  "HighScore", PlayerData.instance.highscores[0]}
            });
        }


        Analytics.CustomEvent("OpenMenu", new Dictionary<string, object>
        {

            { "GamesCount", PlayerData.instance.highscores.Count}

        });





    }
public static void CompleteMission()
    {
        Analytics.CustomEvent("Missions", new Dictionary<string, object>
        {
            {"CompleteMission", 1}

        });

    }

    public static void RewardedAdClick()
    {
        Analytics.CustomEvent("AdForLife", new Dictionary<string, object>
        {
            {"WatchAdForLife", 1}

        });

    }

    public static void InterstitialAd()
    {
        Analytics.CustomEvent("InterstitialAd", new Dictionary<string, object>
        {
            {"InterstitialAd", 1}

        });

    }
}
