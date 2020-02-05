using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CartaObject", order = 1)]

public class CartaObject : MonoBehaviour
{

    public enum TipoPasiva
    {

    }

    public string NombreCarta;
    public Sprite ImagenCarta;
    public int levelCarta;
    public int rolValor;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
