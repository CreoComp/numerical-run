﻿using UnityEngine;
using System;
using System.Collections;

public class ExtraLife : Consumable
{
    protected const int k_MaxLives = 3;
    protected const int k_CoinValue = 10;
    public static float duration_time;

    private void Awake()
    {
        duration = duration_time;
    }
    public override string GetConsumableName()
    {
        return "Life";
    }

    public override ConsumableType GetConsumableType()
    {
        return ConsumableType.EXTRALIFE;
    }

    public override int GetPrice()
    {
        return 2000;
    }


    public override IEnumerator Started(CharacterInputController c)
    {
        yield return base.Started(c);
        if (c.currentLife < k_MaxLives)
            c.currentLife += 1;
		//else
            //c.coins += k_CoinValue;
    }
}
