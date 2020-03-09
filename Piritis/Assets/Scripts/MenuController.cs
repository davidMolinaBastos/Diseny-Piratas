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
    //public Animation[] dice;
    //public AnimationClip[] anims;
    public Image[] dice = new Image[3];
    public Sprite[] diceSprites = new Sprite[6];
    public GameObject[] results = new GameObject[3];
    public Text[] resultDisplay = new Text[3];

    [Header("Shop")]
    public GameObject[] Buttons;
    public Text goldCounter;
    public Text treasureCounter;
    public Image BackgroundShop;

    [Header("Inventory")]
    public Image BackgroundInventory;
    public Text InvTitle;
    public GameObject ReturnButton;
    public Text[] HandT;
    public GameObject Hand;
    public GameObject DeckLevels;
    public GameObject[] Lvls;

    [HideInInspector] public CartaObject SelectedHandCard = null;
    [HideInInspector] public CartaObject SelectedDeckCard = null;

    GameController gc;
    public void Start()
    {
        //DEACTIVATE UI ON START
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        e_text.enabled = false;
        e_image.enabled = false;


        foreach (Image i in playerCards)
            i.enabled = false;
        foreach (Image i in enemyCards)
            i.enabled = false;
        Background.enabled = false;
        //foreach (Animation a in dice)
        //  a.gameObject.SetActive(false);
        foreach (Image i in dice)
            i.enabled = false;
        foreach (GameObject go in results)
            go.SetActive(false);

        foreach (GameObject go in Buttons)
            go.SetActive(false);
        goldCounter.enabled = false;
        treasureCounter.enabled = false;
        BackgroundShop.enabled = false;

        BackgroundInventory.enabled = false;
        InvTitle.enabled = false;
        ReturnButton.SetActive(false);
        Hand.SetActive(false);
        DeckLevels.SetActive(false);
        foreach (GameObject go in Lvls)
            go.SetActive(false);
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

    public void DisplayFight(bool display, CartaObject[] pirateHand, CartaObject[] playerHand)
    {
        if (playerHand != null)
            for (int i = 0; i < 3; i++)
            {
                if (playerHand[i] != null)
                {
                    playerCards[i].enabled = display;
                    playerCards[i].sprite = playerHand[i].sprite;
                    //dice[i].clip = anims[gc.ReturnPlayerRolls()[i]];
                    //dice[i].Play();
                    //gc.fightCounter = dice[i].clip.length + 1f;
                    dice[i].enabled = display;
                    dice[i].sprite = diceSprites[Mathf.Clamp(gc.ReturnPlayerRolls()[i], 0, 6)];
                    gc.fightCounter = 2f;
                }
            }
        for (int i = 0; i < 3; i++)
        {
            enemyCards[i].enabled = display;
            if(pirateHand != null)
            if (pirateHand[i] != null)
                enemyCards[i].sprite = pirateHand[i].sprite;

        }
        Background.enabled = display;
        if (!display)
            foreach (GameObject go in results)
                go.SetActive(display);
        //foreach (Animation a in dice)
          //  a.gameObject.SetActive(display);
          
    }

    public void DisplayShop(bool display, float gold, float treasure)
    {
        foreach (GameObject go in Buttons)
            go.SetActive(display);
        goldCounter.enabled = display;
        treasureCounter.enabled = display;
        treasureCounter.text = "Treasure: " + treasure;
        goldCounter.text = "Gold: " + gold;
        BackgroundShop.enabled = display;
    }

    public void DisplayInventory(bool display, PlayerController pc, int Case, int lvl)
    {
        BackgroundInventory.enabled = display;
        InvTitle.enabled = display;
        ReturnButton.SetActive(display);
        switch (Case)
        {
            case 1:
                Hand.SetActive(display);
                for (int i = 0; i < 3; i++)
                    HandT[i].text = pc.GetHand()[i].nickname;
                break;
            case 2:
                DeckLevels.SetActive(display);
                break;
            case 3:
                Lvls[lvl].SetActive(display);
                break;
        }
    }
}
