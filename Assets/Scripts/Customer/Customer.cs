using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public RuntimeAnimatorController IdleAmination;
    public RuntimeAnimatorController WalkAnimation;
    
    private Animator animator;
    private Vector3 nearestBankPos;
    private Vector3 nearestATMPos;
    private Vector3 leavePos;
    private string state = "Idel";

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        leavePos = GameObject.Find("CustomerLeavePosition").transform.position;
        nearestBankPos = GameObject.Find("Branch").transform.position;
        nearestATMPos = GameObject.Find("ATM").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) 
        {
        case "":
            CalculateNextState();
            break;
        case "Idel":
            Idel();
            break;
        case "Loan":
            CalculateNextState();
            break;
        case "PayLoan":
            CalculateNextState();
            break;
        case "Deposite":
            CalculateNextState();
            break;
        case "Withdraw":
            CalculateNextState();
            break;
        case "Leave":
            LeaveCityState();
            break;
        default:
            break;
        }
    }

    void GoToNearestBankState() {

    }

    void GoToNearestATMState() {

    }

    IEnumerator WaitForSecThenLeave(int sec) {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        state = "WaitToLeave";

        yield return new WaitForSeconds(sec);

        Debug.Log("Finished WaitForSeconds at timestamp : " + Time.time);

        state = "Leave";
    }

    void Idel() {
        animator.runtimeAnimatorController = IdleAmination;
        if (state != "WaitToLeave") {
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

    }
}
