using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodoScript : MonoBehaviour
{
    // 0 = Norte 
    //1 = Este 
    //2 = Sur
    // 3 = Oeste
    public Dictionary<string, GameObject> CloseNodes;

    public List<GameObject> nodos;

    public void Start()
    {

    }
    /*
    public Dictionary<string, GameObject> getDicionary()
    {
        //
        if (nodos[0] != null)
        {
            Debug.Log("sdfdsf");
            CloseNodes.Add("Norte", nodos[0]);
        }
        else
            CloseNodes.Add("Norte", null);
        //
        if (nodos[1] != null)
            CloseNodes.Add("Este", nodos[1]);
        else
            CloseNodes.Add("Este", null);
        //
        if (nodos[2] != null)
            CloseNodes.Add("Sur", nodos[2]);
        else
            CloseNodes.Add("Sur", null);
        //
        if (nodos[3] != null)
            CloseNodes.Add("Oeste", nodos[3]);
        else
            CloseNodes.Add("Oeste", null);

        return CloseNodes;
    }
    */
}
