using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TooltipPopup tooltip;
    public float price;
    
    // Start is called before the first frame update
    void Start()
    {
        tooltip = GameObject.Find("ToolTip").GetComponent<TooltipPopup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData){
        tooltip.infoText.text = string.Format("{0} baht", price.ToString("n0"));
        tooltip.DisplayInfo();
    }

    public void OnPointerExit(PointerEventData eventData){
        tooltip.HideInfo();
    }
}
