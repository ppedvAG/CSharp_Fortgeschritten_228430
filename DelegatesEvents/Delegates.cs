namespace DelegatesEvents;

internal class Delegates
{
	public delegate void Vorstellungen(string name); //Definition von Delegate, speichert Referenzen auf Methoden (Methodenzeiger)

	static void Main(string[] args)
	{
		Vorstellungen v = new Vorstellungen(VorstellungDE); //Erstellung von Delegate mit Initialmethode
		v("Lukas"); //Delegate ausführen

		v += VorstellungEN; //Methode an das Delegate anhängen
		v += VorstellungEN; //Selbe Methode kann mehrmals angehängt werden
		v("Max"); //Methoden werden von oben nach unten ausgeführt

		v -= VorstellungEN; //Methode abnehmen, untere Methoden werden zuerst abgenommen
		v -= VorstellungEN;
		v -= VorstellungEN; //Kein Fehler, wenn die Methode nicht angehängt ist
		v -= VorstellungEN;
		v("Stefan");

		v -= VorstellungDE; //ACHTUNG: Wenn die letzte Methode abgenommen wird, ist das Delegate null
		//v("Lukas");

		if (v is not null) //Null Check vor Ausführung
			v("Lukas");

		//Null Propagation: Wenn das Objekt nicht null ist, führe den Code danach aus, sonst überspringe ihn
		List<int> x = null;
		x?.Add(1);

		v?.Invoke("Max");

		foreach (Delegate dg in v.GetInvocationList()) //Delegate anschauen
		{
            Console.WriteLine(dg.Method.Name);
        }
	}

	static void VorstellungDE(string name)
	{
        Console.WriteLine("Hallo mein Name ist " + name);
    }

	static void VorstellungEN(string name)
	{
        Console.WriteLine($"Hello my name is {name}");
    }
}