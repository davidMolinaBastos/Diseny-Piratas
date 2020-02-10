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

    public enum GameStates
    {
        EVENT, ISLAND, FREEROAM, INVENTORY
    }
    GameStates gameState;
    float gold,treasureParts;

    public KeyCode AcceptKeyCode;

    private void Start()
    {
        cb = GetComponent<CardBlackboard>();
        mc = GetComponent<MenuController>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ChangeState(GameStates.FREEROAM);
    }
    
    public void CallEvent(EventNodeScript evento)
    {
        ChangeState(GameStates.EVENT);
    }

    public void AddResetElement(IRestartGameElement RestartGameElement)
    {
        m_ResetNodes.Add(RestartGameElement);
    }
    private void Update()
    {
        switch (gameState)
        {
            case GameStates.EVENT:
                if (Input.GetKeyDown(AcceptKeyCode))
                    ChangeState(GameStates.FREEROAM);
                break;
            case GameStates.ISLAND:
                break;
            case GameStates.INVENTORY:
                break;
            case GameStates.FREEROAM:
                break;
        }
    }
    public void ChangeState(GameStates newState)
    {
        switch (gameState)
        {
            case GameStates.EVENT:
                mc.DisplayEvent(false);
                break;
            case GameStates.ISLAND:
                break;
            case GameStates.INVENTORY:
                break;
            case GameStates.FREEROAM:
                pc.SetMoving(false);
                break;
        }
        switch (newState)
        {
            case GameStates.EVENT:
                mc.DisplayEvent(true);
                break;
            case GameStates.ISLAND:
                break;
            case GameStates.INVENTORY:
                break;
            case GameStates.FREEROAM:
                pc.SetMoving(true);
                break;
        }
        gameState = newState;
    }
}
public interface IRestartGameElement
{
    void ResetNode();
}