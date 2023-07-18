namespace Multitasking;

internal class _03_TaskBeenden
{
	static void Main(string[] args)
	{
		CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
		CancellationToken cancellationToken = cancellationTokenSource.Token;

		Task t = new Task(Run, cancellationToken);
		t.Start();

		Thread.Sleep(500);

		cancellationTokenSource.Cancel();

		Console.ReadKey();
	}

	static void Run(object o) //object o muss gegeben sein für CancellationToken
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
                Console.WriteLine($"Side Task: {i}");
				ct.ThrowIfCancellationRequested(); //Task wirft Exception ist aber nicht sichtbar
				Thread.Sleep(25);
            }
		}
	}
}
