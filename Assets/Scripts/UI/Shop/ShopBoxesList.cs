using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;

#if UNITY_ANALYTICS
using UnityEngine.Analytics;
#endif

public class ShopBoxesList : ShopList
{
    public AssetReference headerPrefab;

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
		BoxElement box = itm.gameObject.GetComponent<BoxElement>();
		if (!PlayerData.instance.isValidTransaction(-box.cost))
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

    public void BuyBox(BoxElement box)
	{
        if (PlayerData.instance.isValidTransaction(-box.cost))
		{
            PlayerData.instance.AddCoins(-box.cost);
			BoxManager.openBoxCount = box.AmounBoxes;
			Populate();
			SceneManager.LoadScene("LootBox");
		}
	}

}
