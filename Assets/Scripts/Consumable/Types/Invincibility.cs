using UnityEngine;
using System;
using System.Collections;

public class Invincibility : Consumable
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
        if (_index == 3)
            duration = dura;
    }
    public override string GetConsumableName()
    {
        return "Invincible";
    }

    public override ConsumableType GetConsumableType()
    {
        return ConsumableType.INVINCIBILITY;
    }

    public override int GetPrice()
    {
        return 1500;
    }

	public override void Tick(CharacterInputController c)
    {
        base.Tick(c);

        c.characterCollider.SetInvincibleExplicit(true);
    }

    public override IEnumerator Started(CharacterInputController c)
    {
        yield return base.Started(c);
        c.characterCollider.SetInvincible(duration);
    }

    public override void Ended(CharacterInputController c)
    {
        base.Ended(c);
        c.characterCollider.SetInvincibleExplicit(false);
    }
}
