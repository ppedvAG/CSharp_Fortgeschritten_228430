namespace DelegatesEvents;

internal class ComponentWithEvent
{
	static void Main(string[] args)
	{
		Component comp = new();
		comp.Progress += (counter) => Console.WriteLine($"Fortschritt: {counter}");
		comp.ProcessCompleted += () => Console.WriteLine("Prozess fertig");
		comp.StartProcess();
	}
}
