using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		Stopwatch stopwatch = Stopwatch.StartNew();

		//Toast();
		//Geschirr();
		//Kaffee();
		//stopwatch.Stop();
		//Console.WriteLine(stopwatch.ElapsedMilliseconds); //Synchron, 7s

		//stopwatch.Restart();
		//Task t1 = Task.Run(Toast);
		//Task t2 = new Task(Geschirr);
		//t2.ContinueWith(geschirr => Task.Run(Kaffee)); //Abhängigkeit mittels ContinueWith eingebaut
		//t2.Start();
		//Task.WaitAll(t1, t2);
		//stopwatch.Stop();
		//Console.WriteLine(stopwatch.ElapsedMilliseconds); //Parallel aber WaitAll, 4s

		//async Methoden
		//Enden generell mit Async laut Konvention, wenn sie awaited werden sollen
		//async void: Kann await benutzen, kann aber selbst nicht awaited werden
		//async Task: Kann await benutzen und selbst auch awaited werden. async Task Methoden müssen keinen Rückgabewert haben
		//async Task<T>: Kann await benutzen und selbst auch awaited werden. async Task<T> Methoden müssen einen Rückgabewert haben
		//Wenn eine Async Methode aufgerufen wird, wird diese als Task gestartet
		//await: Warte darauf, das diese Aufgabe fertig ist
		//await kann auch ein Ergebnis zurückgeben, wenn der Task ein Ergebnis hat

		//stopwatch.Restart();
		//Task t1 = ToastAsync(); //Startet den Toast Task
		//Task t2 = GeschirrAsync().ContinueWith(_ => KaffeeAsync()); //Starte den Geschirr Task und im Anschluss den Kaffee Task
		//await t1; //Warte bis der Toast Task fertig ist
		//await t2; //Warte bis die Task Kette fertig ist
		//stopwatch.Stop();
		//Console.WriteLine(stopwatch.ElapsedMilliseconds); //Parallel, 4s

		stopwatch.Restart();
		Task<Toast> t1 = ToastObjektAsync(); //Startet den Toast Task
		Task<Tasse> t2 = GeschirrObjektAsync(); //Starte den Geschirr Task
		Tasse tasse = await t2; //Warte hier auf die Tasse und nimm das Objekt aus dem Task heraus (t2.Result)
		Task<Kaffee> t3 = KaffeeObjektAsync(tasse); //Jetzt kann der Kaffee gemacht werden
		Kaffee kaffee = await t3; //Hier sollten die awaits anhand der Länge der Tasks sortiert werden (längster Task am Ende)
		Toast toast = await t1; //Wenn die Länge der Tasks nicht determistisch ist, sollte geschätzt werden
		stopwatch.Stop();
		Console.WriteLine(stopwatch.ElapsedMilliseconds); //Parallel, 4s

		//Task<Toast> t1 = ToastObjektAsync();
		//Kaffee kaffee = await KaffeeObjektAsync(await GeschirrObjektAsync());
		//Toast toast = await t1;

		//WhenAll: Warte auf mehrere Tasks, nützlich bei Tasks die ungefähr gleich lange dauern oder bei denen nicht klar ist, wie lange diese dauern
		await Task.WhenAll(t1, t2, t3); //-> mehrere await Statements konsolidieren
		await Task.WhenAny(t1, t2, t3); //WhenAny: Warte auf den ersten Task der fertig ist
	}

	static void Toast()
	{
		Thread.Sleep(4000);
        Console.WriteLine("Toast fertig");
    }

	static void Geschirr()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Geschirr fertig");
	}

	static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}

	static async Task ToastAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
	}

	static async Task GeschirrAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Geschirr fertig");
	}

	static async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
	}

	static async Task<Toast> ToastObjektAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	static async Task<Tasse> GeschirrObjektAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Geschirr fertig");
		return new Tasse();
	}

	static async Task<Kaffee> KaffeeObjektAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee();
	}
}

public class Toast { }

public class Tasse { }

public class Kaffee { }