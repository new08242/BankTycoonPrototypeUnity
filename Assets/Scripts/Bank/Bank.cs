using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    // Player
    private string mouseState;

    // Resources
    private float money;
    private float customerMoney;
    private float customerDebt;
    private int atmCount;
    private int branchCount;
    private float monthlyExpense;

    // Product
    public Dictionary<string, AccountProduct> accProducts = new Dictionary<string, AccountProduct>();
    public Dictionary<string, Account> accounts = new Dictionary<string, Account>();
    public int accountCount = 0;
    public int accPrdCount = 0;

    private Dictionary<string, LoanProduct> loanProducts = new Dictionary<string, LoanProduct>();
    private Dictionary<string, Loan> loans = new Dictionary<string, Loan>();
    private int contractCount = 0;

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
        mouseState = MouseState.CameraControl;
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

    public string GetMouseState() {
        return mouseState;
    }
    public string SetMouseState(string mstate) {
        mouseState = mstate;
        return mouseState;
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

    // bank ability
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

    // Account
    public string AddAccountProduct(AccountProduct product) {
        if (!accProducts.ContainsKey(product.accProductName)) {
            accProducts.Add(Bank.Instance.accPrdCount.ToString(), product);
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
    public Error AddAccount(Account acc) {
        if (!accProducts.ContainsKey(acc.accountProduct.accProductName)) {
            return ErrorList.NotFound;
        }
        
        accounts.Add(acc.accountNo, acc);

        return ErrorList.Success;
    }

    // Loan
    public string AddLoanProduct(LoanProduct product) {
        if (!loanProducts.ContainsKey(product.loanName)) {
            loanProducts.Add(product.loanName, product);
            return "";
        }

        return "already_exist";
    }
    public Dictionary<string, LoanProduct> GetLoanProduct() {
        return loanProducts;
    }
    public string EditLoanProduct(LoanProduct product) {
        if (loanProducts.ContainsKey(product.loanName)) {
            loanProducts[product.loanName] = product;
            return "";
        }

        return "not_exist";
    }

    void CalculateExpense() {
        
    }
}
