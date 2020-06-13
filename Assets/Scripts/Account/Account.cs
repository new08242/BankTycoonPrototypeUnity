using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account
{
    public AccountProduct accountProduct;
    public string ownerName;
    public float amount;
    public string status;

    public Account(AccountProduct product, string name, float amt) {
        this.accountProduct = product;
        this.ownerName = name;
        this.amount = amt;
    }
}
