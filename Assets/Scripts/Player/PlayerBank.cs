using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBank : MonoBehaviour
{
    // Resources
    private float money;
    private int atmCount;
    private int branchCount;

    // Bank abilities
    private Dictionary<string, bool> bankAbilities = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        money = 2 * Mathf.Pow(10, 7);

        bankAbilities.Add(PlayerBankAbility.Account, false);
        bankAbilities.Add(PlayerBankAbility.Loan, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetMoney() {
        return money;
    }
    public float AddMoney(float amount) {
        money += amount;
        return money;
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
        Debug.Log("2 bank ability:" + bankAbilities[PlayerBankAbility.Build]);
    }
    public bool GetAbilityByKey(string ability) {
        if (bankAbilities.ContainsKey(ability)) {
            return bankAbilities[ability];
        }

        return false;
    }
}
