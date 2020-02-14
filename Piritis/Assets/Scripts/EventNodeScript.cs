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
    public TEvent[] eventos = new TEvent[2];

    [Header("Stats")]
    public float goldValue;
    public float crewValue;
    public string message = "default";
    [Range(0, 3)] public int pirateLv;
    public CartaObject[] pirateHand = new CartaObject[2];

    bool depleted = false;

    void Start()
    {
        tipoNodo = TNodo.EVENTO;
        Random.InitState((int)System.DateTime.Now.Ticks);
        foreach (TEvent E in eventos)
        {
            if (E == TEvent.FIGHT)
            {
                List<CartaObject> list = GameObject.FindGameObjectWithTag("GameController").GetComponent<CardBlackboard>().GetCardListOfLvl(pirateLv);
                for (int i = 0; i < pirateHand.GetLength(0) - 1; i++)
                {
                    Random.InitState((int)System.DateTime.Now.Ticks);
                    int index = Random.Range(0, 2);
                    print(GameObject.FindGameObjectWithTag("GameController").GetComponent<CardBlackboard>().GetCardListOfLvl(pirateLv));
                    //CartaObject objeto = list[index];
                    //pirateHand[i] = objeto;
                }
            }
        }
        evento = eventos[Random.Range(0, 3)];
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddResetElement(this);
    }
    public void ResetNode()
    {
        depleted = false;
        Random.InitState((int)System.DateTime.Now.Ticks);
        evento = eventos[Random.Range(0, 3)];
    }
    public TEvent GetEventType() { return evento; }
}
