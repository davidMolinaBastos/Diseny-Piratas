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

    
    float gold, treasureParts;
    bool eventActive;

    public KeyCode AcceptKeyCode = KeyCode.E;

    private void Start()
    {
        cb = GetComponent<CardBlackboard>();
        mc = GetComponent<MenuController>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void CallEvent(EventNodeScript evento)
    {
        //mc.DisplayEvent(true, evento);
        eventActive = true;
        pc.SetMoving(false);
    }

    public void AddResetElement(IRestartGameElement RestartGameElement)
    {
        m_ResetNodes.Add(RestartGameElement);
    }
    private void Update()
    {
        if (eventActive)
        {
            if (Input.GetKeyDown(AcceptKeyCode))
            {
                print("E pressed");
                //mc.DisplayEvent(false, null);
                eventActive = false;
                pc.SetMoving(true);
            }
        }
    }
}
public interface IRestartGameElement
{
    void ResetNode();
}