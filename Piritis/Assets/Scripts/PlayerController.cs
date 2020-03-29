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
    public CartaObject[] cardHand = { null, null, null };

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        cb = GameObject.FindGameObjectWithTag("GameController").GetComponent<CardBlackboard>();
        for (int i = 0; i < cardHand.Length; i++)
            SwitchCards(cardHand[i], cb.ReturnRandomPlayerCard(), i);
    }

    void Update()
    {
        for (int i = 0; i < cardHand.Length; i++)
            if (cardHand[i] == null && cb.ReturnDeckCount() > 0)
                SwitchCards(cardHand[i], cb.ReturnRandomPlayerCard(), i);
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
                        nextNode = null;
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
        if (oldHand != null)
            cb.AddCardToDeck(oldHand);
        cb.RemoveDeckCard(oldDeck);
        cardHand[handID] = oldDeck;
    }

    public void RemoveCardFromHand(int index)
    {
        if (cb.ReturnDeckCount() < 1)
            gameController.GameEnd(gameController.loosescene);
        cardHand[index] = null;
        SwitchCards(null, cb.ReturnRandomPlayerCard(), index);
    }

    /*public CartaObject GetRandomDeckCard()
    {
        if (cb.CartasPlayer.Count < 1)
            return null;
        Random.InitState((int)System.DateTime.Now.Ticks);
        cb.CartasPlayer.TrimExcess();
        int r = Random.Range(0, cb.CartasPlayer.Count);
        return cb.CartasPlayer[r];
    }
    */
    public void Teleport(Transform objective)
    {
        transform.position = objective.position;
        CurrentNode = objective.gameObject.GetComponent<NodoScript>();
        gameController.CallShopWindow();
    }

    public void AddNewRandomCard(int lvl) { cb.AddCardToDeck(cb.ReturnRandomCard(lvl)); }
    public void DeleteRandomDeckCard()
    {
        if (cb.ReturnDeckCount() > 1)
            cb.RemoveDeckCard(cb.ReturnRandomPlayerCard());
    }

    public CartaObject[] GetHand() { return cardHand; }

    public bool CheckLooseState()
    {
        if (cb.EmptyDeck())
            for (int i = 0; i < cardHand.Length; i++)
                if (cardHand[i] == null)
                    return true;
        return false;
    }
    public void DeleteCards(bool[] delete)
    {
        for (int i = 0; i < delete.Length; i++)
            if (delete[i])
                RemoveCardFromHand(i);
    }
    public void SetMoving(bool movin) { canMove = movin; }
}