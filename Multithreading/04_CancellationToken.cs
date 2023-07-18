namespace Multithreading;

internal class _04_CancellationToken
{
	static void Main(string[] args)
	{
		//Sender-Empfänger Modell
		//Die Source produziert beliebig viele Tokens, über die Source können die Tokens gesteuert werden
		CancellationTokenSource cts = new CancellationTokenSource(); //Sender
		CancellationToken ct = cts.Token; //Empfänger
		
		Thread t = new Thread(Run);
		t.Start(ct);

		Thread.Sleep(500);

		cts.Cancel(); //Auf der Source alle Tokens von der entsprechenden Source canceln
	}

	static void Run(object o)
	{
		try
		{
			if (o is CancellationToken token)
			{
				for (int i = 0; i < 100; i++)
				{
					Console.WriteLine($"Side Thread {i}");

					token.ThrowIfCancellationRequested();
				}
			}
		}
		catch (OperationCanceledException e)
		{
            Console.WriteLine("Thread abgebrochen");
        }
	}
}
