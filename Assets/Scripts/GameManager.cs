using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float goalMoney = 21000000;
    public GameObject winBanner;
    public Text goalText;

    // Update is called once per frame
    void Update()
    {
        CheckPassGoal();
    }

    void CheckPassGoal() {
        float moneyWithoutDeposit = Bank.Instance.money - Bank.Instance.customerMoney;
        goalText.text = string.Format("Goal: 21M total money not include deposit.\n{0} / 21,000,000", moneyWithoutDeposit.ToString("n2"));
        if (moneyWithoutDeposit >= goalMoney) {
            winBanner.SetActive(true);
        }
    }
}
