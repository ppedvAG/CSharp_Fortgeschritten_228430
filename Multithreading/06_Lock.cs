namespace Multithreading;

internal class _06_Lock
{
	static int Zahl = 0;

	static object LockObject = new object();

	static void Main(string[] args)
	{
		List<Thread> threads = new List<Thread>();
		for (int i = 0; i < 100; i++)
		{
			threads.Add(new Thread(ZahlPlusPlus));
			threads[i].Start();
		}
    }

	static void ZahlPlusPlus()
	{
		for (int i = 0; i < 100; i++)
		{
			lock (LockObject) //Dieser Codeblock wird gesperrt wenn ein Thread darauf zugreift
			{
				Zahl++;
				Console.WriteLine(Zahl); //Irreguläre Muster ohne Lock
			} //Lock wird geöffnet

			Monitor.Enter(LockObject); //Dieser Codeblock wird gesperrt wenn ein Thread darauf zugreift
			Zahl++;
			Console.WriteLine(Zahl);
			Monitor.Exit(LockObject); //Monitor wird geöffnet

			//Monitor und Lock haben 1:1 den selben Code
		}
	}
}
