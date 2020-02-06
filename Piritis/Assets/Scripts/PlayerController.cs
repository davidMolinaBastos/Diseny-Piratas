﻿using System.Collections;
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
    public float speed, closeNodeR; 

    //Privadas
    NodoScript nextNode;
    bool moving = false;
    GameController gameController;
    CardBlackboard cb;
    List<CartaObject> currentDeck;
    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        cb = GameObject.FindGameObjectWithTag("GameController").GetComponent<CardBlackboard>();
    }
    void Update()
    {
        //Movimiento
        if (!moving) {
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
            if((nextNode.transform.position - transform.position).magnitude < closeNodeR)
            {
                CurrentNode = nextNode;
                switch (CurrentNode.tipoNodo) {
                    case NodoScript.TNodo.ISLA:
                        gameController.DisplayIslandHUD();
                        break;
                    case NodoScript.TNodo.EVENTO:
                        EventNodeScript evento = (EventNodeScript)CurrentNode;
                        gameController.StartEvent(evento);
                        break;
                }
                moving = false;
            }
            else
                transform.Translate((nextNode.transform.position-transform.position).normalized * speed * Time.deltaTime);
        }
    }
}