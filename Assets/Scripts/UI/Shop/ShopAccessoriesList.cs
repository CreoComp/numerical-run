﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

#if UNITY_ANALYTICS
using UnityEngine.Analytics;
#endif

public class ShopAccessoriesList : ShopList
{
    public AssetReference headerPrefab;

    List<Character> m_CharacterList = new List<Character>();
    public override void Populate()
    {
		m_RefreshCallback = null;

        foreach (Transform t in listRoot)
        {
            Destroy(t.gameObject);
        }

        m_CharacterList.Clear();
        foreach (KeyValuePair<string, Character> pair in CharacterDatabase.dictionary)
        {
            Character c = pair.Value;

            if (c.accessories !=null && c.accessories.Length > 0)
                m_CharacterList.Add(c);
        }

        headerPrefab.InstantiateAsync().Completed += (op) =>
        {
            LoadedCharacter(op, 0);
        };
    }

    void LoadedCharacter(AsyncOperationHandle<GameObject> op, int currentIndex)
    {
        if (op.Result == null || !(op.Result is GameObject))
        {
            Debug.LogWarning(string.Format("Unable to load header {0}.", headerPrefab.RuntimeKey));
        }
        else
        {
            Character c = m_CharacterList[currentIndex];

            GameObject header = op.Result;
            header.transform.SetParent(listRoot, false);
            ShopItemListItem itmHeader = header.GetComponent<ShopItemListItem>();
            itmHeader.nameText.text = c.characterName;

            prefabItem.InstantiateAsync().Completed += (innerOp) =>
            {
	            LoadedAccessory(innerOp, currentIndex, 0);
            };
        }
    }

    void LoadedAccessory(AsyncOperationHandle<GameObject> op, int characterIndex, int accessoryIndex)
    {
	    Character c = m_CharacterList[characterIndex];
	    if (op.Result == null || !(op.Result is GameObject))
	    {
		    Debug.LogWarning(string.Format("Unable to load shop accessory list {0}.", prefabItem.Asset.name));
	    }
	    else
	    {
		    CharacterAccessories accessory = c.accessories[accessoryIndex];

		    GameObject newEntry = op.Result;
		    newEntry.transform.SetParent(listRoot, false);

		    ShopItemListItem itm = newEntry.GetComponent<ShopItemListItem>();

		    string compoundName = c.characterName + ":" + accessory.accessoryName;

		    itm.nameText.text = accessory.accessoryName;
		    itm.pricetext.text = accessory.cost.ToString();
		    itm.icon.sprite = accessory.accessoryIcon;
		    itm.buyButton.image.sprite = itm.buyButtonSprite;

		    itm.buyButton.onClick.AddListener(delegate()
		    {
			    Buy(compoundName, accessory.cost);
		    });

		    m_RefreshCallback += delegate() { RefreshButton(itm, accessory, compoundName); };
		    RefreshButton(itm, accessory, compoundName);
	    }

	    accessoryIndex++;

	    if (accessoryIndex == c.accessories.Length)
	    {//we finish the current character accessory, load the next character

		    characterIndex++;
		    if (characterIndex < m_CharacterList.Count)
		    {
			    headerPrefab.InstantiateAsync().Completed += (innerOp) =>
			    {
				    LoadedCharacter(innerOp, characterIndex);
			    };
		    }
	    }
	    else
	    {
		    prefabItem.InstantiateAsync().Completed += (innerOp) =>
		    {
			    LoadedAccessory(innerOp, characterIndex, accessoryIndex);
		    };
	    }
    }

	protected void RefreshButton(ShopItemListItem itm, CharacterAccessories accessory, string compoundName)
	{
		if (PlayerData.instance.isValidTransaction(accessory.cost))
		{
			itm.buyButton.interactable = false;
			itm.pricetext.color = Color.red;
		}
		else
		{
			itm.pricetext.color = Color.black;
		}

		if (PlayerData.instance.characterAccessories.Contains(compoundName))
		{
			itm.buyButton.interactable = false;
			itm.buyButton.image.sprite = itm.disabledButtonSprite;
			itm.buyButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Owned";
		}
	}



	public void Buy(string name, int cost)
    {
        PlayerData.instance.AddCoins(-cost);
		PlayerData.instance.AddAccessory(name);

		Refresh();
    }
}
