using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    [Header("Event Text Window")]
    public Text e_text;
    public Image e_image;

    public void DisplayEvent(bool display)
    {
        e_text.enabled = display;
        e_image.enabled = display;
    }
}
