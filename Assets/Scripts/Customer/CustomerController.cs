using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public int population;
    public GameObject customer;
    public GameObject spawnPointPos;
    public GameObject leavePosition;

    private struct StateToAssign {
        public string state;
        public string nextState;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCustomerWithStateEveryBySec(2.0f));
    }

    IEnumerator SpawnCustomerWithStateEveryBySec(float sec) {
        GameObject newCust = Instantiate(customer);
        newCust.transform.position = spawnPointPos.transform.position;
        Customer custScript = newCust.GetComponent<Customer>();

        StateToAssign stateToAssign = RandomCustomerState();
        custScript.SetState(stateToAssign.state);
        custScript.nextState = stateToAssign.nextState;
        custScript.leavePos = leavePosition.transform.position;

        yield return new WaitForSeconds(sec);

        StartCoroutine(SpawnCustomerWithStateEveryBySec(sec));
    }

    private StateToAssign RandomCustomerState() {
        StateToAssign stateToAssign = new StateToAssign();
        int randResult = Random.Range(1, 4);
        switch (randResult)
        {
        case 1:
            stateToAssign.state = CustomerState.DecideBankDestinationState;
            stateToAssign.nextState = CustomerState.DepositState;
            break;
        case 2:
            stateToAssign.state = CustomerState.DecideBankDestinationState;
            stateToAssign.nextState = CustomerState.WithdrawState;
            break;
        case 3:
            stateToAssign.state = CustomerState.DecideLoanDestinationState;
            stateToAssign.nextState = CustomerState.LoanState;
            break;
        case 4:
            stateToAssign.state = CustomerState.DecideLoanDestinationState;
            stateToAssign.nextState = CustomerState.PayLoanState;
            break;
        default:
            break;
        }

        return stateToAssign;
    }
}
