using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNodeScript : NodoScript, IRestartGameElement
{
    //Lista de posibles eventos
    public enum TEvent { FIGHT, CHANGE_GOLD, CHANGE_CARD }

    [Header("Eventos")]
    public List<TEvent> eventos;

    [Header("Stats")]
    public float goldValue, crewValue;
    
    bool depleted = false;
    TEvent evento;
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        evento = eventos[Random.Range(0,2)];
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddResetElement(this);
    }
    public void ResetNode()
    {
        depleted = false;
        Random.InitState((int)System.DateTime.Now.Ticks);
        evento = eventos[Random.Range(0, 2)];
    }
}
