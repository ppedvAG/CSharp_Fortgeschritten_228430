namespace Sprachfeatures;

internal class Program
{
	static void Main(string[] args)
	{
		object o = null;
		if (o is int i)
		{
			//int i = (int) o;
			Console.WriteLine(i);
		}

		//(string, string, string) x;

		double d = 32_5.2349_8_573_42;
		int g = 1_598_794_257;

		//Referenztypen
		//class
		//Bei Variablenzuweisung wird das Objekt referenziert
		//Bei Vergleichen werden die Speicheradressen verglichen
		//if (t == t2) -> if (t.GetHashCode() == t2.GetHashCode())
		Test t = new Test();
		Test t2 = t;

		Console.WriteLine(t.GetHashCode());
		Console.WriteLine(t2.GetHashCode());

		t.Zahl = 5;
		t2.Zahl = 10;

		//Wertetyp
		//struct
		//Bei Variablenzuweisung wird der Wert kopiert
		//Bei Vergleichen werden die Inhalte verglichen
		//if (w == w2) -> if (w == w2)
		int w = 5;
		int w2 = w;
		w = 10;

		//Null-Coaslescing Operator: Wenn der Linke Wert nicht null ist, nimm ihn, sonst rechts
		string name;
		string nameNeu = null;

		//Ohne NC-Operator
		if (nameNeu != null)
			name = nameNeu;
		else
			throw new Exception();

		//Vereinfacht
		name = nameNeu ?? throw new Exception();

		//Nur bestimmte Parameter übergeben (muss vom Entwickler des Konstruktors vorgesehen sein mittels optionaler Parameter)
		Program program = new Program(vorname: "Lukas", nachname: "Kern");
		Program program1 = new Program(nachname: "", adresse: "");

		//Readonly
		//3 Punkte zum Setzen der Variable möglich
		//- Bei der Variable selbst
		//- Im Konstruktor
		//- Bei Objektinitialisierung
		Test t3 = new Test(10); // { ReadonlyZahl = 10 };

		//return <Wert> switch { ... }

		string inputName = "LUkas";
		string fixName = char.ToUpper(inputName[0]) + inputName[1..].ToLower();


		List<int> x = null;
		if (x == null)
			x = new List<int>();

		x ??= new List<int>();

		//Verbatim String: String der Escape Sequenzen ignoriert (\n, \t, \)
		string str = @"\n \t \\";
		string pfad = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_07_17";

		//String Interpolation: String, in den Code eingebaut werden kann
		//Nützlich für Ausgaben um diese in den String einzubauen
		//Kann auch komplexeren Code enthalten (Berechnungen, Ternary Operator, Null Coalescing Operator, Switch Pattern)
		string kombi = "Der Name ist " + name + ", w ist " + w + ", w2 ist " + w2;
		string inter = $"Der Name ist {name}, w ist {w}, w2 ist {w2}";
		string addition = $"w + w2 = {w + w2}";
		string add = $"{w} + {w2} = {w + w2}";
		string name5 = $"Der Name ist länger als 5 Zeichen: {(name.Length > 5 ? "Ja" : "Nein")}";

		InterfaceTestClass iTest = new InterfaceTestClass();
		//Test2 im Objekt hier nicht sichtbar
		ITest it = iTest;
		it.Test2(); //Test2 ist nur sichtbar wenn die Variable den Interfacetyp hat
            Console.WriteLine(ITest.Zahl);

		Dictionary<string, int> dict = new();

		switch (DateTime.Now.DayOfWeek)
		{
			case >= DayOfWeek.Monday and <= DayOfWeek.Friday:
                    Console.WriteLine("Wochentag");
				break;
			case DayOfWeek.Saturday or DayOfWeek.Sunday:
                    Console.WriteLine("Wochenende");
				break;
        }

		Fahrzeug fzg = new(250, FahrzeugMarke.Audi);

		string Test() => $"Der Pfad zum Kurs ist: {pfad}, der Name ist: {name}, {w switch
			{
				1 => "Eins",
				2 => "Zwei",
				3 => "Drei",
				_ => "Andere Zahl"
			}}";

		string s = $$"""Der Pfad zum "Kurs" ist: {{{pfad}}}, der Name ist: {{{name}}}, {{{w switch
			{
				1 => "Eins",
				2 => "Zwei",
				3 => "Drei",
				_ => "Andere Zahl"
			}}}}""";
	}

	//Strg + . -> Schnelloptionen aufrufen (Glühbirne links)
	public void ExpressionTest() => Console.WriteLine();

	public int ExpTest() => 1; //Return fällt bei Expression Body weg

	//Konstruktor mit 10+ Parametern
	public Program(string vorname = "", string nachname = "", int alter = 0, string adresse = "")
	{

	}
}

public class Test
{
	public int Zahl;

	public readonly int ReadonlyZahl = 5;

	public Test(int x = 0)
	{
		ReadonlyZahl = x;
	}
}

public interface ITest
{
	static readonly int Zahl = 5;

	void Test1();

	void Test2()
	{
		//Bad Practice
		Console.WriteLine();
	}
}

public class InterfaceTestClass : ITest
{
	public void Test1()
	{
		throw new NotImplementedException();
	}
}

//public class Fahrzeug
//{
//	public int MaxV { get; set; }

//	public FahrzeugMarke Marke { get; set; }

//	public Fahrzeug(int MaxV, FahrzeugMarke Marke)
//	{
//		this.MaxV = MaxV;
//		this.Marke = Marke;
//	}
//}

//field: Um Attribute an Parameter bei Records anhängen zu können
public record Fahrzeug([field: Obsolete] int MaxV, FahrzeugMarke Marke);

public enum FahrzeugMarke { Audi, BMW, VW }