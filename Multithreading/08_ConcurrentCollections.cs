using System.Collections.Concurrent;

namespace Multithreading;

internal class _08_ConcurrentCollections
{
	static void Main(string[] args)
	{
		ConcurrentBag<int> list = new();
		list.Add(1);

		ConcurrentDictionary<string, int> dict = new();
		dict.TryAdd("a", 1);
		dict.TryAdd("b", 2);
		dict.TryAdd("c", 3);

		foreach (KeyValuePair<string, int> kv in dict)
		{
            Console.WriteLine(kv);
        }
	}
}
