abstract class Payment              //Abstract class cannot be instantiated
{
    public abstract void Pay();     //Can have abstract + non-abstract methods
}


class CreditCardPayment : Payment
{
    public override void Pay()
    {
        Console.WriteLine("Payment done using credit card");
    }
}


class Program
{
    static void Main()
    {
        Payment payment = new CreditCardPayment();
        payment.Pay();
    }
}