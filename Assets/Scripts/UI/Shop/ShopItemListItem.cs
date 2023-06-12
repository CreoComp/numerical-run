using UnityEngine;
using UnityEngine.UI;

public class ShopItemListItem : MonoBehaviour
{
    public int index;
    public Image icon;
    public Text nameText;
    public Text pricetext;
    public Button buyButton;

	public Text countText;

	public Sprite buyButtonSprite;
	public Sprite disabledButtonSprite;

    public Text Level;


    private void OnEnable()
    {
        BoosterUpgrade.UpgradeUI += UpgradeConsumble;
    }

    private void OnDisable()
    {
        BoosterUpgrade.UpgradeUI -= UpgradeConsumble;

    }
    private void Start()
    {
        if (Application.systemLanguage == SystemLanguage.Russian)
            Level.text = "Уровень " + (BoosterUpgrade.Instance.boosters[index].nowLevel + 1);
        else
            Level.text = "Level " + (BoosterUpgrade.Instance.boosters[index].nowLevel + 1);

        pricetext.text = BoosterUpgrade.Instance.boosters[index].cost[BoosterUpgrade.Instance.boosters[index].nowLevel] + "";
    }
    void UpgradeConsumble(int INDEX, int nowLevel, int cost)
    {
        if (INDEX == index)
        {


            if (Application.systemLanguage == SystemLanguage.Russian)
                Level.text = "Уровень " + (nowLevel + 1);
            else
                Level.text = "Level " + (nowLevel + 1);

            pricetext.text = cost + "";

        }
    }
}
