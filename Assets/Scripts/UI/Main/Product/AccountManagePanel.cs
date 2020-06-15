using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AccountManagePanel : MonoBehaviour
{
    public GameObject productMenuPanel;
    public GameObject accountMenuPanel;
    public GameObject createPanel;
    public GameObject listPanel;

    // Create Panel Component
    public Dropdown accountTypeDropDown;
    public InputField nameInput;
    public InputField interestRateInput;
    public GameObject createLog;

    // List Panel Component
    public Text No;
    public Text Name;
    public Text Interest;
    public Text TotalAccount;

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
        Bank.Instance.AddAccountProduct(accPro);
        Bank.Instance.SetAbility(BankAbility.Deposit, true);
        createLog.SetActive(true);
        StartCoroutine(WaitForSecThenInactive(1));
    }
    IEnumerator WaitForSecThenInactive(int sec) {
        yield return new WaitForSeconds(sec);
        createLog.SetActive(false);
    }

    public void OnClickListMenu() {
        No.text = "";
        Name.text = "";
        Interest.text = "";
        TotalAccount.text = "";

        listPanel.SetActive(true);
        accountMenuPanel.SetActive(false);

        int num = 0;
        if (Bank.Instance.GetAccountProduct().Count < 1) {
            return;
        }
        foreach(var product in Bank.Instance.GetAccountProduct()) {
            num++;
            No.text += num.ToString() + ".";
            No.text += "\n";

            Name.text += product.Value.accProductName;
            Name.text += "\n";

            Interest.text += product.Value.interestRate.ToString("n2") + "%";
            Interest.text += "\n";

            TotalAccount.text += product.Value.count.ToString();
            TotalAccount.text += "\n";  
        }
    }
    public void OnClickCloseListPanel() {
        listPanel.SetActive(false);
        accountMenuPanel.SetActive(true);
    }
}