using Microsoft.VisualBasic.FileIO;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
		//File, Directory, Path, Environment
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//SystemJson();

		//NewtonsoftJson();

		//XML();

		//CSV();
	}

	static void SystemJson()
	{
		//File, Directory, Path, Environment
		//string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		//string folderPath = Path.Combine(desktop, "Test");
		//string filePath = Path.Combine(folderPath, "Test.txt");

		//if (!Directory.Exists(folderPath))
		//	Directory.CreateDirectory(folderPath);

		//List<Fahrzeug> fahrzeuge = new()
		//{
		//	new Fahrzeug(0, 251, FahrzeugMarke.BMW),
		//	new Fahrzeug(1, 274, FahrzeugMarke.BMW),
		//	new Fahrzeug(2, 146, FahrzeugMarke.BMW),
		//	new Fahrzeug(3, 208, FahrzeugMarke.Audi),
		//	new Fahrzeug(4, 189, FahrzeugMarke.Audi),
		//	new Fahrzeug(5, 133, FahrzeugMarke.VW),
		//	new Fahrzeug(6, 253, FahrzeugMarke.VW),
		//	new Fahrzeug(7, 304, FahrzeugMarke.BMW),
		//	new Fahrzeug(8, 151, FahrzeugMarke.VW),
		//	new PKW(9, 250, FahrzeugMarke.VW),
		//	new Fahrzeug(10, 217, FahrzeugMarke.Audi),
		//	new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		//};

		//JsonSerializerOptions options = new(); //Einstellungen bei (De-)Serialisieren
		//options.WriteIndented = true; //Json schön schreiben
		//options.ReferenceHandler = ReferenceHandler.IgnoreCycles; //Zirkelbezüge ignorieren

		////WICHTIG: Options übergeben
		//string json = JsonSerializer.Serialize(fahrzeuge, options);
		//File.WriteAllText(filePath, json);

		//string readJson = File.ReadAllText(filePath);
		//Fahrzeug[] readFzg = JsonSerializer.Deserialize<Fahrzeug[]>(readJson, options);

		///////////////////////////////////////////////////////////////////////////////////

		//JsonDocument doc = JsonDocument.Parse(readJson); //Das Json einlesen und zu einem JsonDocument konvertieren um es per Hand einzulesen/verarbeiten
		//foreach (JsonElement element in doc.RootElement.EnumerateArray())
		//{
		//	Console.WriteLine(element.GetProperty("ID").GetInt32()); //Auf einzelne Felder mittels GetProperty zugreifen
		//	Console.WriteLine(element.GetProperty("MaxV").GetInt32()); //Am Ende das Objekt zu einem Typen konvertieren mittels Get<Typ>()
		//	Console.WriteLine((FahrzeugMarke) element.GetProperty("Marke").GetInt32());
		//	Console.WriteLine();
		//}
	}

	static void NewtonsoftJson()
	{
		////File, Directory, Path, Environment
		//string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		//string folderPath = Path.Combine(desktop, "Test");
		//string filePath = Path.Combine(folderPath, "Test.txt");

		//if (!Directory.Exists(folderPath))
		//	Directory.CreateDirectory(folderPath);

		//List<Fahrzeug> fahrzeuge = new()
		//{
		//	new Fahrzeug(0, 251, FahrzeugMarke.BMW),
		//	new Fahrzeug(1, 274, FahrzeugMarke.BMW),
		//	new Fahrzeug(2, 146, FahrzeugMarke.BMW),
		//	new Fahrzeug(3, 208, FahrzeugMarke.Audi),
		//	new Fahrzeug(4, 189, FahrzeugMarke.Audi),
		//	new Fahrzeug(5, 133, FahrzeugMarke.VW),
		//	new Fahrzeug(6, 253, FahrzeugMarke.VW),
		//	new Fahrzeug(7, 304, FahrzeugMarke.BMW),
		//	new Fahrzeug(8, 151, FahrzeugMarke.VW),
		//	new PKW(9, 250, FahrzeugMarke.VW),
		//	new Fahrzeug(10, 217, FahrzeugMarke.Audi),
		//	new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		//};

		//JsonSerializerSettings settings = new(); //Einstellungen bei (De-)Serialisieren
		//settings.Formatting = Formatting.Indented; //Json schön schreiben
		//settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //Zirkelbezüge ignorieren
		//settings.TypeNameHandling = TypeNameHandling.Objects; //Vererbung ermöglichen

		////WICHTIG: Settings übergeben
		//string json = JsonConvert.SerializeObject(fahrzeuge, settings);
		//File.WriteAllText(filePath, json);

		//string readJson = File.ReadAllText(filePath);
		//Fahrzeug[] readFzg = JsonConvert.DeserializeObject<Fahrzeug[]>(readJson, settings);

		//////////////////////////////////////////////////////////////////////////////////

		//JToken doc = JToken.Parse(readJson); //JToken: Allgemeine Klasse für alle Json Elemente (auch das Dokument selbst)
		//foreach (JToken t in doc)
		//{
		//	Console.WriteLine(t["ID"].Value<int>());
		//	Console.WriteLine(t["MaxV"].Value<int>());
		//	Console.WriteLine((FahrzeugMarke) t["Marke"].Value<int>());
		//	Console.WriteLine();
		//}
	}

	static void XML()
	{
		//File, Directory, Path, Environment
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new PKW(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		XmlSerializer xml = new XmlSerializer(fahrzeuge.GetType());
		using (StreamWriter sw = new StreamWriter(filePath))
		{
			xml.Serialize(sw, fahrzeuge);
		}

		using (StreamReader sw = new StreamReader(filePath))
		{
			List<Fahrzeug> readFzg = xml.Deserialize(sw) as List<Fahrzeug>;
		}

		///////////////////////////////////////////////////////////////

		XmlDocument doc = new XmlDocument();
		doc.Load(filePath);

		foreach (XmlNode node in doc.DocumentElement.ChildNodes)
		{
			Console.WriteLine(int.Parse(node["ID"].InnerText));
			Console.WriteLine(int.Parse(node["MaxV"].InnerText));
			Console.WriteLine(Enum.Parse<FahrzeugMarke>(node["Marke"].InnerText));

			//Console.WriteLine(node.Attributes.GetNamedItem("ID").InnerText);
		}
	}

	static void CSV()
	{
		//File, Directory, Path, Environment
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test");
		string filePath = Path.Combine(folderPath, "Test.txt");

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		TextFieldParser tfp = new TextFieldParser(filePath);
		tfp.SetDelimiters(";");
		List<Fahrzeug> readFzg = new();
		while (!tfp.EndOfData)
		{
			string[] fields = tfp.ReadFields();
			Fahrzeug fzg = new Fahrzeug();
			fzg.ID = int.Parse(fields[0]);
			fzg.MaxV = int.Parse(fields[1]);
			fzg.Marke = Enum.Parse<FahrzeugMarke>(fields[2]);
			readFzg.Add(fzg);
		}
	}
}

//Vererbung ermöglichen mit System.Text.Json
//[JsonDerivedType(typeof(Fahrzeug), "F")]
//[JsonDerivedType(typeof(PKW), "P")]

[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(PKW))]

[Serializable]
public class Fahrzeug
{
	//System.Text.Json Attribute
	//[JsonIgnore]
	//[JsonPropertyName("Identifier")]
	public int ID { get; set; }

	//Newtonsoft.Json Attribute
	//[JsonIgnore]
	//[JsonProperty("Maximalgeschwindigkeit")]
	public int MaxV { get; set; }

	//[XmlIgnore]
	//[XmlAttribute]
	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int ID, int MaxV, FahrzeugMarke Marke)
	{
		this.ID = ID;
		this.MaxV = MaxV;
		this.Marke = Marke;
	}

    public Fahrzeug()
    {
        
    }
}

public class PKW : Fahrzeug
{
	public PKW(int ID, int MaxV, FahrzeugMarke Marke) : base(ID, MaxV, Marke)
	{
	}

    public PKW()
    {
        
    }
}

public enum FahrzeugMarke { Audi, BMW, VW }