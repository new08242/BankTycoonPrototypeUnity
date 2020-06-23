using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoanManagePanel : MonoBehaviour
{
    public GameObject productMenuPanel;
    public GameObject loanMenuPanel;
    public GameObject manageContractPanel;
    public GameObject listLoanProductPanel;

    // List Panel Component
    public Text No;
    public Text Name;
    public Text Interest;
    public Text TotalContract;

    public void OnClickClose() {
        gameObject.SetActive(false);
        productMenuPanel.SetActive(true);
    }

    public void OnClickCloseManageContract() {
        manageContractPanel.SetActive(false);
        loanMenuPanel.SetActive(true);
    }
    public void OnClickManageContract() {
        manageContractPanel.SetActive(true);
        loanMenuPanel.SetActive(false);
    }

    public void OnClickCloseListLoanProduct() {
        listLoanProductPanel.SetActive(false);
        loanMenuPanel.SetActive(true);
    }
    public void OnClickListLoanProduct() {
        No.text = "";
        Name.text = "";
        Interest.text = "";
        TotalContract.text = "";

        int num = 0;
        if (Bank.Instance.GetLoanProduct().Count < 1) {
            return;
        }
        foreach(var product in Bank.Instance.GetLoanProduct()) {
            num++;
            No.text += num.ToString() + ".";
            No.text += "\n";

            Name.text += product.Value.loanName;
            Name.text += "\n";

            Interest.text += product.Value.interestRate.ToString("n2") + "%";
            Interest.text += "\n";

            TotalContract.text += product.Value.totalContract.ToString();
            TotalContract.text += "\n";  
        }

        listLoanProductPanel.SetActive(true);
        loanMenuPanel.SetActive(false);
    }
}
