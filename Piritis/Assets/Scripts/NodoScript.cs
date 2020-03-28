using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodoScript : MonoBehaviour
{
    public enum TNodo { ISLA, EVENTO, VOID }

    /* 0 = Norte 
       1 = Este 
       2 = Sur
       3 = Oeste */

    public List<NodoScript> nodos;
    public TNodo tipoNodo;
}
