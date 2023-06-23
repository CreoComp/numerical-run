using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoxManager : MonoBehaviour
{
    public static int openBoxCount = 30;

    public bool isWaitToOpen = true;
    public bool isWaitToCloseBox = true;

    [SerializeField] Animator boxAnim;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject ImageTap;

    [SerializeField] Image SecondItemImage;  
    [SerializeField] Sprite[] SecondItemSprite;


    [SerializeField] TextMeshProUGUI Coins;
    [SerializeField] TextMeshProUGUI Fragments;
    [SerializeField] TextMeshProUGUI AmountSecondItemText;


    void Update()
    {

            DetectTouch();
    }

    private void DetectTouch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Touch();

        }
        if (Input.touchCount > 0)
        {
            Touch();

        }

    }

    void Touch()
    {
        if (isWaitToOpen)
            Open();
        else if (isWaitToCloseBox)
            CloseBox();
    }


    private void Open()
    {
        isWaitToCloseBox = false;

        isWaitToOpen = false;
        ImageTap.SetActive(false);
        openBoxCount--;
        StartCoroutine(StartOpen());

        int AmountStars = Random.Range(10, 35);
        int AmountFragments = Random.Range(1, 10);
        int SecondItem = Random.Range(1, SecondItemSprite.Length);
        int AmountSecondItem = 1;

        Fragments.text = AmountFragments + "";
        SecondItemImage.sprite = SecondItemSprite[SecondItem];
        Coins.text = AmountStars + "";
        AmountSecondItemText.text = AmountSecondItem + "";


        PlayerData.instance.AddCoins(AmountStars);

        switch(SecondItem)
        {
            case 0:
                PlayerData.instance.Add(Consumable.ConsumableType.COIN_MAG);

                break;

            case 1:
                PlayerData.instance.Add(Consumable.ConsumableType.INVINCIBILITY);

                break;

            case 2:
                PlayerData.instance.Add(Consumable.ConsumableType.SCORE_MULTIPLAYER);

                break;
        }



        PlayerData.instance.AddCoins(AmountStars);
        PlayerData.instance.AddFragments(AmountFragments);
    }

    IEnumerator StartOpen()
    {
        boxAnim.SetTrigger("open");
        yield return new WaitForSeconds(2f); // поставимть время анимации
        panel.SetActive(true);
        panel.GetComponent<Animator>().SetTrigger("open");
        yield return new WaitForSeconds(2f);
        isWaitToCloseBox = true;
    }

    void CloseBox()
    {
        if (openBoxCount <= 0)
        {
            SceneManager.LoadScene("Shop");
        }
        ImageTap.SetActive(true);
        isWaitToOpen = true;
        boxAnim.SetTrigger("close");

        isWaitToCloseBox = false;

        panel.SetActive(false);

    }

}
