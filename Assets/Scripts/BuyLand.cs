using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyLand : MonoBehaviour
{
    public GameObject land;
    public GameObject customerSpawn;

    public void Buy() {
        if (Bank.Instance.money < 5000000) { return; }

        Bank.Instance.money -= 5000000;
        land.layer = LayerMask.NameToLayer("Owned");
        customerSpawn.SetActive(true);
        Destroy(this.gameObject);
    }
}
