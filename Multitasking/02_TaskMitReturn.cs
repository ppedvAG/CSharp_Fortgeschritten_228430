namespace Multitasking;

internal class _02_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> intTask = new Task<int>(Run);
		intTask.Start();
        Console.WriteLine(intTask.Result); //intTask.Result blockiert den Main Thread, 2 Lösungen: ContinueWith, await

		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Main Thread: {i}");
		}

		Task t2 = Task.Run(() => { }); //Task mit anonymer Methode

		Task t3 = Task.Run(() =>
		{
            Console.WriteLine("Mehrzeilige");
            Console.WriteLine("anonyme");
            Console.WriteLine("Methode");
        });

		t2.Wait(); //Warte hier bis t2 fertig ist, führe den Code danach erst nach dem Ende von t2 aus
		Task.WaitAll(intTask, t2, t3); //Warte auf alle gegebenen Tasks
		Task.WaitAny(intTask, t2, t3); //Warte bis einer der gegebenen Tasks fertig ist, der Rückgabewert ist der Index des Tasks
    }

	static int Run()
	{
		int summe = 0;
		for (int i = 0; i < 1000; i++)
		{
			summe += i;
			Thread.Sleep(1); //Thread.Sleep bezieht sich hier auf den Task
		}
		return summe;
	}
}
