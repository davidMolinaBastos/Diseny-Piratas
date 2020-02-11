using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardScript : MonoBehaviour
{
    public Text pasiva;
    public Text nickname;

    public Image image;

    public Text levelCarta;
    public Text rollValor;

    public void ChangeCard(CartaObject co)
    {
        pasiva.text = co.pasiva.ToString();
        nickname.text = co.nickname;

        image.sprite = co.sprite;
    }
}
