using UnityEngine;
using System;
using System.Collections;

public class Score2Multiplier : Consumable
{
    private void OnEnable()
    {
        BoosterUpgrade.UpgradeConsumableDuration += ChangeDura;
    }
    private void OnDisable()
    {
        BoosterUpgrade.UpgradeConsumableDuration -= ChangeDura;

    }

    void ChangeDura(float dura, int _index)
    {
        if(_index == 2)
        duration = dura;
    }
    public override string GetConsumableName()
    {
        return "x2";
    }

    public override ConsumableType GetConsumableType()
    {
        return ConsumableType.SCORE_MULTIPLAYER;
    }

    public override int GetPrice()
    {
        return 750;
    }

	public override IEnumerator Started(CharacterInputController c)
    {
        yield return base.Started(c);

        m_SinceStart = 0;

        c.trackManager.modifyMultiply += MultiplyModify;
    }

    public override void Ended(CharacterInputController c)
    {
        base.Ended(c);

        c.trackManager.modifyMultiply -= MultiplyModify;
    }

    protected int MultiplyModify(int multi)
    {
        return multi * 2;
    }
}
