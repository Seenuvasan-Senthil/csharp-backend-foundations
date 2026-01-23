class Employee
{
    public int Id { get; }
    public string Name { get; }


    // Constructor
    public Employee(int id, string name)
    {
        Id = id;
        Name = name;
    }
}


class Program
{
    static void Main()
    {
        Employee emp = new Employee(1, "Seenu");
        Console.WriteLine(emp.Name);
    }
}