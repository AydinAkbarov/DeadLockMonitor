//Mutex Task
class Program
{
    // Obyekt əvəzinə Mutex istifadə olunur.
    static Mutex mutex = new Mutex();
    static int TotalStudents = 0;

    static void AddStudents()
    {
        for (int i = 0; i < 5; i++)
        {
            // Mutex istifadə etmək Monitor.Enter/Exit qədər yer tutur.
            mutex.WaitOne();
            try
            {
                TotalStudents++;
                Console.WriteLine($"{Thread.CurrentThread.Name} -> TotalStudents = {TotalStudents}");
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            Thread.Sleep(100);
        }
    }

    static void Main()
    {
        Thread t1 = new Thread(AddStudents) { Name = "Thread 1" };
        Thread t2 = new Thread(AddStudents) { Name = "Thread 2" };

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        Console.WriteLine($"Final TotalStudents: {TotalStudents}");
    }
}