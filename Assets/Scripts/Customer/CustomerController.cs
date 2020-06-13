using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public GameObject customer;
    public GameObject spawnPointPos;

    private Bank playerBank;
    // Start is called before the first frame update
    void Start()
    {
        playerBank = GameObject.Find("PlayerBank").GetComponent<Bank>();
        StartCoroutine(SpawnCustomerWithStateEveryBySec(2.0f)); 
    }

    IEnumerator SpawnCustomerWithStateEveryBySec(float sec) {
        GameObject newCust = Instantiate(customer);
        newCust.transform.position = spawnPointPos.transform.position;
        Customer custScript = newCust.GetComponent<Customer>();
        custScript.SetState(RandomCustomerState());

        yield return new WaitForSeconds(sec);

        StartCoroutine(SpawnCustomerWithStateEveryBySec(sec));
    }

    private string RandomCustomerState() {
        string state = "";
        float randResult = Random.Range(1, 5);

        int randResultInt = (int)randResult;

        switch (randResultInt)
        {
        case 1:
            state = CustomerState.DecideDepositState;
            break;
        case 2:
            state = CustomerState.WithdrawState;
            break;
        case 3:
            state = CustomerState.LoanState;
            break;
        case 4:
            state = CustomerState.PayLoanState;
            break;
        default:
            break;
        }

        // hard code for testing
        state = CustomerState.DecideDepositState;

        return state;
    }
}
