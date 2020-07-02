using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildPanel : MonoBehaviour, IPointerEnterHandler
{
    public GameObject mainPanel;
    public GameObject atmButton;
    public GameObject branchButton;

    private Bank playerBankScript;

    private void Start() {
        playerBankScript = GameObject.Find("PlayerBank").GetComponent<Bank>();
    }

    private void Update() {
        if (playerBankScript.GetAbilityByKey(BankAbility.Build)) {
            atmButton.GetComponent<Button>().interactable = true;
            branchButton.GetComponent<Button>().interactable = true;
        }
    }

    public void OnClickClose() {
        gameObject.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData){
        PlayerInputController.Instance.SetCurrentPlaceableObject(null);
    }
}
