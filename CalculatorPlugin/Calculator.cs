using PluginBase;

namespace CalculatorPlugin;

public class Calculator : IPlugin
{
	public string Name => "Calculator Plugin";

	public string Description => "Ein einfacher Rechner";

	public string Version => "1.0";

	public string Author => "Lukas Kern";

	[ReflectionVisible("Addiere")]
	public double Addiere(int x, int y) => x + y;

	[ReflectionVisible("Subtrahiere")]
	public double Subtrahiere(int x, int y) => x - y;
}