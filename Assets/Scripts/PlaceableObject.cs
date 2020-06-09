using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    private GameObject playerBank;
    private PlayerBank playerBankScript;

    // object attributes
    public float price;

    private void Start() {
        playerBank = GameObject.Find("PlayerBank");
        playerBankScript = playerBank.GetComponent<PlayerBank>();
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
            playerBankScript.SetAbility(PlayerBankAbility.ATM, true);
            playerBankScript.SetAbility(PlayerBankAbility.Account, true);
            break;
        case "Branch":
            playerBankScript.SetAbility(PlayerBankAbility.Account, true);
            playerBankScript.SetAbility(PlayerBankAbility.Loan, true);
            break;
        case "HQ":
            playerBankScript.SetAbility(PlayerBankAbility.Build, true);
            break;
        default:
            break;
        }
    }
}
