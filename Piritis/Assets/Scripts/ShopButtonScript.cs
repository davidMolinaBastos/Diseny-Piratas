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
                gc.ChangeGold(-exampleIsland.GetPriceOf(false, lvl));
                pc.AddNewRandomCard(lvl);
                gc.CallShopWindow();
                break;
            case TButton.BuyTreasure:
                gc.ChangeGold(-exampleIsland.GetPriceOf(true, lvl));
                gc.ChangePieces(1);
                gc.CallShopWindow();
                break;
        }
    }
}
