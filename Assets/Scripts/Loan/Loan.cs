public class Loan
{
    public LoanProduct loanProduct;
    public int startDay;
    public int payDay;
    public float badDebtRisk;
    public float amount;
    public string status;

    public Loan(LoanProduct loanProduct, int startDay, int payDay, float badDebtRisk, float amount) {
        this.loanProduct = loanProduct;
        this.startDay = startDay;
        this.payDay = payDay;
        this.badDebtRisk = badDebtRisk;
        this.amount = amount;
        this.status = LoanStatus.Waiting;
    }
}