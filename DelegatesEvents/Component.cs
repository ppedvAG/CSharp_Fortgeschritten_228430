namespace DelegatesEvents;

internal class Component
{
	//Events müssen nicht EventHandler sein -> Delegate mit void wird benötigt
	public event Action<int> Progress; //int als Parameter für den Fortschritt

	public event Action ProcessCompleted;

	public event EventHandler ProcessStarted;

	public void StartProcess()
	{
		ProcessStarted?.Invoke(this, EventArgs.Empty);
		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(100);
			Progress?.Invoke(i); //? hier essentiell, da der Programmierer auf der anderen Seite möglicherweise keine Funktion hier anhängt
		}
		ProcessCompleted?.Invoke(); //? hier essentiell, da der Programmierer auf der anderen Seite möglicherweise keine Funktion hier anhängt
	}
}