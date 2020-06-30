using UnityEngine;
using UnityEngine.EventSystems;

public class ResearchButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TooltipPopup tooltip;
    public void OnPointerEnter(PointerEventData eventData){
        tooltip.infoText.text = "Requirements\nemployee: 30\nmoney: 5,000,000\ntime: 200";
        tooltip.DisplayInfo();
    }

    public void OnPointerExit(PointerEventData eventData){
        tooltip.HideInfo();
    }
}
