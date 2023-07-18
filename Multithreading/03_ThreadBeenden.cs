namespace Multithreading;

internal class _03_ThreadBeenden
{
	static void Main(string[] args)
	{
		try
		{
			Thread t = new Thread(Run);
			t.Start();

			Thread.Sleep(2000);

			t.Interrupt();
		}
		catch (ThreadInterruptedException e)
		{
			//Exception kann nicht hier oben gefangen werden, weil wir im Thread mitbekommen müssen, wann der Thread gestoppt wird
		}
	}

	static void Run()
	{
		try
		{
			for (int i = 0; ; i++)
			{
				Thread.Sleep(100);
				Console.WriteLine($"Überprüfung {i}");
			}
		}
		catch (ThreadInterruptedException e)
		{
            Console.WriteLine("Thread gestoppt"); //Exception muss hier unten gefangen werden
        }
	}
}
