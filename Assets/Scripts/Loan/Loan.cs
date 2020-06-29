using UnityEngine;

public class Loan
{
    public LoanProduct loanProduct;
    public int startDay;
    public int payDay;
    public int duration;
    public float badDebtRisk;
    public float amount;
    public string status;
    public float interestRate; 
    public GameObject contractUI;
    public string id;

    public Loan(LoanProduct loanProduct, int duration, float badDebtRisk, float amount, float interestRate) {
        this.id = Bank.Instance.loanRunningID.ToString();
        this.loanProduct = loanProduct;
        this.duration = duration;
        this.badDebtRisk = badDebtRisk;
        this.amount = amount;
        this.status = LoanStatus.Waiting;
        this.interestRate = interestRate;

        Bank.Instance.loanRunningID++;
    }
}