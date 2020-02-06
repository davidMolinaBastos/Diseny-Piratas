using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodoScript : MonoBehaviour
{
    public enum TNodo {
        ISLA, EVENTO, PIRATA, VOID  }
    // 0 = Norte 
    // 1 = Este 
    // 2 = Sur
    // 3 = Oeste
    public List<NodoScript> nodos;
    public TNodo Tipo;
    public SpriteRenderer imagen;
    private void Start()
    {
        imagen = GetComponent<SpriteRenderer>();
    }
}
