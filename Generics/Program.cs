namespace Generics;

internal class Program
{
	static void Main(string[] args)
	{
		List<int> ints = new(); //Generic: T wird nach unten übernommen (hier T = int)
		ints.Add(1); //T wird durch int ersetzt

		List<string> listStr = new();
		listStr.Add("123"); //T wird durch String ersetzt

		Dictionary<string, int> dict = new(); //Klasse mit 2 Generics: TKey -> string, TValue -> int
		dict.Add("1", 1);
	}
}

public class DataStore<T> :
	IProgress<T>, //T bei Vererbung weitergeben
	IEquatable<int>
{
	private T[] data;

	public List<T> Data => data.ToList();

	public void Add(T item, int index)
	{
		data[index] = item;
	}

	public T Get(int index)
	{
		if (index < 0 || index > data.Length)
			return default; //default: Standardwert von T (int: 0, string: null, bool: false, ...)
		return data[index];
	}

	public void Report(T value)
	{
		throw new NotImplementedException();
	}

	public bool Equals(int other)
	{
		PrintType<int>();
		throw new NotImplementedException();
	}

	public MyType PrintType<MyType>()
	{
        Console.WriteLine(default(MyType)); //default: Standardwert von T (int: 0, string: null, bool: false, ...)
		Console.WriteLine(typeof(MyType));
        Console.WriteLine(nameof(MyType)); //Der Typ als string ("int", "string", "bool", ...)

		//Typvergleiche mit Generics
		//if (MyType is int)
		//{

		//}

		if (typeof(MyType) == typeof(int))
		{

		}

		object o = 123;
		MyType t = (MyType) Convert.ChangeType(o, typeof(MyType));
		return t;
    }
}