/*
Notes:
Parent method must be virtual
Child method must be override
*/
class Animal
{
    public virtual void Sound()
    {
        Console.WriteLine("Animal makes sound");
    }
}


class Dog : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Dog barks");
    }
}

class Program
{
    static void Main()
    {
        Animal a = new Dog();
        a.Sound(); // Dog barks
    }
}