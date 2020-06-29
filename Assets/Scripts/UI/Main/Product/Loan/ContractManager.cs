using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractManager : MonoBehaviour
{
    public GameObject content;
    public GameObject contractPrefab;
    public GameObject baseContractGObj;

    // Content
    private Vector2 baseContentSize = new Vector2(0, 285);

    // Contract
    private Vector2 anMin = new Vector2(0, 0.5f);
    private Vector2 anMax = new Vector2(0, 0.5f);
    private Vector2 anPivot = new Vector2(0.5f, 0.5f);
    private Vector2 contractSize = new Vector2(150, 200);
    private Vector3 startPos;
    // Singleton instance
    public static ContractManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start() {
        startPos = baseContractGObj.transform.localPosition;
    }

    void Update() {
        SettleLoanContract();
    }

    public void UpdateContractList() {
        if (Bank.Instance.loans.Count < 1) {
            return;
        }

        // Set content size
        Vector2 contentSize = baseContentSize;
        if (Bank.Instance.loans.Count < 4 ) {
            RectTransform rectTrans = content.GetComponent<RectTransform>();
            rectTrans.sizeDelta = contentSize;

        } else {
            RectTransform rectTrans = content.GetComponent<RectTransform>();
            contentSize.x += 100;

            int extraX = (200 * (Bank.Instance.loans.Count-4));
            contentSize.x += extraX; 
            rectTrans.sizeDelta = contentSize;
        }

        // Set contracts
        Vector3 pos = startPos;
        foreach (var contract in Bank.Instance.loans) {
            RectTransform rectTrans = contract.contractUI.GetComponent<RectTransform>();
            // set anchor
            rectTrans.anchorMin = anMin;
            rectTrans.anchorMax = anMax;
            rectTrans.pivot = anPivot;

            // set size
            rectTrans.sizeDelta = contractSize;

            // set position
            rectTrans.localPosition = pos;

            // set next start pos;
            pos.x += 200;
        }
    }

    public GameObject CreateContractUI(Loan loan) {
        GameObject c = Instantiate(contractPrefab);
        c.transform.SetParent(content.transform, false);
        c.transform.localPosition = startPos;
        c.name = (Bank.Instance.loanRunningID).ToString();

        // set detail to contract
        Text contractText = c.transform.GetChild(0).GetComponent<Text>();
        contractText.text = "";
        contractText.text += string.Format("Name: {0} \n", loan.loanProduct.loanName);
        contractText.text += string.Format("Amount: {0} \n", loan.amount);
        contractText.text += string.Format("Duration: {0} \n", loan.duration);
        contractText.text += string.Format("Interest rate: {0} \n", loan.interestRate);
        contractText.text += string.Format("Bad debt risk: {0} \n\n", loan.badDebtRisk);
        contractText.text += string.Format("Status: {0} \n", loan.status);

        return c;
    }

    public void UpdateContractUI(string id) {
        // set detail to contract
        Loan loan = Bank.Instance.loans.Find(item => item.id == id);
        Text contractText = loan.contractUI.transform.GetChild(0).GetComponent<Text>();
        contractText.text = "";
        contractText.text += string.Format("Name: {0} \n", loan.loanProduct.loanName);
        contractText.text += string.Format("Amount: {0} \n", loan.amount);
        contractText.text += string.Format("Duration: {0} \n", loan.duration);
        contractText.text += string.Format("Interest rate: {0} \n", loan.interestRate);
        contractText.text += string.Format("Bad debt risk: {0} \n\n", loan.badDebtRisk);
        contractText.text += string.Format("Status: {0} \n", loan.status);

        if (loan.status != LoanStatus.Waiting) {
            contractText.text += string.Format("Day start: {0} \n", loan.startDay);
            contractText.text += string.Format("Pay day: {0} \n", loan.payDay);
        }
    }

    public void SettleLoanContract() {
        UpdateContractList();
        Bank.Instance.loans.RemoveAll(item => item.status == LoanStatus.Reject);

        foreach (var contract in Bank.Instance.loans) {
            if (contract.status == LoanStatus.Approve) {
                if (contract.payDay <= TimeSystem.Instance.currentTime) {
                    // settle up
                    float payAmount = contract.amount * (1 + (contract.interestRate/100));
                    Bank.Instance.AddMoney(payAmount);
                    Bank.Instance.AddCustomerDebt(-contract.amount);

                    // update status to settle
                    Loan loan = Bank.Instance.loans.Find(item => item.id == contract.id);
                    loan.status = LoanStatus.Settle;
                    Destroy(loan.contractUI);
                }
            }
        }

        Bank.Instance.loans.RemoveAll(item => item.status == LoanStatus.Settle);
    }
}
