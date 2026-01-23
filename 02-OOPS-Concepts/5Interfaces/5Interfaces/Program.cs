interface ILogger
{
    void Log(string message);      //Interface methods are public by default, No implementation inside interface.
}


class FileLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine("File log: " + message);
    }
}


class Program
{
    static void Main()
    {
        ILogger logger = new FileLogger();
        logger.Log("Application started");
    }
}