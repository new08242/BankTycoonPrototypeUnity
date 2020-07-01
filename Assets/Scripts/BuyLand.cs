using UnityEngine;
using UnityEngine.UI;

public class BuyLand : MonoBehaviour
{
    public GameObject land;
    public GameObject customerSpawn;
    public float price;
    public Text priceText;

    void Start() {
        priceText.text = string.Format("{0} ฿", price.ToString("n0"));
    }

    public void Buy() {
        if (Bank.Instance.money < price) { return; }

        Bank.Instance.money -= price;
        land.layer = LayerMask.NameToLayer("Owned");
        customerSpawn.SetActive(true);
        Destroy(this.gameObject);
    }
}
