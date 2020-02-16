using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardScript : MonoBehaviour
{
    public Image image;
    public void ChangeCard(CartaObject co) { image.sprite = co.sprite; }
}
