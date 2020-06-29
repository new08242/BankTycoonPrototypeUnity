public class AccountProduct
{
    public string type;
    public float interestRate;
    public string accProductName;
    public string status;
    public int count;
    public string id;
    public float totalMoney;

    public AccountProduct(string type, float interestRate, string name, string id) {
        this.id = id; // start at 1
        this.type = type;
        this.interestRate = interestRate;
        this.accProductName = name;
    }
}
