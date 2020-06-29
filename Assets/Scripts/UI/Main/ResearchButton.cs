using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResearchButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TooltipPopup tooltip;
    public void OnPointerEnter(PointerEventData eventData){
        tooltip.DisplayInfo();
    }

    public void OnPointerExit(PointerEventData eventData){
        tooltip.HideInfo();
    }
}
