using UnityEngine;
using UnityEngine.UI;

public class ContractUI : MonoBehaviour
{
    public Button appBut;
    public Button rejBut;
    public string id;

    public void Approve() {
        Loan loan = Bank.Instance.loans.Find(item => item.id == id);
        loan.status = LoanStatus.Approve;
        loan.startDay = (int)TimeSystem.Instance.currentTime;
        loan.payDay = loan.startDay + loan.duration;
        Bank.Instance.AddCustomerDebt(loan.amount);
        Bank.Instance.AddMoney(-loan.amount);
        ContractManager.Instance.UpdateContractUI(id);

        appBut.interactable = false;
        rejBut.interactable = false;       
    }

    public void Reject() {
        Loan loan = Bank.Instance.loans.Find(item => item.id == id);
        loan.status = LoanStatus.Reject;
        ContractManager.Instance.UpdateContractUI(id);

        appBut.interactable = false;
        rejBut.interactable = false;
    }
}
