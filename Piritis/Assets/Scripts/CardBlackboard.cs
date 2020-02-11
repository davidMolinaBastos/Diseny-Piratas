using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBlackboard : MonoBehaviour
{
    [Header("Lista de todas las Cartas")]
    public List<CartaObject> Nivel1;
    public List<CartaObject> Nivel2;
    public List<CartaObject> Nivel3;
    public List<CartaObject> Nivel4;
    public List<CartaObject> Nivel5;

    [HideInInspector]public List<CartaObject>[] Niveles = new List<CartaObject>[5];

    [Header("Deck del Player")]
    public List<CartaObject> CartasPlayer;
    public void Start()
    {
        Niveles[0] = Nivel1;
        Niveles[1] = Nivel2;
        Niveles[2] = Nivel3;
        Niveles[3] = Nivel4;
        Niveles[4] = Nivel5;
    }
}
