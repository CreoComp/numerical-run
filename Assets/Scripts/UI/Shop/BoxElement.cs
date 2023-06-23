using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxElement : MonoBehaviour
{
    public int cost;
    public int AmounBoxes;


    private void Start()
    {
        gameObject.GetComponent<ShopItemListItem>().pricetext.text = cost + "";
    }
}
