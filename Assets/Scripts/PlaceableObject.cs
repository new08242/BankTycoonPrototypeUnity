using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    // object attributes
    public float price;
    public float monthlyExpense;

    public bool IsPlaceable() 
    {
        float currentMoney = Bank.Instance.GetMoney();
        if (currentMoney < price)
        {
            return false;
        }

        return true;
    }

    public void Purchase()
    {
        Bank.Instance.AddMoney(-price);
        string name = gameObject.name;
        name = name.Replace("(Clone)", "");
        switch (name)
        {
        case "ATM":
            Bank.Instance.SetAbility(BankAbility.ATM, true);
            Bank.Instance.SetAbility(BankAbility.Account, true);
            Bank.Instance.atmCount++;
            Bank.Instance.atms.Add(transform);

            break;
        case "Branch":
            Bank.Instance.SetAbility(BankAbility.Account, true);
            Bank.Instance.SetAbility(BankAbility.Deposit, true);
            Bank.Instance.SetAbility(BankAbility.Withdraw, true);
            Bank.Instance.SetAbility(BankAbility.Loan, true);
            Bank.Instance.branchCount++;
            Bank.Instance.branches.Add(transform);

            // auto create basic loan
            Bank.Instance.loanPrdCount++;
            LoanProduct lp = new LoanProduct("BasicLoan", 100000f, 1000000f, 20f, Bank.Instance.loanPrdCount.ToString());
            Bank.Instance.AddLoanProduct(lp);

            Bank.Instance.accPrdCount++;
            AccountProduct acc = new AccountProduct("Saving", 2f, "BasicAccount", Bank.Instance.accPrdCount.ToString());
            Bank.Instance.AddAccountProduct(acc);

            break;
        case "HQ":
            Bank.Instance.SetAbility(BankAbility.Build, true);
            Bank.Instance.SetAbility(BankAbility.Product, true);
            Bank.Instance.SetAbility(BankAbility.HR, true);
            Bank.Instance.hqCount++;
            break;
        default:
            break;
        }
    }
}
