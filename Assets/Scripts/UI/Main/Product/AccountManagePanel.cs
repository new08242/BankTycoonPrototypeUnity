using UnityEngine;
using UnityEngine.UI;

public class AccountManagePanel : MonoBehaviour
{
    public GameObject productMenuPanel;
    public GameObject accountMenuPanel;
    public GameObject createPanel;

    // Create Panel Component
    public Dropdown accountTypeDropDown;
    public InputField nameInput;
    public InputField interestRateInput;

    // List Panel Component
    public Text accountProductListDetail;

    private Bank playerBankScript;

    // Start is called before the first frame update
    void Start()
    {
        playerBankScript = GameObject.Find("PlayerBank").GetComponent<Bank>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickClose() {
        gameObject.SetActive(false);
        productMenuPanel.SetActive(true);
    }

    public void OnClickCloseCreatePanel() {
        createPanel.SetActive(false);
        accountMenuPanel.SetActive(true);
    }

    public void OnClickCreateMenu() {
        createPanel.SetActive(true);
        accountMenuPanel.SetActive(false);
    }

    public void OnClickCreateAccount() {
        string type = accountTypeDropDown.options[accountTypeDropDown.value].text;
        float interestRate = float.Parse(interestRateInput.text);

        
        AccountProduct accPro = new AccountProduct(type, interestRate, nameInput.text);
        playerBankScript.AddAccountProduct(accPro);
        playerBankScript.SetAbility(BankAbility.Deposit, true);
    }

    public void OnClickListMenu() {
        accountProductListDetail.text = "";
    }
}
