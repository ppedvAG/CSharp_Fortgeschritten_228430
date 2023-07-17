using System.ComponentModel;
using System.Threading.Channels;

namespace DelegatesEvents;

internal class ActionFuncPredicate
{
	static List<int> TestList = new();

	static void Main(string[] args)
	{
		//Action, Func, Predicate: Vordefinierte Delegates die in vielen Stellen in C# eingebaut sind
		//Essentiell für die Fortgeschrittene Programmierung
		//Können alles was in dem vorherigen Teil gezeigt wurde

		//Action: Methode mit void und bis zu 16 Parametern
		Action<int, int> action = Addiere;
		action(5, 6);
		action?.Invoke(3, 4);

		DoAction(5, 8, Addiere); //Das Verhalten der Funktion über den Action Parameter anpassen
		DoAction(2, 5, Subtrahiere);
		DoAction(1, 8, action);

		//Praktisches Beispiel
		TestList.ForEach(Quadriere);
		void Quadriere(int x) => Console.WriteLine(x * x);

		//Func: Methode mit Rückgabewert und bis zu 16 Parametern
		Func<int, int, double> func = Multipliziere; //Rückgabetyp der Func ist immer der letzte Parameters
		double d = func(5, 6); //Das Ergebnis der Func auslesen
		double? d2 = func?.Invoke(3, 4); //double?: Nullable double (Invoke könnte null zurückgeben falls die Func null ist)
		double d3 = func?.Invoke(3, 4) ?? double.NaN;

		DoFunc(4, 6, Multipliziere);
		DoFunc(9, 4, Dividiere);
		DoFunc(3, 4, func);

		//Anonyme Funktionen: Funktionen für den einmaligen Gebrauch, ohne sie anlegen zu müssen
		func += delegate (int x, int y) { return x + y; }; //Anonyme Methode

		func += (int x, int y) => { return x + y; }; //Kürzere Form

		func += (x, y) => { return x - y; };

		func += (x, y) => (double) x / y; //Kürzeste, häufigste Form

		//Anwenden von Anonymen Funktionen
		DoAction(5, 7, (x, y) => Console.WriteLine(x + y));
		DoFunc(9, 3, (x, y) => (double) x % y);

		//Mit anonymer Func
		TestList.Count(e => e % 2 == 0);
		TestList.Count(Count2); //Mit dedizierter Methode

		bool Count2(int x) => x % 2 == 0;
	}

	#region Action
	static void Addiere(int x, int y) => Console.WriteLine(x + y);

	static void Subtrahiere(int x, int y) => Console.WriteLine(x - y);

	static void DoAction(int x, int y, Action<int, int> action) => action?.Invoke(x, y);
	#endregion

	#region Func
	public static double Multipliziere(int x, int y) => x * y;

	public static double Dividiere(int x, int y) => x / y;

	public static double DoFunc(int x, int y, Func<int, int, double> func) => func?.Invoke(x, y) ?? double.NaN;
	#endregion
}
