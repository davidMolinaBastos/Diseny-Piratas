using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    [Header("Event Text Window")]
    public Text e_text;
    public Image e_image;
    [HideInInspector] public EventNodeScript currentEvent;

    [Header("Fight Window")]
    public Image[] playerCards = new Image[3];
    public Image[] enemyCards = new Image[3];
    public Image Background;

    [Header("Fight Window")]
    public GameObject[] Buttons;
    public Text goldCounter;
    public Image BackgroundShop;
    public void Start()
    {
        //DEACTIVATE UI ON START
        e_text.enabled = false;
        e_image.enabled = false;
        foreach (Image i in playerCards)
            i.enabled = false;
        foreach (Image i in enemyCards)
            i.enabled = false;
        Background.enabled = false;
        foreach (GameObject go in Buttons)
            go.SetActive(false);
        goldCounter.enabled = false;
        BackgroundShop.enabled = false;
    }

    //DISPLAY METHODS
    public void DisplayEvent(bool display, EventNodeScript evento)
    {
        currentEvent = evento;
        if (currentEvent != null)
            e_text.text = currentEvent.message;
        e_text.enabled = display;
        e_image.enabled = display;
    }

    public void DisplayFight(bool display)
    {
        foreach (Image i in playerCards)
            i.enabled = display;
        foreach (Image i in enemyCards)
            i.enabled = display;
        Background.enabled = display;
    }
    public void DisplayShop(bool display, float gold)
    {
        foreach (GameObject go in Buttons)
            go.SetActive(display);
        goldCounter.enabled = display;
        goldCounter.text = "Gold: " + gold;
        BackgroundShop.enabled = display;
    }
}
