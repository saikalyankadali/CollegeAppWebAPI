namespace CollegeApp.Logger
{
    public class LogToServerMemory : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Log to Server Memory: {message}");
        }
    }
}
