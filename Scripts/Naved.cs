using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Naved : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public static bool nav = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        nav = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        nav = false;
    }
}
