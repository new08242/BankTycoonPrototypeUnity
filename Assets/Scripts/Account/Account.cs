public class Account
{
    public string accountNo;
    public AccountProduct accountProduct;
    public string ownerName;
    public float amount;
    public string status;

    public Account(AccountProduct product, string name, float amt) {
        Bank.Instance.accountCount++;
        Bank.Instance.accProducts[product.id].count++;
        this.accountNo = Bank.Instance.accountCount.ToString();

        this.accountProduct = product;
        this.ownerName = name;
        this.amount = amt;
    }
}
