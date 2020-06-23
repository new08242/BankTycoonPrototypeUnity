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
        Bank.Instance.accPrdCount++;

        AccountProduct accPro = new AccountProduct(type, interestRate, nameInput.text, Bank.Instance.accPrdCount.ToString());
        Bank.Instance.AddAccountProduct(accPro);
        Bank.Instance.SetAbility(BankAbility.Deposit, true);
        Bank.Instance.SetAbility(BankAbility.Withdraw, true);
        createLog.SetActive(true);
        StartCoroutine(WaitForSecThenInactive(1));
    }
    IEnumerator WaitForSecThenInactive(int sec) {
        yield return new WaitForSeconds(sec);
        createLog.SetActive(false);
    }

    public void OnClickListMenu() {
        listPanel.SetActive(true);
        accountMenuPanel.SetActive(false);
    }
    public void OnClickCloseListPanel() {
        listPanel.SetActive(false);
        accountMenuPanel.SetActive(true);
    }
}