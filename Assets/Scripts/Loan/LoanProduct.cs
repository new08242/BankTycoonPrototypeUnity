public class LoanProduct
{
    public float interestRate;
    public string loanName;
    public float minAmount;
    public float maxAmount;
    public int totalContract;
    public string id;
    
    public LoanProduct(string name, float minAmount, float maxAmount, float interest, string id) {
        this.id = id; // start at 1
        this.interestRate = interest;
        this.loanName = name;
        this.minAmount = minAmount;
        this.maxAmount = maxAmount;
    }
}
