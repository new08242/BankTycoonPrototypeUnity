using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public RuntimeAnimatorController IdleAmination;
    public RuntimeAnimatorController WalkAnimation;
    
    private Animator animator;
    private GameObject nearestBank;
    private GameObject nearestATM;
    private Vector3 leavePos;
    private string state;
    private string previousState;

    // Deposit state
    private Vector3 depositDestination;

    // Loan state
    private Vector3 loanDestination;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.runtimeAnimatorController = IdleAmination;

        leavePos = GameObject.Find("CustomerLeavePosition").transform.position;
        nearestBank = GameObject.FindGameObjectWithTag("Branch");
        nearestATM = GameObject.FindGameObjectWithTag("ATM");
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) 
        {
        case "":
            CalculateNextState();
            break;
        case CustomerState.IdelState:
            Idel();
            break;
        case CustomerState.LoanState:
            Loan();
            break;
        case CustomerState.PayLoanState:
            PayLoan();
            break;
        case CustomerState.DecideDepositState:
            DecideDeposit();
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
    void DecideDeposit() {
        // decide deposit
        if (Bank.Instance.GetAbilityByKey(BankAbility.Deposit)) {
            // TODO: refactor this part to be dynamic
            float randResult = Random.Range(1, 3);
            int randResultInt = (int)randResult;

            if (randResultInt == 1) {
                if (nearestBank != null) {
                    depositDestination = nearestBank.transform.position;
                }
                else if (nearestATM != null) {
                    depositDestination = nearestATM.transform.position;
                }
            }
            else {
                if (nearestATM != null) {
                    depositDestination = nearestATM.transform.position;
                }
                else if (nearestBank != null) {
                    depositDestination = nearestBank.transform.position;
                }
            }

            previousState = state;
            state = CustomerState.DepositState;
            return;
        }

        // can't do deposit bank lack the ability
        previousState = state;
        state = CustomerState.LeaveState;
    }
    void Deposit() {
        animator.runtimeAnimatorController = WalkAnimation;
        navMeshAgent.SetDestination(depositDestination);
        if (Mathf.Abs(gameObject.transform.position.x - depositDestination.x) <= 0.1) {
            float randResult = Random.Range(1000, 30000);
            Bank.Instance.AddMoney(randResult);
            Bank.Instance.AddCustomerMoney(randResult);

            previousState = state;
            state = CustomerState.IdelState;
        }
    }

    void Withdraw() {
        animator.runtimeAnimatorController = WalkAnimation;
        navMeshAgent.SetDestination(depositDestination);
        if (Mathf.Abs(gameObject.transform.position.x - depositDestination.x) <= 0.1) {
            float randResult = Random.Range(1000, 30000);
            Bank.Instance.AddMoney(-randResult);
            Bank.Instance.AddCustomerMoney(-randResult);

            previousState = state;
            state = CustomerState.IdelState;
        }
    }

    void Loan() {
        animator.runtimeAnimatorController = WalkAnimation;
        navMeshAgent.SetDestination(depositDestination);
        if (Mathf.Abs(gameObject.transform.position.x - depositDestination.x) <= 0.1) {
            float randResult = Random.Range(10000, 300000);
            Bank.Instance.AddMoney(-randResult);
            Bank.Instance.AddCustomerDebt(-randResult);

            previousState = state;
            state = CustomerState.IdelState;
        }
    }

    void PayLoan() {

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
