using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePanel : MonoBehaviour
{
    public GameObject playerBank;
    public Text moneyText;
    public Text updateMoneyText;

    private Bank playerBankScript;
    private float currentDisplayMoney;
    private float currentMoney;
    // Start is called before the first frame update
    void Start()
    {
        playerBank = GameObject.Find("PlayerBank");
        playerBankScript = playerBank.GetComponent<Bank>();
        currentMoney = playerBankScript.GetMoney();
        currentDisplayMoney = currentMoney;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentMoney();
    }

    private void UpdateCurrentMoney() {
        currentMoney = playerBankScript.GetMoney();
        moneyText.text = "฿ " + currentMoney.ToString("n2");

        ShowUpdateMoney();

        currentDisplayMoney = currentMoney;
    }

    private void ShowUpdateMoney() {
        if (currentDisplayMoney != currentMoney)
        {
            updateMoneyText.gameObject.SetActive(true);

            if (currentDisplayMoney > currentMoney)
            {
                updateMoneyText.color = Color.red;
                updateMoneyText.text = "-฿ " + (currentDisplayMoney - currentMoney).ToString("n2");
            }
            else
            {
                updateMoneyText.color = Color.green;
                updateMoneyText.text = "+฿ " + (currentMoney - currentDisplayMoney).ToString("n2");
            }
        }
    }
}
