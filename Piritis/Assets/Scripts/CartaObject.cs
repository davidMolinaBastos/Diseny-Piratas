using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CartaData", menuName = "ScriptableObjects/CartaObject", order = 0)]
public class CartaObject : ScriptableObject
{
    public enum TipoPasiva
    {
         MAL_EMPATE, BUEN_EMPATE, HUIDA, SUPER_EMPATE, //Eventos de empate
         CAMBIAR_VALOR,  SUMA_ORO, SUMA_ALIADOS, KING_SLAYER, CAPITANÍA //Cambios de valores
    }

    public string nickname;

    [Space(5)]
    public TipoPasiva pasiva;
    public Sprite sprite;

    [Space(10)]
    [Range(1, 5)] public int levelCarta;
    public int rollValor;

    [Header("Valor de la suma o resta")]
    public int valor;
}
