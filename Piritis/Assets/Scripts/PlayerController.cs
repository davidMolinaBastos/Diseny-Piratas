using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("KeyCodes")]
    public KeyCode m_LeftKeyCode = KeyCode.A;
    public KeyCode m_RightKeyCode = KeyCode.D;
    public KeyCode m_UpKeyCode = KeyCode.W;
    public KeyCode m_DownKeyCode = KeyCode.S;

    [Space(10)]
    public NodoScript CurrentNode;

    [Header("VariablesMovimiento")]
    public float speed;
    public float closeNodeR;


    //Privadas
    NodoScript nextNode;
    bool moving = false, canMove = true;
    GameController gameController;
    CardBlackboard cb;
    CartaObject[] cardHand = { null, null, null };

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        cb = GameObject.FindGameObjectWithTag("GameController").GetComponent<CardBlackboard>();
        for (int i = 0; i < 3; i++)
            cardHand[i] = cb.ReturnRandomPlayerCard();
    }

    void Update()
    {
        //Movimiento
        if (canMove)
        {
            if (!moving)
            {
                if (Input.GetKey(m_UpKeyCode) && CurrentNode.nodos[0] != null)
                {
                    nextNode = CurrentNode.nodos[0];
                    moving = true;
                }
                else if (Input.GetKey(m_DownKeyCode) && CurrentNode.nodos[2] != null)
                {
                    nextNode = CurrentNode.nodos[2];
                    moving = true;
                }
                else if (Input.GetKey(m_RightKeyCode) && CurrentNode.nodos[1] != null)
                {
                    nextNode = CurrentNode.nodos[1];
                    moving = true;
                }
                else if (Input.GetKey(m_LeftKeyCode) && CurrentNode.nodos[3] != null)
                {
                    nextNode = CurrentNode.nodos[3];
                    moving = true;
                }
            }
            else
            {
                if (nextNode != null)
                {
                    if ((nextNode.transform.position - transform.position).magnitude < closeNodeR)
                    {
                        CurrentNode = nextNode;
                        switch (CurrentNode.tipoNodo)
                        {
                            case NodoScript.TNodo.ISLA:
                                gameController.CallShopWindow();
                                break;
                            case NodoScript.TNodo.EVENTO:
                                gameController.CallEvent(CurrentNode.gameObject.GetComponent<EventNodeScript>());
                                break;
                        }
                        moving = false;
                    }
                    else
                        transform.Translate((nextNode.transform.position - transform.position).normalized * speed * Time.deltaTime);
                }
            }
        }
    }



    public void SwitchCards(CartaObject oldHand, CartaObject oldDeck, int handID)
    {
        cb.CartasPlayer.Add(oldHand);
        cb.CartasPlayer.Remove(oldDeck);
        cardHand[handID] = oldDeck;
    }

    public void RemoveCardFromHand(int index)
    {
        cardHand[index] = null;
        cardHand[index] = GetRandomDeckCard();
    }

    public CartaObject GetRandomDeckCard()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        return cb.CartasPlayer[Random.Range(0, cb.CartasPlayer.Capacity)];
    }

    public void AddNewRandomCard(int lvl) { cb.CartasPlayer.Add(cb.ReturnRandomCard(lvl)); }
    public void DeleteRandomDeckCard()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        cb.CartasPlayer.RemoveAt(Random.Range(0, cb.CartasPlayer.Capacity));
    }

    public CartaObject[] GetHand() { return cardHand; }

    public void SetMoving(bool movin) { canMove = movin; }
}