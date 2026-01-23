class Person
{
    public string Name { get; set; }


    public void Introduce()
    {
        Console.WriteLine("Hello, I am " + Name);
    }
}


class Employee : Person
{
    public int EmployeeId { get; set; }
}


class Program
{
    static void Main()
    {
        Employee emp = new Employee();
        emp.Name = "Seenu";
        emp.EmployeeId = 101;
        emp.Introduce();
    }
}