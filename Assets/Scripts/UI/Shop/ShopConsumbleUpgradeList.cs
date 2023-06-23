using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

#if UNITY_ANALYTICS
using UnityEngine.Analytics;
#endif

public class ShopConsumbleUpgradeList : ShopList
{
    private void OnEnable()
    {
        BoosterUpgrade.Upgrade += Populate;
    }
    private void OnDisable()
    {
        BoosterUpgrade.Upgrade -= Populate;

    }
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
		Boosters consumble = BoosterUpgrade.Instance.boosters[itm.gameObject.GetComponent<ConsumbleUpdateElement>().index - 1];




		if (consumble.nowLevel >= consumble.cost.Count)
		{

            itm.buyButton.interactable = false;
			itm.buyButton.image.sprite = itm.disabledButtonSprite;
			itm.buyButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Max Level";
            itm.pricetext.text = "";

		}
		else
		{
            itm.pricetext.text = consumble.cost[consumble.nowLevel] + "";


            if (!PlayerData.instance.isValidTransaction(-consumble.cost[consumble.nowLevel]))
            {
                itm.buyButton.interactable = false;
                itm.pricetext.color = Color.red;
            }
            else
            {
                itm.buyButton.interactable = true;

                itm.pricetext.color = Color.black;
            }

        }
    }
}
