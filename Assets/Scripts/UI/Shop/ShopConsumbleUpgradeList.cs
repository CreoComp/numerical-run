using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

#if UNITY_ANALYTICS
using UnityEngine.Analytics;
#endif

public class ShopConsumbleUpgradeList : ShopList
{
    public override void Populate()
    {
        m_RefreshCallback = null;
        foreach (Transform t in listRoot)
        {
            RefreshButton(t.GetComponent<ShopItemListItem>());
        }
        
        
    }


    protected void RefreshButton(ShopItemListItem itm)
	{
		Boosters consumble = BoosterUpgrade.Instance.boosters[itm.index];

		itm.pricetext.text = consumble.cost[consumble.nowLevel] + "";


        if (!PlayerData.instance.isValidTransaction(consumble.cost[consumble.nowLevel]))
		{
			itm.buyButton.interactable = false;
			itm.pricetext.color = Color.red;
		}
		else
		{
			itm.pricetext.color = Color.black;
		}

		if (consumble.nowLevel >= consumble.cost.Count - 1)
		{
			itm.buyButton.interactable = false;
			itm.buyButton.image.sprite = itm.disabledButtonSprite;
			itm.buyButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Max Level";
		}
	}

    void Buy(ShopItemListItem itm)
    {
        BoosterUpgrade.Instance.UpgradeBooster(itm.index);
    }
}
