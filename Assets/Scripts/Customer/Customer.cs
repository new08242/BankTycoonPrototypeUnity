using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    // Customer data
    public NavMeshAgent navMeshAgent;
    public RuntimeAnimatorController IdleAmination;
    public RuntimeAnimatorController WalkAnimation;
    private Animator animator;
    private Transform nearestBank;
    private Transform nearestATM;
    public GameObject stateIndicator;
    public Vector3 leavePos;
    private string state;
    private string previousState;
    public string nextState;
    public string custName;

    // Deposit state
    private Vector3 bankATMDestination;

    // Loan state
    private Vector3 loanDestination;

    // Start is called before the first frame update
    void Start()
    {
        GameObject eventCam = GameObject.Find("Camera Rig").transform.GetChild(0).gameObject;
        stateIndicator.GetComponent<Canvas>().worldCamera = eventCam.GetComponent<Camera>();
        stateIndicator.transform.GetChild(1).GetComponent<Text>().text = state;

        animator = this.GetComponent<Animator>();
        animator.runtimeAnimatorController = IdleAmination;

        nearestBank = Bank.Instance.branches.FindClosest(transform.position);
        nearestATM = Bank.Instance.atms.FindClosest(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        stateIndicator.transform.GetChild(1).GetComponent<Text>().text = state;

        switch (state) 
        {
        case "":
            CalculateNextState();
            break;
        case CustomerState.IdelState:
            Idel();
            break;
        case CustomerState.DecideLoanDestinationState:
            DecideLoanDestination(nextState);
            break;
        case CustomerState.LoanState:
            Loan();
            break;
        case CustomerState.PayLoanState:
            PayLoan();
            break;
        case CustomerState.DecideBankDestinationState:
            DecideBankDestination(nextState);
            break;
        case CustomerState.DepositState:
            Deposit();
            break;
        case CustomerState.WithdrawState:
            Withdraw();
            break;
        case CustomerState.LeaveState:
            LeaveCityState();
            break;
        default:
            break;
        }
    }

    // TODO: Need refactor, Infinite state machine pattern 
    // and made destination dynamic in list and random within the list
    void DecideBankDestination(string nextState) {
        // decide deposit
        if (Bank.Instance.GetAbilityByKey(BankAbility.Deposit)) {
            // TODO: refactor this part to be dynamic
            float randResult = Random.Range(1, 3);

            if (randResult == 1) {
                if (nearestBank != null) {
                    bankATMDestination = nearestBank.transform.position;
                }
                else if (nearestATM != null) {
                    bankATMDestination = nearestATM.transform.position;
                }
            }
            else {
                if (nearestATM != null) {
                    bankATMDestination = nearestATM.transform.position;
                }
                else if (nearestBank != null) {
                    bankATMDestination = nearestBank.transform.position;
                }
            }

            previousState = state;
            state = nextState;
            return;
        }

        // can't do deposit bank lack the ability
        previousState = state;
        state = CustomerState.LeaveState;
    }
    void Deposit() {
        if (!Bank.Instance.GetAbilityByKey(BankAbility.Deposit)) {
            previousState = state;
            state = CustomerState.LeaveState;
            return;
        }

        animator.runtimeAnimatorController = WalkAnimation;
        navMeshAgent.SetDestination(bankATMDestination);
        if (Mathf.Abs(gameObject.transform.position.x - bankATMDestination.x) <= 0.1) {

            // create bank account
            int randAmount = Random.Range(1000, 30001);
            int randAccPrd = Random.Range(1, Bank.Instance.GetAccountProduct().Count+1);
            Account acc = new Account(Bank.Instance.GetAccountProduct()[randAccPrd.ToString()], custName, randAmount);
            Bank.Instance.AddAccount(acc);
            
            Bank.Instance.AddMoney(randAmount);
            Bank.Instance.AddCustomerMoney(randAmount);
            Bank.Instance.GetAccountProduct()[randAccPrd.ToString()].totalMoney += randAmount;

            previousState = state;
            state = CustomerState.IdelState;
        }
    }

    void Withdraw() {
        if (!Bank.Instance.GetAbilityByKey(BankAbility.Withdraw)) {
            previousState = state;
            state = CustomerState.LeaveState;
            return;
        }

        animator.runtimeAnimatorController = WalkAnimation;
        navMeshAgent.SetDestination(bankATMDestination);
        if (Mathf.Abs(gameObject.transform.position.x - bankATMDestination.x) <= 0.1) {
            float randAmount = Random.Range(100, 10001);
            
            if (Bank.Instance.GetCustomerMoney() < randAmount) {
                previousState = state;
                state = CustomerState.DepositState;
                return;
            }

            int randAccPrd = Random.Range(1, Bank.Instance.GetAccountProduct().Count+1);
            
            Bank.Instance.AddMoney(-randAmount);
            Bank.Instance.AddCustomerMoney(-randAmount);

            if (Bank.Instance.GetAccountProduct()[randAccPrd.ToString()].totalMoney < randAmount) {
                previousState = state;
                state = CustomerState.DepositState;
                return;
            }
            Bank.Instance.GetAccountProduct()[randAccPrd.ToString()].totalMoney -= randAmount;

            previousState = state;
            state = CustomerState.IdelState;
        }
    }

    void DecideLoanDestination(string nextState) {
        // decide deposit
        if (Bank.Instance.GetAbilityByKey(BankAbility.Loan)) {
            loanDestination = nearestBank.transform.position;

            previousState = state;
            state = nextState;
            return;
        }

        // can't do loan, bank lack the ability
        previousState = state;
        state = CustomerState.LeaveState;
    }
    void Loan() {
        animator.runtimeAnimatorController = WalkAnimation;
        navMeshAgent.SetDestination(loanDestination);
        if (Mathf.Abs(gameObject.transform.position.x - loanDestination.x) <= 0.1) {
            
            int randAmount = Random.Range(10000, 300001);

            // create loan contract
            int randLoanPrd = Random.Range(1, Bank.Instance.loanProducts.Count);
            int randDuration = Random.Range(15, 61);
            int randBadDepRisk = Random.Range(1, 11);

            Loan loan = new Loan(Bank.Instance.loanProducts[randLoanPrd.ToString()], (int)randDuration, randBadDepRisk, randAmount, Bank.Instance.loanProducts[randLoanPrd.ToString()].interestRate);
            loan.contractUI = ContractManager.Instance.CreateContractUI(loan);
            loan.contractUI.GetComponent<ContractUI>().id = loan.id;

            Bank.Instance.loans.Add(loan);
            Bank.Instance.loanProducts[randLoanPrd.ToString()].totalContract++;

            ContractManager.Instance.UpdateContractList();

            previousState = state;
            state = CustomerState.IdelState;
        }
    }

    void PayLoan() {
        animator.runtimeAnimatorController = WalkAnimation;
        navMeshAgent.SetDestination(loanDestination);
        if (Mathf.Abs(gameObject.transform.position.x - loanDestination.x) <= 0.1) {
            float randResult = Random.Range(10000, 300001);
            Bank.Instance.AddMoney(randResult);
            Bank.Instance.AddCustomerDebt(-randResult);

            previousState = state;
            state = CustomerState.IdelState;
        }
    }

    IEnumerator WaitForSecThenLeave(int sec) {
        state = CustomerState.WaitToLeaveState;

        yield return new WaitForSeconds(sec);

        state = CustomerState.LeaveState;
    }

    void Idel() {
        animator.runtimeAnimatorController = IdleAmination;
        if (state != CustomerState.WaitToLeaveState) {
            StartCoroutine(WaitForSecThenLeave(2));
        }
    }

    void LeaveCityState() {
        animator.runtimeAnimatorController = WalkAnimation;
        navMeshAgent.SetDestination(leavePos);
        if (Mathf.Abs(gameObject.transform.position.x - leavePos.x) <= 0.1) {
            Destroy(gameObject);
        }
    }    

    void CalculateNextState() {
        state = CustomerState.IdelState;
    }

    public void SetState(string state) {
        this.state = state;
    }
}
