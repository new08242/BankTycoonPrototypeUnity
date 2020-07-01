using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    // Player
    public string mouseState;

    // Resources
    public float money;
    public float customerMoney;
    public float customerDebt;
    public int atmCount;
    public int branchCount;
    public int hqCount;
    public float monthlyExpense;
    private bool isPaidMonthly = false;
    public float annualExpense;
    private bool isPaidAnnually = false;

    public int employeeCount;
    public List<Employee> employees = new List<Employee>();

    // location
    public KdTree<Transform> branches = new KdTree<Transform>();
    public KdTree<Transform> atms = new KdTree<Transform>();

    // Product
    public Dictionary<string, AccountProduct> accProducts = new Dictionary<string, AccountProduct>();
    public Dictionary<string, Account> accounts = new Dictionary<string, Account>();
    public int accountCount = 0;
    public int accPrdCount = 0;

    public Dictionary<string, LoanProduct> loanProducts = new Dictionary<string, LoanProduct>();
    public List<Loan> loans = new List<Loan>();
    public int contractCount = 0;
    public int loanPrdCount = 0;
    public int loanRunningID = 0;

    // Bank abilities
    public Dictionary<string, bool> bankAbilities = new Dictionary<string, bool>();

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
        AutoApproveLoanContract();
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
            loanProducts.Add(product.id, product);
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
        float totalExpense = 0;
        totalExpense += hqCount*50000;
        totalExpense += atmCount*1000;
        totalExpense += branchCount*10000;
        totalExpense += employeeCount*15000;
        totalExpense += (accountCount+accPrdCount+loanPrdCount+loans.Count)*3;

        monthlyExpense = totalExpense;

        // monthly expense
        if ((int)TimeSystem.Instance.currentTime % 30 == 0) {
            if (!isPaidMonthly) {
                Debug.Log("Paid monthly: " + monthlyExpense);
                money -= monthlyExpense;
                isPaidMonthly = true;
            }
        }

        // set not paid
        if ((int)TimeSystem.Instance.currentTime % 30 == 1) {
            isPaidMonthly = false;
        }

        totalExpense = 0;
        foreach (var accPrd in accProducts) {
            totalExpense += accPrd.Value.totalMoney * (accPrd.Value.interestRate/100);
        }

        annualExpense = totalExpense;

        // annual expense
        if ((int)TimeSystem.Instance.currentTime % 365 == 0) {
            if (!isPaidAnnually) {
                Debug.Log("Paid annually: " + annualExpense);
                money -= annualExpense;
                isPaidAnnually = true;
            }
        }

        // set not paid
        if ((int)TimeSystem.Instance.currentTime % 365 == 1) {
            isPaidAnnually = false;
        }
    }

    public void AutoApproveLoanContract() {
        if (!Bank.Instance.GetAbilityByKey(BankAbility.Loan)) { return; }
        if (Bank.Instance.employees.Count < 1) { return; }

        if ((int)TimeSystem.Instance.currentTime%3 == 0) {
            foreach (var em in employees) {
                int count = 0;
                foreach (var loan in loans) {
                    if (count >= 5) { break; }
                    if (loan.status == LoanStatus.Waiting) { 
                        if (money > loan.amount) {
                            loan.contractUI.GetComponent<ContractUI>().Approve();
                            count++;
                        }
                    }
                }
            }
        }
    }
}
