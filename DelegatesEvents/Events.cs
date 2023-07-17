namespace DelegatesEvents;

internal class Events
{
	//Event: Statischer Punkt (muss nicht static sein), an den Methoden angehängt werden können
	//Kann nicht instanziert werden
	static event EventHandler TestEvent;

	static event EventHandler<TestEventArgs> ArgsEvent;

	static event EventHandler<int> IntEvent
	{
		add
		{
            Console.WriteLine($"Methode wurde angehängt: {value.Method.Name}");
        }
		remove
		{
			Console.WriteLine($"Methode wurde abgenommen: {value.Method.Name}");
		}
	}


	static void Main(string[] args)
	{
		TestEvent += Events_TestEvent;
		TestEvent?.Invoke(null, EventArgs.Empty); //Bei Invoke wird normalerweise this als sender übergeben (nicht möglich da in der Main Methode)


		ArgsEvent += Events_ArgsEvent;
		ArgsEvent(null, new TestEventArgs() { Value = 10 });


		IntEvent += Events_IntEvent;
		//IntEvent?.Invoke(null, 2);
	}

	private static void Events_IntEvent(object? sender, int e)
	{
        Console.WriteLine("Die Zahl ist " + e);
    }

	private static void Events_ArgsEvent(object? sender, TestEventArgs e)
	{
        Console.WriteLine(e.Value);
    }

	private static void Events_TestEvent(object? sender, EventArgs e)
	{
        Console.WriteLine("TestEvent wurde aufgerufen");
    }
}

public class TestEventArgs : EventArgs
{
	public int Value { get; set; }
}