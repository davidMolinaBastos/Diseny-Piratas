using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonScript : MonoBehaviour
{
    public enum TButton { Close, BuyCard, BuyTreasure }
    public TButton ButtonType = TButton.Close;

    [Range(1, 5)] public int lvl;
    public IslandScript exampleIsland;

    GameController gc;
    PlayerController pc;
    public void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (ButtonType == TButton.Close || ButtonType == TButton.BuyTreasure)
            return;
        else
            pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void OnClick()
    {
        switch (ButtonType)
        {
            case TButton.Close:
                gc.CloseShopWindow();
                break;
            case TButton.BuyCard:
                if (!gc.CanBuy(0))
                    break;
                if (!gc.CanBuy(exampleIsland.GetPriceOf(false, lvl)))
                    break;
                gc.ChangeGold(-exampleIsland.GetPriceOf(false, lvl));
                pc.AddNewRandomCard(lvl);
                gc.CloseShopWindow();
                gc.CallShopWindow();
                break;
            case TButton.BuyTreasure:
                if (!gc.CanBuy(0))
                    break;
                if (!gc.CanBuy(exampleIsland.GetPriceOf(true, lvl)))
                    break;
                gc.ChangeGold(-exampleIsland.GetPriceOf(true, lvl));
                gc.ChangePieces(1);
                gc.CloseShopWindow();
                gc.CallShopWindow();
                break;
        }
    }
}
