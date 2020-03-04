using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CardBlackboard))]
[RequireComponent(typeof(MenuController))]
public class GameController : MonoBehaviour
{
    List<IRestartGameElement> m_ResetNodes = new List<IRestartGameElement>();

    CardBlackboard cb;
    MenuController mc;
    BattleManager bm;
    PlayerController pc;
    EventNodeScript evento;

    public float gold = 0, treasureParts = 0;
    bool eventActive;
    [HideInInspector] public bool inventory;
    [HideInInspector] public int invCase;
    BattleManager.TResults[] results;
    int[] playerRolls;
    [HideInInspector] public float fightCounter = 5;
    // PORT ROYAL = 0, TORTUGA = 1, NEW PROVIDENCE = 2, 
    public Transform[] Islas = new Transform[6];

    //Default Methods
    private void Start()
    {
        cb = GetComponent<CardBlackboard>();
        mc = GetComponent<MenuController>();
        bm = GetComponent<BattleManager>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (eventActive && evento.GetEventType() == EventNodeScript.TEvent.FIGHT && fightCounter > 0)
            fightCounter -= Time.deltaTime;

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.T)) RefillNodes();  //DebugRefill
        if (Input.GetKeyDown(KeyCode.Y)) gold += 100f; //DebugAddMoney
#endif
        if (eventActive)
        {
            /*
            SE TIENE QUE CAMBIAR, y se ha cambiao
            */
            if (evento.GetEventType() == EventNodeScript.TEvent.FIGHT && fightCounter <= 0)//Input.anyKeyDown && evento.GetEventType() == EventNodeScript.TEvent.FIGHT)
            {
                mc.DisplayFight(false, null, pc.GetHand());
                eventActive = false;
                pc.SetMoving(true);
                evento = null;
                DeleteCards();
                bm.FlushValues();
            }
            else if (evento.GetEventType() != EventNodeScript.TEvent.FIGHT && Input.anyKeyDown)
            {
                mc.DisplayEvent(false, null);
                eventActive = false;
                pc.SetMoving(true);
                evento = null;
            }
        }
    }

    //InventoryManager
    public void CallInventory(int Case, int Lvl)
    {
        inventory = true;
        invCase = Case;
        mc.DisplayInventory(true, pc, Case, Lvl);
        pc.SetMoving(false);
    }
    public void CloseInventory(int Case, int Lvl)
    {
        inventory = false;
        invCase = Case;
        mc.DisplayInventory(false, pc, Case, Lvl);
        pc.SetMoving(true);
    }

    //Shop Managment
    public void CallShopWindow()
    {
        mc.DisplayShop(true, gold, treasureParts);
        pc.SetMoving(false);
        RefillNodes();
    }
    public void CloseShopWindow()
    {
        mc.DisplayShop(false, gold, treasureParts);
        pc.SetMoving(true);
    }
    public bool CanBuy(float value)
    {
        if (value > gold)
            return false;
        return true;
    }
    public void ChangeGold(float value) { gold += value; }
    public void ChangePieces(int value) { treasureParts += value; }

    //Event Managment
    public void CallEvent(EventNodeScript e)
    {
        if (e.GetDeplete())
            return;
        if (e.GetEventType() == EventNodeScript.TEvent.FIGHT)
        {
            PerformEvent(e);
            mc.DisplayFight(true, e.pirateHand, pc.GetHand());
            eventActive = true;
            pc.SetMoving(false);
            evento = e;
            e.Deplete();
        }
        else
        {
            mc.DisplayEvent(true, e);
            eventActive = true;
            pc.SetMoving(false);
            evento = e;
            e.Deplete();
            PerformEvent(e);
        }
    }
    void PerformEvent(EventNodeScript e)
    {
        switch (e.GetEventType())
        {
            case EventNodeScript.TEvent.FIGHT:
                bm.Battle(pc.GetHand(), e.pirateHand);
                gold += bm.GetWinnings();
                playerRolls = bm.ReturnPlayerRolls();
                foreach (int i in playerRolls) print("Rolled:" + i);
                results = bm.ReturnResults();
                foreach (BattleManager.TResults r in results) print("Result:" + r);
                break;
            case EventNodeScript.TEvent.CHANGE_GOLD:
                e.ChangeGoldEffect();
                break;
            case EventNodeScript.TEvent.CHANGE_CARD:
                e.ChangeCardEffect();
                break;
            case EventNodeScript.TEvent.CHANGE_BOTH:
                e.ChangeBothEffect();
                break;
            case EventNodeScript.TEvent.TELEPORT:
                e.TeleportEffect();
                break;
        }
    }

    public int[] ReturnPlayerRolls() { return playerRolls; }

    public void DeleteCards()
    {
        for (int i = 0; i < 2; i++)
            if (results[i] == BattleManager.TResults.Loose)
                pc.RemoveCardFromHand(i);
    }



    //Restarts and refills
    public void AddResetElement(IRestartGameElement RestartGameElement) { m_ResetNodes.Add(RestartGameElement); }
    public void RefillNodes()
    {
        GameObject[] eve = GameObject.FindGameObjectsWithTag("EventNode");
        for (int i = 0; i < eve.GetLength(0); i++)
            eve[i].GetComponent<EventNodeScript>().ResetNode();
    }
}


public interface IRestartGameElement
{
    void ResetNode();
}