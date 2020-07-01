public class Employee
{
    public string id;
    public string name;
    public float salary;
    public int efficiency;

    public Employee(string name, float salary, int eff) {
        Bank.Instance.employeeCount++;
        this.id = Bank.Instance.employeeCount.ToString(); // start at 1
        this.name = name;
        this.salary = salary;
        this.efficiency = eff;
    }
}
