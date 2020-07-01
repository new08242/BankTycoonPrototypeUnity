using UnityEngine;
using UnityEngine.UI;

public class HRPanel : MonoBehaviour
{
    public GameObject hireButton;
    public GameObject manageButton;
    public GameObject hirePanel;
    public GameObject managePanel;
    public GameObject menu;

    // hire
    public Button hire;
    public Button reject;

    // manage
    public GameObject fire;
    public GameObject training;
    public GameObject promote;
    public GameObject listManage;

    // Update is called once per frame
    void Update()
    {
        if (Bank.Instance.GetAbilityByKey(BankAbility.HR)) {
            hireButton.GetComponent<Button>().interactable = true;
            manageButton.GetComponent<Button>().interactable = true;
        }
    }

    public void HireButton() {
        menu.SetActive(false);
        hirePanel.SetActive(true);
    }
    public void HireClose() {
        menu.SetActive(true);
        hirePanel.SetActive(false);
    }

    public void ManageButton() {
        menu.SetActive(false);
        managePanel.SetActive(true);
    }
    public void ManageClose() {
        menu.SetActive(true);
        managePanel.SetActive(false);
    }

    // hire
    public void Hire() {
        Employee employee = new Employee("John Doe", 15000, 100);
        Bank.Instance.employees.Add(employee);
        hire.interactable = false;
        reject.interactable = false;
        fire.SetActive(true);
        promote.SetActive(true);
        training.SetActive(true);
        listManage.SetActive(true);
    }
    public void Reject() {
        hire.interactable = false;
        reject.interactable = false;
    }

    // manage
    public void Fire() {
        Bank.Instance.money -= 15000;
        Bank.Instance.employees.RemoveAll(item => item.id != "");
        fire.SetActive(false);
        promote.SetActive(false);
        training.SetActive(false);
        listManage.SetActive(false);
    }
}
