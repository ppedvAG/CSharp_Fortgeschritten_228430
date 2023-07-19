namespace LinqErweiterungsmethoden;

public static class ExtensionMethods
{
	public static int Quersumme(this int x) //mittels this <Typ> sich auf einen Typen beziehen
	{
		return x.ToString().Sum(e => (int) char.GetNumericValue(e));
	}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> x)
	{
		return x.OrderBy(e => Random.Shared.Next());
	}

	public static string EnumToString(this FahrzeugMarke m)
	{
		return m switch
		{
			FahrzeugMarke.Audi => "Audi",
			FahrzeugMarke.BMW => "BMW",
			FahrzeugMarke.VW => "VW",
		};
	}
}
