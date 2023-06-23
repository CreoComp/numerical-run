using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif
#if UNITY_ANALYTICS
using UnityEngine.Analytics;
#endif

public class ShopUI : MonoBehaviour
{
    public ConsumableDatabase consumableDatabase;

    public ShopItemList itemList;
    public ShopCharacterList characterList;
    public ShopBoxesList accessoriesList;
    public ShopConsumbleUpgradeList ShopConsumbleUpgradeList;

    [Header("UI")]
    public Text coinCounter;
    public Button cheatButton;

    protected ShopList m_OpenList;

    protected const int k_CheatCoins = 1000000;

    public void UpgradeConsumble(int index)
    {
        BoosterUpgrade.Instance.UpgradeBooster(index);
    }

	void Start ()
    {

        PlayerData.Create();

        consumableDatabase.Load();
        CoroutineHandler.StartStaticCoroutine(CharacterDatabase.LoadDatabase());
        CoroutineHandler.StartStaticCoroutine(ThemeDatabase.LoadDatabase());



#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
        //Disable cheating on non dev build outside of the editor
        cheatButton.interactable = false;
#else
        cheatButton.interactable = true;
#endif

        m_OpenList = itemList;
        itemList.Open();
	}
	
	void Update ()
    {
        coinCounter.text = PlayerData.instance.Coins.ToString();
    }

    public void OpenItemList()
    {
        m_OpenList.Close();
        itemList.Open();
        m_OpenList = itemList;
    }

    public void OpenCharacterList()
    {
        m_OpenList.Close();
        characterList.Open();
        m_OpenList = characterList;
    }

    public void OpenThemeList()
    {
        m_OpenList.Close();
        ShopConsumbleUpgradeList.Open();
        m_OpenList = ShopConsumbleUpgradeList;
    }

    public void OpenAccessoriesList()
    {
        m_OpenList.Close();
        accessoriesList.Open();
        m_OpenList = accessoriesList;
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

	public void CloseScene()
	{
        SceneManager.UnloadSceneAsync("shop");
	    LoadoutState loadoutState = GameManager.instance.topState as LoadoutState;
	    if(loadoutState != null)
        {
            loadoutState.Refresh();
        }
	}

	public void CheatCoin()
	{
#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
        return ; //you can't cheat in production build
#endif

        PlayerData.instance.AddCoins(k_CheatCoins);
        PlayerData.instance.AddFragments(k_CheatCoins);
	}

}
