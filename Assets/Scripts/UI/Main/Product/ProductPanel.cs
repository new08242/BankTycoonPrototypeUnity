using UnityEngine;
using UnityEngine.UI;

public class ProductPanel : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject menuPanel;
    public GameObject accountManagePanel;
    public GameObject loanManagePanel;

    // Product main menu
    public GameObject accountButton;
    public GameObject loanButton;

    private Bank playerBankScript;

    // Start is called before the first frame update
    void Start()
    {
        playerBankScript = GameObject.Find("PlayerBank").GetComponent<Bank>();
    }

    // Update is called once per frame
    void Update()
    {
       if (playerBankScript.GetAbilityByKey(BankAbility.Product)) {
            accountButton.GetComponent<Button>().interactable = true;
            loanButton.GetComponent<Button>().interactable = true;
        }
    }

    public void OnClickClose() {
        gameObject.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OnClickAccount() {
        menuPanel.SetActive(false);
        accountManagePanel.SetActive(true);
    }

    public void OnClickLoan() {
        menuPanel.SetActive(false);
        loanManagePanel.SetActive(true);
    }
}
