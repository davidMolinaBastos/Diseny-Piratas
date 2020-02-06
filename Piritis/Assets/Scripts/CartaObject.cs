using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CartaData", menuName = "ScriptableObjects/CartaObject", order = 0)]
public class CartaObject : ScriptableObject
{
    public enum TipoPasiva
    {

    }
    public string nickname;
    public Sprite sprite;
    public int levelCarta;
    public int rollValor;
    public int ID;
}
