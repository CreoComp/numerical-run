using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class BoosterUpgrade : MonoBehaviour
{
    public static Action Upgrade;
    public static Action<float, int> UpgradeConsumableDuration;
    public static Action<int, int, int> UpgradeUI;

    public List<Boosters> boosters = new List<Boosters>();


    public static BoosterUpgrade Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpgradeBooster(int index)
    {
        Boosters consumble = boosters[index];


        int cost = consumble.cost[consumble.nowLevel];

        if (PlayerData.instance.isValidTransaction(-cost))
        {
            PlayerData.instance.AddCoins(-cost);


            consumble.nowLevel++; 

            Upgrade?.Invoke();
            if (consumble.nowLevel <= consumble.cost.Count - 1)
            UpgradeUI?.Invoke(consumble.index + 1, consumble.nowLevel, consumble.cost[consumble.nowLevel]);
        }


    }
}

[System.Serializable]
public class Boosters
{
    public string name;
    public int index;
    public List<int> cost;
    public int nowLevel;
    public List<int> duration;

}