using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NodoScript))]
public class IslandScript : NodoScript
{
    //Nombre De la Isla
    string nombre;

    [Header("Precios")]
    public float lvl1Price;
    public float lvl2Price;
    public float lvl3Price;
    public float lvl4Price;
    public float lvl5Price;


    [HideInInspector] public List<float> prices = new List<float>();

    public float treasurePrice;

    private void Start()
    {
        tipoNodo = TNodo.ISLA;
        prices = new List<float> { lvl1Price, lvl2Price, lvl3Price, lvl4Price, lvl5Price };
    }


    public float GetPriceOf(bool treasure, int lvl)
    {
        float price;
        if (treasure)
            price = treasurePrice;
        else
            price = prices[lvl-1];
        return price;
    }
}
