using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    private GameObject playerBank;
    private Bank playerBankScript;

    // object attributes
    public float price;

    private void Start() {
        playerBank = GameObject.Find("PlayerBank");
        playerBankScript = playerBank.GetComponent<Bank>();
    }

    public bool IsPlaceable() 
    {
        float currentMoney = playerBankScript.GetMoney();
        if (currentMoney < price)
        {
            return false;
        }

        return true;
    }

    public void Purchase()
    {
        playerBankScript.AddMoney(-price);
        string name = gameObject.name;
        name = name.Replace("(Clone)", "");
        switch (name)
        {
        case "ATM":
            playerBankScript.SetAbility(BankAbility.ATM, true);
            playerBankScript.SetAbility(BankAbility.Account, true);
            break;
        case "Branch":
            playerBankScript.SetAbility(BankAbility.Account, true);
            playerBankScript.SetAbility(BankAbility.Loan, true);
            break;
        case "HQ":
            playerBankScript.SetAbility(BankAbility.Build, true);
            playerBankScript.SetAbility(BankAbility.Product, true);
            break;
        default:
            break;
        }
    }
}
