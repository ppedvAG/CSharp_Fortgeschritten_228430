using System.Reflection;

namespace Reflection;

internal class Program
{
	static void Main(string[] args)
	{
		Program p = new Program();
		Type pt = p.GetType(); //Typ aus Objekt entnehmen mittels GetType()
		Type t = typeof(Program); //Typ holen durch typeof(<Typname>)

		object o = Activator.CreateInstance(pt); //Objekt über Typ erstellen

		Convert.ChangeType(o, t); //Typ vom einem Objekt ändern ohne Cast

		/////////////////////////////////////////

		pt.GetMethods(); //Alle Methoden des Typs finden + alle Informationen über alle Methoden
		pt.GetMethod("Test").Invoke(o, null);

        Console.WriteLine(pt.GetProperty("Text").GetValue(o));

		/////////////////////////////////////////

		Assembly a = Assembly.GetExecutingAssembly(); //Das derzeitige Assembly

		//Externe DLL laden
		Assembly loaded = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_07_17\DelegatesEvents\bin\Debug\net7.0\DelegatesEvents.dll");

		Type componentType = loaded.GetType("DelegatesEvents.Component");

		object component = Activator.CreateInstance(componentType);

		componentType.GetEvent("Progress").AddEventHandler(component, (int i) => Console.WriteLine($"Fortschritt: {i}"));
		componentType.GetMethod("StartProcess").Invoke(component, null);
	}

	public string Text { get; set; } = "Zwei Text";

	public void Test()
	{
        Console.WriteLine("Ein Test");
    }
}