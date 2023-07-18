namespace Multitasking;

internal class _05_ContinueWith
{
	static void Main(string[] args)
	{
		//ContinueWith: Tasks verketten, wenn der erste Task fertig ist, können Folgetasks gestartet werden
		//Auf den vorherigen Task zugreifen mithilfe von Variable der Action
		Task t = new Task(() => { });
		t.ContinueWith(vorherigerTask => { });
		t.ContinueWith(vorherigerTask => { }); //Mehrere Tasks anhängen, werden gleichzeitig gestartet wenn der erste Task fertig ist
		t.Start();

		Task<double> berechne = new Task<double>(() =>
		{
			Thread.Sleep(1000);
			//throw new Exception();
			return Math.Pow(4, 23);
		});
		berechne.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result)); //Dieser Task wird immer ausgeführt (auch bei Fehlern)
		berechne.ContinueWith(vorherigerTask => Console.WriteLine("Task erfolgreich"), TaskContinuationOptions.OnlyOnRanToCompletion); //Führe diesen Task nur bei keinem Fehler aus
		berechne.ContinueWith(vorherigerTask => Console.WriteLine("Task Exception"), TaskContinuationOptions.OnlyOnFaulted); //Führe diesen Task nur bei einem Fehler aus
		berechne.Start();

		Console.ReadKey();
	}
}
