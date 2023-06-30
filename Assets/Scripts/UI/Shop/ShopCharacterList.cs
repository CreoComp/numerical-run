using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AddressableAssets;

#if UNITY_ANALYTICS
using UnityEngine.Analytics;
#endif

public class ShopCharacterList : ShopList
{
    public override void Populate()
    {
		m_RefreshCallback = null;
        foreach (Transform t in listRoot)
        {
            Destroy(t.gameObject);
        }

        var sortedDictionary =
	        CharacterDatabase.dictionary.OrderBy(c => c.Value.costInFragments);
        
        foreach(KeyValuePair<string, Character> pair in sortedDictionary)
        {
            Character c = pair.Value;
            if (c != null)
            {
                prefabItem.InstantiateAsync().Completed += (op) =>
                {
                    if (op.Result == null || !(op.Result is GameObject))
                    {
                        Debug.LogWarning(string.Format("Unable to load character shop list {0}.", prefabItem.Asset.name));
                        return;
                    }
                    GameObject newEntry = op.Result;
                    newEntry.transform.SetParent(listRoot, false);

                    ShopItemListItem itm = newEntry.GetComponent<ShopItemListItem>();
                    
                    itm.icon.sprite = c.icon;
                    itm.nameText.text = c.characterName;
                    itm.pricetext.text = c.costInFragments.ToString();

                    itm.buyButton.image.sprite = itm.buyButtonSprite;


                    itm.buyButton.onClick.AddListener(delegate() { Buy(c); });

                    m_RefreshCallback += delegate() { RefreshButton(itm, c); };
                    RefreshButton(itm, c);
                };
            }
        }
    }

	protected void RefreshButton(ShopItemListItem itm, Character c)
	{
		if (!PlayerData.instance.IsValidTransactionInFragments(-c.costInFragments))
		{
			itm.buyButton.interactable = false;
			itm.pricetext.color = Color.red;
		}
		else
		{
			itm.pricetext.color = Color.black;
		}


		if (PlayerData.instance.characters.Contains(c.characterName))
		{
			itm.buyButton.interactable = false;
			itm.buyButton.image.sprite = itm.disabledButtonSprite;
			itm.buyButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Owned";
		}
	}



	public void Buy(Character c)
    {
        PlayerData.instance.AddFragments(-c.costInFragments);
        PlayerData.instance.AddCharacter(c.characterName);
        // Repopulate to change button accordingly.
        Populate();
    }
}
