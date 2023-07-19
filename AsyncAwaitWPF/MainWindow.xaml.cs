using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncAwaitWPF
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Progress.Value = 0;
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(25);
				Progress.Value++;
			} //UI Updates finden am Main Thread statt, Main Thread wird blockiert
		}

		private void Button_Click1(object sender, RoutedEventArgs e)
		{
			Task.Run(() => //UI Updates sind nicht erlaubt von Side Threads
			{
				Dispatcher.Invoke(() => Progress.Value = 0); //Dispatcher.Invoke: Rufe den Code in der Klammer auf dem Thread auf in dem die Komponente erstellt wurde (hier Window)
				for (int i = 0; i < 100; i++)
				{
					Thread.Sleep(25);
					Dispatcher.Invoke(() => Progress.Value++);
				}
			});
		}

		private async void Button_Click2(object sender, RoutedEventArgs e)
		{
			Progress.Value = 0;
			for (int i = 0; i < 100; i++)
			{
				await Task.Delay(25);
				Progress.Value++;
			}
		}

		private async void Button_Click3(object sender, RoutedEventArgs e)
		{
			using HttpClient client = new HttpClient();
			Task<HttpResponseMessage> resp = client.GetAsync(@"http://www.gutenberg.org/files/54700/54700-0.txt"); //Hier wird der Task gestartet
			//Dinge vor dem Abschluss der Response durchführen (z.B. UI Updates)
			TB.Text = "Text wird geladen...";
			HttpResponseMessage httpResponse = await resp; //Warte hier auf das Ergebnis
			if (httpResponse.IsSuccessStatusCode)
			{
				Task<string> textTask = httpResponse.Content.ReadAsStringAsync();
				//Dinge vor dem Abschluss der Response durchführen (z.B. UI Updates)
				TB.Text = "Text wird angezeigt...";
				await Task.Delay(500);
				string text = await textTask; //Warte hier auf das Ergebnis
				TB.Text = text;
			}
		}

		private async void Button_Click4(object sender, RoutedEventArgs e)
		{
			ConcurrentDictionary<int, string> texte = new();
			for (int i = 0; i < 100; i++)
			{
				StringBuilder sb = new();
				for (int j = 0; j < 1000000; j++)
					sb.Append(j);
				texte[i] = sb.ToString();
			}

			string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
			string folderPath = Path.Combine(desktop, "Test");

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			Stopwatch sw = Stopwatch.StartNew();
			//Normale foreach
			foreach (KeyValuePair<int, string> kv in texte)
			{
				await File.WriteAllTextAsync(Path.Combine(folderPath, $"Test{kv.Key}.txt"), kv.Value);
				TB.Text += $"File geschrieben: {kv.Key}\n";
			}
			sw.Stop();
			TB.Text += $"Normal: {sw.ElapsedMilliseconds}\n";

			//Parallel foreach
			sw.Restart();
			await Parallel.ForEachAsync(texte, (kv, ct) => 
			{
				File.WriteAllText(Path.Combine(folderPath, $"Test{kv.Key}.txt"), kv.Value); //Hier kein await notwendig, da ForEachAsync bereits Asynchron ist
				Dispatcher.Invoke(() => TB.Text += $"File geschrieben: {kv.Key}\n");
				return ValueTask.CompletedTask;			
			});
			sw.Stop();
			TB.Text += $"Parallel: {sw.ElapsedMilliseconds}\n";

			//foreach mit Task List
			sw.Restart();
			List<Task> tasks = new();
			foreach (KeyValuePair<int, string> kv in texte)
			{
				tasks.Add(Task.Run(() =>
				{
					File.WriteAllText(Path.Combine(folderPath, $"Test{kv.Key}.txt"), kv.Value); //Hier kein await notwendig, da Tasks bereits Asynchron sind
					Dispatcher.Invoke(() => TB.Text += $"File geschrieben: {kv.Key}\n");
				}));
			}
			await Task.WhenAll(tasks);
			sw.Stop();
			TB.Text += $"Task List: {sw.ElapsedMilliseconds}\n";
		}
	}
}
