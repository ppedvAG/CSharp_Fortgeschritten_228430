namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		#region Einfaches Linq
		List<int> ints = Enumerable.Range(1, 20).ToList();

		Console.WriteLine(ints.Average());
		Console.WriteLine(ints.Min());
		Console.WriteLine(ints.Max());
		Console.WriteLine(ints.Sum());

        Console.WriteLine(ints.First()); //Gibt das erste Element der Liste zurück, Exception wenn kein Element gefunden wurde
        Console.WriteLine(ints.FirstOrDefault()); //Gibt das erste Element der Liste zurück, default Wert (0, null, false, ...) falls kein Element gefunden wurde

		Console.WriteLine(ints.Last()); //Gibt das letzte Element der Liste zurück, Exception wenn kein Element gefunden wurde
		Console.WriteLine(ints.LastOrDefault()); //Gibt das letzte Element der Liste zurück, default Wert (0, null, false, ...) falls kein Element gefunden wurde

        //Console.WriteLine(ints.First(e => e % 50 == 0)); //Exception
		Console.WriteLine(ints.FirstOrDefault(e => e % 50 == 0)); //0

		//-> Aufbau First
		//bool found = false;
		//foreach (int e in ints)
		//{
		//	if (e % 50 == 0)
		//	{
		//		Console.WriteLine(e);
		//		found = true;
		//		break;
		//	}
		//}
		//if (!found)
		//	throw new InvalidOperationException();
		#endregion

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		#region Vergleich Linq Schreibweisen
		//Alle BMWs finden
		List<Fahrzeug> bmwsForEach = new();
		foreach (Fahrzeug f in fahrzeuge)
			if (f.Marke == FahrzeugMarke.BMW)
				bmwsForEach.Add(f);

		//Standard-Linq: SQL-ähnliche Schreibweise (alt)
		List<Fahrzeug> bmwsAlt = (from f in fahrzeuge
								  where f.Marke == FahrzeugMarke.BMW
								  select f).ToList();

		//Methodenkette
		List<Fahrzeug> bmwsNeu = fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).ToList();
		#endregion

		#region Linq mit Objektliste
		//Alle Fahrzeuge mit mindestens 200km/h
		fahrzeuge.Where(e => e.MaxV >= 200);

		//Alle VWs mit MaxV >= 200
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW && e.MaxV >= 200);
		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW).Where(e => e.MaxV >= 200); //Ineffizient, da 2 Schleifen benötigt wegen 2x Where

		//Autos nach MaxV sortieren
		fahrzeuge.OrderBy(e => e.MaxV);
		fahrzeuge.OrderByDescending(e => e.MaxV);
		//fahrzeuge = fahrzeuge.OrderBy(e => e.MaxV).ToList();

		//Autos nach Marke und danach nach Geschwindigkeit sortieren
		fahrzeuge.OrderBy(e => e.Marke).ThenBy(e => e.MaxV);
		fahrzeuge.OrderByDescending(e => e.Marke).ThenByDescending(e => e.MaxV);

		//Order und OrderDescending bei Numerischen- und String Listen
		ints.Order();
		ints.OrderDescending();

		//Select: Liste umformen
		//Häufigster Anwendungsfall: Einzelnes Feld extrahieren und weiterverarbeiten
		fahrzeuge.Select(e => e.Marke);
		fahrzeuge.Select(e => e.Marke).Distinct(); //Marken eindeutig machen
		fahrzeuge.Select(e => e.MaxV);

		//Praktisches Beispiel:
		//Alle Dateien und Ordner ohne Pfade und Endungen auflisten
		string[] pfade = Directory.GetFiles(@"C:\Windows\System32");
		List<string> p = new();
		foreach (string s in pfade)
			p.Add(Path.GetFileNameWithoutExtension(s));

		List<string> p2 = Directory.GetFiles(@"C:\Windows\System32").Select(e => Path.GetFileNameWithoutExtension(e)).ToList();
        Console.WriteLine(p.SequenceEqual(p2));

		//All & Any
		//Fahren alle Fahrzeuge mindestens 200km/h?
		fahrzeuge.All(e => e.MaxV >= 200);
		if (fahrzeuge.All(e => e.MaxV >= 200))
		{

		}

		//Fährt mindestens ein Fahrzeug über 200km/h?
		fahrzeuge.Any(e => e.MaxV > 200);

		fahrzeuge.Any(); //fahrzeuge.Count > 0

		//Wieviele VWs haben wir?
		fahrzeuge.Count(e => e.Marke == FahrzeugMarke.VW);

		//Linq vereinfachen
		fahrzeuge.Select(e => e.MaxV).Average();
		fahrzeuge.Average(e => e.MaxV);

		fahrzeuge.Where(e => e.Marke == FahrzeugMarke.VW).Count();
		fahrzeuge.Count(e => e.Marke == FahrzeugMarke.VW);

		fahrzeuge.Where(e => e.MaxV >= 300).First();
		fahrzeuge.First(e => e.MaxV >= 300);

		//Min, Max, MinBy, MaxBy
		fahrzeuge.Min(e => e.MaxV); //Die kleinste Geschwindigkeit (int)
		fahrzeuge.MinBy(e => e.MaxV); //Das Fahrzeug mit der kleinsten Geschwindigkeit (Fahrzeug)

		fahrzeuge.Max(e => e.MaxV); //Die größte Geschwindigkeit (int)
		fahrzeuge.MaxBy(e => e.MaxV); //Das Fahrzeug mit der größten Geschwindigkeit (Fahrzeug)

		//Skip & Take
		fahrzeuge.OrderByDescending(e => e.MaxV).Take(5);

		//Chunk, SelectMany
		//Chunk: Teilt die Liste in X große Teile auf
		fahrzeuge.Chunk(5);
		//SelectMany: Glättet eine Liste von Listen auf eine einzelne Liste herunter
		fahrzeuge.Chunk(5).SelectMany(e => e);

		//GroupBy, ToDictionary
		//Alle Objekte nach einem Kriterium gruppieren (Audi-Gruppe, BMW-Gruppe, VW-Gruppe)
		fahrzeuge.GroupBy(e => e.Marke);

		//ToDictionary: Wandelt eine Liste in ein Dictionary um
		Dictionary<FahrzeugMarke, List<Fahrzeug>> dict = fahrzeuge
			.GroupBy(e => e.Marke)
			.ToDictionary(e => e.Key, e => e.ToList());
		//dict[FahrzeugMarke.VW]

		//ToDictionary kann auch auf eine normale Liste angewandt werden (benötigt einen eindeutigen Schlüssel -> Feld das eindeutig ist)
		fahrzeuge.ToDictionary(e => e.GetHashCode());

		//Was ist das schnellste Fahrzeug pro Marke?
		fahrzeuge.GroupBy(e => e.Marke)
			.ToDictionary(e => e.Key, e => e.MaxBy(x => x.MaxV)); //Hier kann der Value Selektor angepasst werden, um andere Values im Dictionary zu haben

		//Aggregate: Wendet für jedes Element der Liste eine Funktion an. Das Ergebnis dieser Funktion kann in den Aggregator geschrieben werden.
		//Der Aggregator ist das Ergebnis der Funktion
		string liste = fahrzeuge.Aggregate(string.Empty, (agg, fzg) => agg + $"Das Fahrzeug hat die Marke {fzg.Marke} und kann maximal {fzg.MaxV}km/h fahren.\n");
        Console.WriteLine(liste);

		//Mit Aggregate
        Console.WriteLine
		(
			fahrzeuge
				.OrderByDescending(e => e.MaxV)
				.Take(5)
				.Aggregate(string.Empty, (agg, fzg) => agg + $"Das Fahrzeug hat die Marke {fzg.Marke} und kann maximal {fzg.MaxV}km/h fahren.\n")
		);

		//Ohne Aggregate
		IEnumerable<Fahrzeug> top5 = fahrzeuge.OrderByDescending(e => e.MaxV).Take(5);
		foreach (Fahrzeug fzg in top5)
            Console.WriteLine($"Das Fahrzeug hat die Marke {fzg.Marke} und kann maximal {fzg.MaxV}km/h fahren.");
		#endregion

		#region Erweiterungsmethoden
		int i = 38257;
		i.Quersumme();
        Console.WriteLine(359827589.Quersumme());

		fahrzeuge.Shuffle();
		dict.Shuffle();
		new int[] { 1, 2, 3, 4, 5 }.Shuffle();

		FahrzeugMarke.Audi.EnumToString();
        #endregion
    }
}

public record Fahrzeug(int MaxV, FahrzeugMarke Marke);

public enum FahrzeugMarke { Audi, BMW, VW }