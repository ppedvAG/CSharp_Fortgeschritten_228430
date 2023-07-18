namespace Multithreading;

internal class _02_ThreadMitParameter
{
	static void Main(string[] args)
	{
		Thread t = new Thread(Run);
		t.Start(200);
		//t.Start(new int[] { 1, 2, 3, 4, 5 });
		//t.Start(new ThreadData("Gestartet", 200));

		for (int i = 0; i < 100; i++)
		{
            Console.WriteLine($"Main Thread: {i}");
        }
	}

	static void Run(object obj)
	{
		if (obj is int x) //Schneller Cast
		{
			for (int i = 0; i < x; i++)
			{
                Console.WriteLine($"Side Thread: {i}");
            }
		}
	}
}

public record ThreadData(string Status, int Zahl);