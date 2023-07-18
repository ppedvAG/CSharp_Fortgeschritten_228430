namespace Multithreading;

internal class _07_Mutex
{
	static void Main(string[] args)
	{
		Mutex m = null;
		if (Mutex.TryOpenExisting("Threading", out _))
		{
            Console.WriteLine("Programm bereits gestartet");
        }
		else
		{
			m = new Mutex(true, "Threading");
            Console.WriteLine("Mutex geöffnet");
        }

		Console.ReadKey();
		m?.ReleaseMutex();
	}
}
