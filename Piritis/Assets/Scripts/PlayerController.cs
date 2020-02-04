using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public enum TdeNodo{
        isla, enemigo, evento
    }
    public TdeNodo TipoNodo;

    [Header("KeyCodes")]
    public KeyCode m_LeftKeyCode = KeyCode.A;
    public KeyCode m_RightKeyCode = KeyCode.D;
    public KeyCode m_UpKeyCode = KeyCode.W;
    public KeyCode m_DownKeyCode = KeyCode.S;
    [Space(10)]
    public NodoScript ActualNode;

    public float speed; 

    Vector2 finalPosition;
    bool moving = false;
    //private Dictionary<string, GameObject> PossiblePositions;
    
    void Start()
    {
    }
    
    void Update()
    {
        //Movimiento
        
        if (!moving) {
            if (Input.GetKey(m_UpKeyCode) && ActualNode.nodos[0] != null)
            {
                finalPosition = ActualNode.nodos[0].transform.position;
                moving = true;
            }

            else if (Input.GetKey(m_DownKeyCode) && ActualNode.nodos[2] != null)
            {
                finalPosition = ActualNode.nodos[2].transform.position;
                moving = true;
            }

            else if (Input.GetKey(m_RightKeyCode) && ActualNode.nodos[1] != null)
            {
                finalPosition = ActualNode.nodos[1].transform.position;
                moving = true;
            }

            else if (Input.GetKey(m_LeftKeyCode) && ActualNode.nodos[3] != null)
            {
                finalPosition = ActualNode.nodos[3].transform.position;
                moving = true;
            }
        }
        else
        {
            if(((Vector2)transform.position- finalPosition).magnitude < 0.05f)
            {
                moving = false;
                return;
            }
            transform.position = new Vector3(1,1,0) * ((Vector2)transform.position - finalPosition).normalized * speed * Time.deltaTime;
        }
    }
    private void LateUpdate()
    {
        print(finalPosition);
    }
    void OnTriggerEnter2D(Collider2D _Collider)
    {
        Debug.Log("Collision");
        if (_Collider.tag == "Nodo")
            ActualNode = _Collider.GetComponent<NodoScript>();
    }
}