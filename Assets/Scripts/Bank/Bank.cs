using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    // Resources
    private float money;
    private float customerMoney;
    private float customerDebt;
    private int atmCount;
    private int branchCount;

    // Product
    private Dictionary<string, AccountProduct> accProducts = new Dictionary<string, AccountProduct>();
    private Dictionary<string, Account> accounts = new Dictionary<string, Account>();

    // Bank abilities
    private Dictionary<string, bool> bankAbilities = new Dictionary<string, bool>();

    // Singleton instance
    public static Bank Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        money = 2 * Mathf.Pow(10, 7);

        bankAbilities.Add(BankAbility.Account, false);
        bankAbilities.Add(BankAbility.Loan, false);
        bankAbilities.Add(BankAbility.Deposit, false);
        bankAbilities.Add(BankAbility.Withdraw, false);
    }

    // Update is called once per frame
    void Update()
    {
        // foreach(var ab in bankAbilities) {
        //     Debug.Log("ability: " + ab.Key + "bool: " + ab.Value);
        // }

        CalculateExpense();
    }

    public float GetMoney() {
        return money;
    }
    public float AddMoney(float amount) {
        money += amount;
        return money;
    }

    public float GetCustomerMoney() {
        return customerMoney;
    }
    public float AddCustomerMoney(float amount) {
        customerMoney += amount;
        return customerMoney;
    }

    public float GetCustomerDebt() {
        return customerDebt;
    }
    public float AddCustomerDebt(float amount) {
        customerDebt += amount;
        return customerDebt;
    }

    public int AddATM(int amount) {
        atmCount += amount;
        return atmCount;
    }
    public int GetATM() {
        return atmCount;
    }

    public int AddBranch(int amount) {
        branchCount += amount;
        return branchCount;
    }
    public int GetBranch() {
        return branchCount;
    }

    public void SetAbility(string ability, bool flag) {
        if (bankAbilities.ContainsKey(ability)) {
            bankAbilities[ability] = flag;
            return;
        }

        bankAbilities.Add(ability, flag);
    }
    public bool GetAbilityByKey(string ability) {
        if (bankAbilities.ContainsKey(ability)) {
            return bankAbilities[ability];
        }

        return false;
    }

    public string AddAccountProduct(AccountProduct product) {
        if (!accProducts.ContainsKey(product.accProductName)) {
            accProducts.Add(product.accProductName, product);
            return "";
        }

        return "already_exist";
    }
    public Dictionary<string, AccountProduct> GetAccountProduct() {
        return accProducts;
    }

    public string EditAccoutProduct(AccountProduct product) {
        if (accProducts.ContainsKey(product.accProductName)) {
            accProducts[product.accProductName] = product;
            return "";
        }

        return "not_exist";
    }

    void CalculateExpense() {

    }
}
