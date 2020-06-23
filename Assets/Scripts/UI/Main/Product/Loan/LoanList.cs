using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoanList : MonoBehaviour
{
    public Text No;
    public Text Name;
    public Text Interest;
    public Text TotalAccount;

    // Update is called once per frame
    void Update()
    {
        UpdateList();
    }

    private void UpdateList() {
        No.text = "";
        Name.text = "";
        Interest.text = "";
        TotalAccount.text = "";

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

            TotalAccount.text += product.Value.totalContract.ToString();
            TotalAccount.text += "\n";  
        }
    }
}
