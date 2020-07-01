using UnityEngine;
using UnityEngine.EventSystems;

public class BuildButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TooltipPopup tooltip;
    private PlaceableObject obj;

    public GameObject buildPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        tooltip = GameObject.Find("ToolTip").GetComponent<TooltipPopup>();
        obj = buildPrefab.GetComponent<PlaceableObject>();
    }

    public void OnPointerEnter(PointerEventData eventData){
        tooltip.infoText.text = string.Format("{0} baht", obj.price.ToString("n0"));
        tooltip.DisplayInfo();
    }

    public void OnPointerExit(PointerEventData eventData){
        tooltip.HideInfo();
    }

    public void OnClick() {
        GroundPlacementController.Instance.SetCurrentPlaceableObject(buildPrefab);
    }
}
