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
    }
}
