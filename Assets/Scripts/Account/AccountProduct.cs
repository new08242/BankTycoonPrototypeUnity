using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountProduct
{
    public string type;
    public float interestRate;
    public string accProductName;
    public string status;

    public AccountProduct(string type, float interestRate, string name) {
        this.type = type;
        this.interestRate = interestRate;
        this.accProductName = name;
    }
}
