using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NodoScript))]
public class IslandScript : NodoScript
{
    //Objetos, precios, etc etc etc
    string nombre;

    private void Start()
    {
        tipoNodo = TNodo.ISLA;
    }
}
