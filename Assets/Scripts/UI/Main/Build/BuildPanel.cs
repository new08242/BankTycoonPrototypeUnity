using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildPanel : MonoBehaviour, IPointerEnterHandler
{
    public GameObject mainPanel;
    public GameObject groundPlacementController;
    public GameObject hqPrefab;
    public GameObject branchPrefab;
    public GameObject atmPrefab;

    public GameObject atmButton;
    public GameObject branchButton;

    private GroundPlacementController gpcScript;
    private PlayerBank playerBankScript;

    private void Start() {
        groundPlacementController = GameObject.Find("GroundPlacementController");
        gpcScript = groundPlacementController.GetComponent<GroundPlacementController>();
        playerBankScript = GameObject.Find("PlayerBank").GetComponent<PlayerBank>();
    }

    private void Update() {
        if (playerBankScript.GetAbilityByKey(PlayerBankAbility.Build)) {
            atmButton.GetComponent<Button>().interactable = true;
            branchButton.GetComponent<Button>().interactable = true;
        }
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
