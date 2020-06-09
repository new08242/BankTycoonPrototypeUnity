using UnityEngine;
using UnityEngine.EventSystems;

public class BuildPanel : MonoBehaviour, IPointerEnterHandler
{
    public GameObject mainPanel;
    public GameObject groundPlacementController;
    public GameObject hqPrefab;
    public GameObject branchPrefab;
    public GameObject atmPrefab;

    private GroundPlacementController gpcScript;

    private void Start() {
        groundPlacementController = GameObject.Find("GroundPlacementController");
        gpcScript = groundPlacementController.GetComponent<GroundPlacementController>();
    }

    public void OnClickClose() {
        gameObject.SetActive(false);
        mainPanel.SetActive(true);
    }
    public void OnClickHQ() {
        gpcScript.SetCurrentPlaceableObject(hqPrefab);
    }
    public void OnClickBranch() {
        gpcScript.SetCurrentPlaceableObject(branchPrefab);
    }
    public void OnClickATM() {
        gpcScript.SetCurrentPlaceableObject(atmPrefab);
    }

    public void OnPointerEnter(PointerEventData eventData){
        gpcScript.SetCurrentPlaceableObject(null);
    }
}
