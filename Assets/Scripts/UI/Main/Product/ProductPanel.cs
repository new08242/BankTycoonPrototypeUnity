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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Bank.Instance.GetAbilityByKey(BankAbility.Product) && Bank.Instance.GetAbilityByKey(BankAbility.Account)) {
            accountButton.GetComponent<Button>().interactable = true;
        }

        if (Bank.Instance.GetAbilityByKey(BankAbility.Product) && Bank.Instance.GetAbilityByKey(BankAbility.Loan)) {
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
