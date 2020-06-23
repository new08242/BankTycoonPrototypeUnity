using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    private GameObject playerBank;

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
            break;
        case "Branch":
            Bank.Instance.SetAbility(BankAbility.Account, true);
            Bank.Instance.SetAbility(BankAbility.Loan, true);

            // auto create basic loan
            LoanProduct lp = new LoanProduct("BasicLoan", 10000f, 300000f, 10f);
            Bank.Instance.AddLoanProduct(lp);
            break;
        case "HQ":
            Bank.Instance.SetAbility(BankAbility.Build, true);
            Bank.Instance.SetAbility(BankAbility.Product, true);
            break;
        default:
            break;
        }
    }
}
