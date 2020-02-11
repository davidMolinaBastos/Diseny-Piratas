using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NodoScript))]
public class EventNodeScript : NodoScript, IRestartGameElement
{
    //Lista de posibles eventos
    public enum TEvent { FIGHT, CHANGE_GOLD, CHANGE_CARD, CHANGE_BOTH }
    TEvent evento;

    [Header("Eventos")]
    public List<TEvent> eventos;

    [Header("Stats")]
    public float goldValue;
    public float crewValue;
    public string message = "default";

    bool depleted = false;

    void Start()
    {
        tipoNodo = TNodo.EVENTO;
        Random.InitState((int)System.DateTime.Now.Ticks);
        evento = eventos[Random.Range(0, 2)];
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddResetElement(this);
    }
    public void ResetNode()
    {
        depleted = false;
        Random.InitState((int)System.DateTime.Now.Ticks);
        evento = eventos[Random.Range(0, 2)];
    }
}
