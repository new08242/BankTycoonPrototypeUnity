using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePanel : MonoBehaviour
{
    public Text moneyText;
    public Text updateMoneyText;

    public Text customerMoneyText;
    public Text updateCustomerMoneyText;

    public Text customerDebtText;
    public Text updateCustomerDebtText;

    private float currentDisplayMoney;
    private float currentMoney;

    private float currentDisplayCustomerMoney;
    private float currentCustomerMoney;

    private float currentDisplayCustomerDebt;
    private float currentCustomerDebt;

    // Start is called before the first frame update
    void Start()
    {
        currentMoney = Bank.Instance.GetMoney();
        currentDisplayMoney = currentMoney;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentMoney();
    }

    private void UpdateCurrentMoney() {
        currentMoney = Bank.Instance.GetMoney();
        currentCustomerMoney = Bank.Instance.GetCustomerMoney();
        currentCustomerDebt = Bank.Instance.GetCustomerDebt();

        moneyText.text = "฿ " + currentMoney.ToString("n2");
        customerMoneyText.text = "฿ " + currentCustomerMoney.ToString("n2");
        customerDebtText.text = "฿ " + currentCustomerDebt.ToString("n2");

        currentDisplayMoney = ShowUpdateMoney(currentDisplayMoney, currentMoney, updateMoneyText);
        currentDisplayCustomerMoney = ShowUpdateMoney(currentDisplayCustomerMoney, currentCustomerMoney, updateCustomerMoneyText);
        currentDisplayCustomerDebt = ShowUpdateMoney(currentDisplayCustomerDebt, currentCustomerDebt, updateCustomerDebtText);
    }

    private float ShowUpdateMoney(float currentDisplay, float currentMoney , Text textDisplay) {
        if (currentDisplay != currentMoney)
        {
            if (currentDisplay > currentMoney)
            {
                textDisplay.color = Color.red;
                textDisplay.text = "-฿ " + (currentDisplay - currentMoney).ToString("n2");
            }
            else
            {
                textDisplay.color = Color.green;
                textDisplay.text = "+฿ " + (currentMoney - currentDisplay).ToString("n2");
            }

            textDisplay.GetComponent<UpdateMoneyText>().SetFade();
            textDisplay.gameObject.SetActive(true);
        }
        currentDisplay = currentMoney;
        return currentDisplay;
    }
}
