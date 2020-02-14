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
    PlayerController pc;
    EventNodeScript evento;
    
    float gold, treasureParts;
    bool eventActive;

    public KeyCode AcceptKeyCode = KeyCode.E;

    private void Start()
    {
        cb = GetComponent<CardBlackboard>();
        mc = GetComponent<MenuController>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void CallEvent(EventNodeScript e)
    {
        if (e.GetEventType() != EventNodeScript.TEvent.FIGHT)
        {
            mc.DisplayEvent(true, e);
            eventActive = true;
            pc.SetMoving(false);
            evento = e;
        }
        else
        {
            mc.DisplayFight(true);
            eventActive = true;
            pc.SetMoving(false);
        }
    }

    public void AddResetElement(IRestartGameElement RestartGameElement)
    {
        m_ResetNodes.Add(RestartGameElement);
    }

    private void Update()
    {
        if (eventActive)// && evento.GetEventType() != EventNodeScript.TEvent.FIGHT)
        {
            if (Input.anyKeyDown)
            {
                mc.DisplayEvent(false, null);
                // ** //
                mc.DisplayFight(false);
                // ** //
                eventActive = false;
                pc.SetMoving(true);
                evento = null;
            }
        }
    }
}
public interface IRestartGameElement
{
    void ResetNode();
}