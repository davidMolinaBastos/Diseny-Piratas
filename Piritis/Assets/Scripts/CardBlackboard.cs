using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBlackboard : MonoBehaviour
{
    [Header("Lista de todas las Cartas")]
    public CartaObject[] Nivel1 = new CartaObject[13];
    public CartaObject[] Nivel2 = new CartaObject[36];
    public CartaObject[] Nivel3 = new CartaObject[18];
    public CartaObject[] Nivel4 = new CartaObject[17];
    public CartaObject[] Nivel5 = new CartaObject[8];

    public List<CartaObject[]> Niveles = new List<CartaObject[]>();

    [Header("Deck del Player")]
    public List<CartaObject> CartasPlayer;
    public void Start()
    {
        Niveles = new List<CartaObject[]> { Nivel1, Nivel2, Nivel3, Nivel4, Nivel5 };
    }
    public CartaObject ReturnRandomCard(int lvl) { return Niveles[lvl][Random.Range(0, Niveles[lvl].Length)]; }
    public List<CartaObject> GetPlayerCards() { return CartasPlayer; }
}
