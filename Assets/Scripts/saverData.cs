using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.TextCore.Text;
using YG;

public class saverData : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Awake()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    public void Save()
    {
        YandexGame.savesData.Coins = PlayerData.instance.Coins;
        YandexGame.savesData.Fragments = PlayerData.instance.Fragments;
        YandexGame.savesData.rank = PlayerData.instance.rank;
        YandexGame.savesData.ftueLevel = PlayerData.instance.ftueLevel;

        YandexGame.savesData.characters = PlayerData.instance.characters;

        YandexGame.savesData.consumables = PlayerData.instance.consumables;

        YandexGame.SaveProgress();
    }

    public void Load() => YandexGame.LoadProgress();

    public void GetLoad()
    {
        PlayerData.instance.Coins = YandexGame.savesData.Coins;
        PlayerData.instance.Fragments = YandexGame.savesData.Fragments;
        PlayerData.instance.rank = YandexGame.savesData.rank;
        PlayerData.instance.ftueLevel = YandexGame.savesData.ftueLevel;

        PlayerData.instance.characters = YandexGame.savesData.characters;

        PlayerData.instance.consumables = YandexGame.savesData.consumables;
    }
}

