using PluginBase;
using System.Reflection;

namespace PluginClient;

internal class Program
{
	static void Main(string[] args)
	{
		//Plugin laden
		//Pfade sollten aus einer Config kommen (z.B. Json oder XML)
		Assembly loaded = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_07_17\CalculatorPlugin\bin\Debug\net7.0\PluginCalculator.dll");

		Type calcType = loaded.GetTypes().FirstOrDefault(e => e.GetInterface(typeof(IPlugin).Name) != null);

		if (calcType != null) //Plugin gefunden
		{
			object o = Activator.CreateInstance(calcType);
			IPlugin plugin = o as IPlugin; //Gemeinsamer Typ für fixe Felder

            Console.WriteLine($"Name: {plugin.Name}");
            Console.WriteLine($"Beschreibung: {plugin.Description}");
            Console.WriteLine($"Version: {plugin.Version}");
            Console.WriteLine($"Autor: {plugin.Author}");

			for (int i = 0; i < calcType.GetMethods().Length; i++)
			{
				MethodInfo mi = calcType.GetMethods()[i];
				ReflectionVisible attr = mi.GetCustomAttribute<ReflectionVisible>();
				if (attr != null)
				{
					Console.WriteLine($"{i}: {mi.Name}");
					Console.WriteLine(attr.Name);
				}
            }
        }
	}
}