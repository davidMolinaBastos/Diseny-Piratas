using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    [Header("Event Text Window")]
    public Text e_text;
    public Image e_image;
    public EventNodeScript currentEvent;
    public void DisplayEvent(bool display, EventNodeScript evento)
    {
        currentEvent = evento;
        if (currentEvent != null)
            e_text.text = currentEvent.message;
        e_text.enabled = display;
        e_image.enabled = display;
    }
}
