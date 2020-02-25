using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtonScript : MonoBehaviour
{
    public enum TButton { INV, REV, HAND, LVLSELECT, DECK }
    public TButton Type;

    public int HandOrder;
    public int LvlSelect;

    public CartaObject AssociatedCard;
    GameController gc;
    CardBlackboard cb;
    MenuController mc;
    PlayerController pc;

    public void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        cb = gc.gameObject.GetComponent<CardBlackboard>();
        mc = gc.gameObject.GetComponent<MenuController>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (Type == TButton.HAND)
            AssociatedCard = pc.GetHand()[HandOrder];
    }

    public void OnClick()
    {
        switch (Type)
        {
            case TButton.INV:
                if (gc.inventory)
                    gc.CloseInventory(1, 0);
                else
                    gc.CallInventory(1, 0);
                break;
            case TButton.REV:
                if (gc.invCase != 1)
                    gc.CallInventory(gc.invCase - 1, 0);
                else
                    gc.CloseInventory(1, 0);
                break;
            case TButton.HAND:
                gc.CallInventory(2, 0);
                mc.SelectedHandCard = pc.GetHand()[HandOrder];
                break;
            case TButton.LVLSELECT:
                gc.CallInventory(3, LvlSelect);
                break;
            case TButton.DECK:
                gc.CallInventory(1, 0);
                mc.SelectedDeckCard = AssociatedCard;
                pc.SwitchCards(mc.SelectedHandCard, mc.SelectedDeckCard, HandOrder);
                break;
        }
    }
}
