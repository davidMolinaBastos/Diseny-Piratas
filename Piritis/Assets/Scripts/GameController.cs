using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int winMin = 10;
    public string winscene = "WinScene";
    public string loosescene = "LooseScene";
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
        FindObjectOfType<AudioManager>().Play("MusicaGeneral");

        mc.SetHUDValues(gold, treasureParts, cb.CartasPlayer.Count);
    }
    private void Update()
    {
        if (eventActive && evento.GetEventType() == EventNodeScript.TEvent.FIGHT && fightCounter > 0)
            fightCounter -= Time.deltaTime;

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.T)) RefillNodes();  //DebugRefill
        if (Input.GetKeyDown(KeyCode.Y)) gold += 100f; //DebugAddMoney
        if (Input.GetKeyDown(KeyCode.U)) treasureParts++;
        if (Input.GetKeyDown(KeyCode.I)) pc.RemoveCardFromHand(0);
#endif
        if (eventActive)
        {
            if(evento.GetEventType() == EventNodeScript.TEvent.FIGHT && fightCounter >= 1 && !mc.results[1].active)
                for(int i = 0; i < 3; i++)
                {
                    mc.results[i].SetActive(true);
                    mc.resultDisplay[i].text = results[i].ToString();
                }
            
            if (evento.GetEventType() == EventNodeScript.TEvent.FIGHT && fightCounter <= 0 && Input.anyKeyDown)//Input.anyKeyDown && evento.GetEventType() == EventNodeScript.TEvent.FIGHT)
            {
                mc.DisplayFight(false, null, pc.GetHand());
                eventActive = false;
                pc.SetMoving(true);
                evento = null;
                DeleteCards();
                bm.FlushValues();
                mc.DisplayHUD(true);
                mc.SetHUDValues(gold, treasureParts, cb.CartasPlayer.Count);
                FindObjectOfType<AudioManager>().Play("MusicaGeneral");
            }
            else if (evento.GetEventType() != EventNodeScript.TEvent.FIGHT && Input.anyKeyDown)
            {
                mc.DisplayEvent(false, null);
                eventActive = false;
                mc.DisplayHUD(true);
                mc.SetHUDValues(gold, treasureParts, cb.CartasPlayer.Count);
                pc.SetMoving(true);
                evento = null;
            }
        }
        /*
        if (pc.CheckLooseState())
            GameEnd(loosescene);*/
        if (treasureParts > winMin)
            GameEnd(winscene);
    }
    
    public void GameEnd( string scene){SceneManager.LoadScene(scene);}

    //InventoryManager
    public void CallInventory(int Case, int Lvl)
    {
        inventory = true;
        invCase = Case;
        mc.DisplayInventory(true, pc, Case, Lvl);
        mc.DisplayHUD(false);
        pc.SetMoving(false);
    }
    public void CloseInventory(int Case, int Lvl)
    {
        inventory = false;
        invCase = Case;
        mc.DisplayInventory(false, pc, Case, Lvl);
        mc.DisplayHUD(true);
        mc.SetHUDValues(gold, treasureParts, cb.CartasPlayer.Count);
        pc.SetMoving(true);
    }

    //Shop Managment
    public void CallShopWindow()
    {
        mc.DisplayShop(true, gold, treasureParts);
        mc.DisplayHUD(false);
        pc.SetMoving(false);
        RefillNodes();
    }
    public void CloseShopWindow()
    {
        mc.DisplayShop(false, gold, treasureParts);
        mc.DisplayHUD(true);
        mc.SetHUDValues(gold, treasureParts, cb.CartasPlayer.Count);
        pc.SetMoving(true);
    }
    public bool CanBuy(float value)
    {
        if (value > gold)
            return false;
        return true;
    }
    public void ChangeGold(float value) {
        if (gold - value < 0) 
            gold = 0;
        gold += value;
        gold = Mathf.Clamp(gold, 0, gold);
    }
    public void ChangePieces(int value)
    {
        if (treasureParts > winMin)
            GameEnd(winscene);
        treasureParts += value;
    }

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

            if(Random.Range(0, 100) < 50)
                FindObjectOfType<AudioManager>().Play("SonidoCombate");
            else
                FindObjectOfType<AudioManager>().Play("SonidoCombate2");
            FindObjectOfType<AudioManager>().Play("Dados");
            if(Random.Range(0,100)<50)
            FindObjectOfType<AudioManager>().Play("CardSound");
            else
                FindObjectOfType<AudioManager>().Play("CardSound");
            mc.DisplayHUD(false);
        }
        else
        {
            mc.DisplayEvent(true, e);
            eventActive = true;
            pc.SetMoving(false);
            evento = e;
            e.Deplete();
            PerformEvent(e);
            mc.DisplayHUD(false);
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
                results = bm.ReturnResults();
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
        for (int i = 0; i < 3; i++)
            if (results[i] == BattleManager.TResults.Loose)
            {
                print(results[i] + " " + i);
                pc.RemoveCardFromHand(i);
            }
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