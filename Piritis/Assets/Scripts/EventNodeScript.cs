using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NodoScript))]
public class EventNodeScript : NodoScript, IRestartGameElement
{
    //Lista de posibles eventos
    public enum TEvent { FIGHT, CHANGE_GOLD, CHANGE_CARD, CHANGE_BOTH, TELEPORT }
    TEvent evento;

    [Header("Eventos")]
    public TEvent[] eventos = new TEvent[2];


    [Header("Stats")]
    public bool tripulantes;
    public float goldValue;
    public float crewValue;
    public int islandID; //Check GameController for the IDs
    public string message = "default";
    [Range(0, 3)] public int pirateLv;
    public CartaObject[] pirateHand = new CartaObject[2];

    GameController gc;
    bool depleted = false;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        tipoNodo = TNodo.EVENTO;
        gameObject.tag = "EventNode";
        Random.InitState((int)System.DateTime.Now.Ticks);
        foreach (TEvent E in eventos)
        {
            if (E == TEvent.FIGHT)
            {
                for (int i = 0; i < pirateHand.GetLength(0); i++)
                {
                    Random.InitState((int)System.DateTime.Now.Ticks);
                    pirateHand[i] = GameObject.FindGameObjectWithTag("GameController").GetComponent<CardBlackboard>().ReturnRandomCard(pirateLv); ;
                }
            }
        }
        evento = eventos[Random.Range(0, 3)];
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddResetElement(this);
    }

    public void ChangeGoldEffect()
    {
        gc.ChangeGold(goldValue);
    }
    public void ChangeCardEffect()
    {

    }
    public void ChangeBothEffect()
    {
        ChangeCardEffect();
        ChangeGoldEffect();
    }
    public void TeleportEffect()
    {

    }

    //DepleteManagment
    public void ResetNode()
    {
        depleted = false;
        Random.InitState((int)System.DateTime.Now.Ticks);
        evento = eventos[Random.Range(0, 3)];
    }
    public void Deplete() { depleted = true; }
    public bool GetDeplete() { return depleted; }
    
    public TEvent GetEventType() { return evento; }
}
