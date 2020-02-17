﻿using System.Collections;
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

    float gold = 0, treasureParts = 0;
    bool eventActive;


    // PORT ROYAL = 0, TORTUGA = 1, NEW PROVIDENCE = 2
    public Transform[] Islas = new Transform[3];

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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.T)) RefillNodes();  //DebugRefill
        if (Input.GetKeyDown(KeyCode.Y)) gold += 100f; //DebugAddMoney
#endif
        if (eventActive)
        {
            if (Input.anyKeyDown && evento.GetEventType() == EventNodeScript.TEvent.FIGHT)
            {
                mc.DisplayFight(false);
                eventActive = false;
                pc.SetMoving(true);
                evento = null;
            }
            else if (Input.anyKeyDown)
            {
                mc.DisplayEvent(false, null);
                eventActive = false;
                pc.SetMoving(true);
                evento = null;
            }
        }
    }

    //Shop Managment
    public void CallShopWindow()
    {
        mc.DisplayShop(true, gold);
        pc.SetMoving(false);
        RefillNodes();
    }
    public void CloseShopWindow()
    {
        mc.DisplayShop(false, gold);
        pc.SetMoving(true);
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
            mc.DisplayFight(true);
            eventActive = true;
            pc.SetMoving(false);
            e.Deplete();
            PerformEvent(e);
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