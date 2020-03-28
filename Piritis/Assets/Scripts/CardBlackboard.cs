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
    private List<CartaObject> CartasPlayer;
    public void Start()
    {
        Niveles = new List<CartaObject[]> { Nivel1, Nivel2, Nivel3, Nivel4, Nivel5 };
        CartasPlayer.Add(ReturnRandomCard(0));
        CartasPlayer.Add(ReturnRandomCard(0));
        CartasPlayer.Add(ReturnRandomCard(1));
        CartasPlayer.Add(ReturnRandomCard(1));
        CartasPlayer.Add(ReturnRandomCard(2));
        CartasPlayer.Add(ReturnRandomCard(2));
    }
    public CartaObject ReturnRandomCard(int lvl)
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        return Niveles[lvl][Random.Range(0, Niveles[lvl].Length)];
    }


    public List<CartaObject> GetPlayerCards() { return CartasPlayer; }
    public CartaObject ReturnRandomPlayerCard()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        return CartasPlayer[Random.Range(0, CartasPlayer[0].levelCarta)];
    }
}
