using System.Collections;

namespace Sonstiges;

internal class Program
{
	static void Main(string[] args)
	{
		Wagon w1 = new();
		Wagon w2 = new();

        Console.WriteLine(w1 == w2); //true obwohl unterschiedliche Speicheradressen

        Zug z = new();
		z += w1;
		z += w2;

		z++;
		z++;
		z++;
		z++;
		z++;

		foreach (Wagon w in z)
		{

		}

		z[0] = new Wagon();
		Console.WriteLine(z[3]);

        //z[30, "Rot"] = new Wagon();
        Console.WriteLine(z[30, "Rot"]);

		var x = z.Wagons.Select(e => new { e.AnzSitze, HC = e.GetHashCode() }).ToList();
		Console.WriteLine(x[0].HC);
    }
}

public class Zug : IEnumerable
{
	public List<Wagon> Wagons;

	public IEnumerator GetEnumerator()
	{
		//List<Wagon> wagons = new();
		//foreach (Wagon w in Wagons)
		//	wagons.Add(w);
		//return (IEnumerator) wagons.AsEnumerable();

		foreach (Wagon w in Wagons)
			yield return w;

		//return Wagons.GetEnumerator();
	}

	public static Zug operator +(Zug self, Wagon w)
	{
		self.Wagons.Add(w);
		return self;
	}

	public static Zug operator ++(Zug z)
	{
		z.Wagons.Add(new Wagon());
		return z;
	}

	public Wagon this[int index]
	{
		get => Wagons[index];
		set => Wagons[index] = value;
	}

	public Wagon this[int sitze, string farbe]
	{
		get => Wagons.First(e => e.AnzSitze == sitze && e.Farbe == farbe);
	}
}

public class Wagon
{
	public int AnzSitze { get; set; }

	public string Farbe { get; set; }

	public static bool operator ==(Wagon a, Wagon b)
	{
		return a.AnzSitze == b.AnzSitze && a.Farbe == b.Farbe;
	}

	public static bool operator !=(Wagon a, Wagon b)
	{
		return !(a == b);
	}
}