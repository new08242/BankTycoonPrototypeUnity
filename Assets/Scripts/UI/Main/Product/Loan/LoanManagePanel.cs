﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoanManagePanel : MonoBehaviour
{
    public GameObject productMenuPanel;
    public GameObject loanMenuPanel;
    public GameObject manageContractPanel;
    public GameObject listLoanProductPanel;
    public GameObject createLoanPanel;


    // List Panel Component
    public Text No;
    public Text Name;
    public Text Interest;
    public Text TotalContract;

    // Create component
    public InputField interestRateInput;
    public InputField nameInput;
    public GameObject createLog;

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

    public void OnClickCreatePanel() {
        createLoanPanel.SetActive(true);
        loanMenuPanel.SetActive(false);
    }
    public void OnCloseCreatePanel() {
        createLoanPanel.SetActive(false);
        loanMenuPanel.SetActive(true);
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

        listLoanProductPanel.SetActive(true);
        loanMenuPanel.SetActive(false);

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
    }

    public void OnClickCreateLoan() {
        float interestRate = float.Parse(interestRateInput.text);
        Bank.Instance.loanPrdCount++;

        LoanProduct lp = new LoanProduct(nameInput.text, 100000f, 1000000f, interestRate, Bank.Instance.loanPrdCount.ToString());
        Bank.Instance.AddLoanProduct(lp);
        Bank.Instance.SetAbility(BankAbility.LoanContract, true);
        createLog.SetActive(true);
        StartCoroutine(WaitForSecThenInactive(1));
    }
    IEnumerator WaitForSecThenInactive(int sec) {
        yield return new WaitForSeconds(sec);
        createLog.SetActive(false);
    }
}
