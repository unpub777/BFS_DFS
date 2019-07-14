using System;
using System.Collections.Generic;
using System.IO;

namespace BFS_DFS
{
    class Program
    {
		private static readonly List<Node> _inputList = new List<Node>();
        static void Main(string[] args)
        {
			if (args.Length == 0 || args.Length > 2)
			{
				Console.WriteLine("Format:bfs_dfs.exe <inputfile> [bfs|dfs]");
				return;
			}

			List<ISearchService> variants = new List<ISearchService>();

			if (args.Length == 2)
			{
				switch (args[1])
				{
					case "dfs":
						variants.Add(new DFS());
						break;
					case "bfs":
						variants.Add(new BFS());
						break;
					default:
						variants.Add(new DFS());
						variants.Add(new BFS());
						break;
				}
			}
			else
			{
				variants.Add(new DFS());
				variants.Add(new BFS());
			}

			try
			{
				Process(args[0], Path.GetDirectoryName(args[0]), variants);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private static void Process(string inputFile, string outputFile, IEnumerable<ISearchService> variants)
		{
			var tree = BuildTree(inputFile);

			using (StreamWriter outputStream = new StreamWriter($"{outputFile}\\output.txt"))
			{
				foreach (var v in variants)
				{
					WriteOutput(outputStream, v.Start(tree));
					outputStream.WriteLine(string.Empty);
				}
			}
		}

		private static void WriteOutput(StreamWriter swriter, IEnumerable<int> result)
		{
			foreach (var value in result)
			{
				swriter.Write($"{value} ");
			}
		}

		private static Node BuildTree(string filePath)
		{
			int counter = -1;
			using (var streamReader = new System.IO.StreamReader(filePath))
			{
				string line = streamReader.ReadLine();
				for (int i = 0; i < int.Parse(line); i++)
				{
					_inputList.Add(new Node() { Childs = new List<Node>() });
				}

				while ((line = streamReader.ReadLine()) != null)
				{
					var elements = line.Split(" ");
					if (elements.Length == 1)
					{
						_inputList[++counter].Value = int.Parse(elements[0]);
					}
					else if (elements.Length == 2)
					{
						var vertex1 = int.Parse(elements[0]);
						var vertex2 = int.Parse(elements[1]);
						if (vertex1 != vertex2)
						{
							_inputList[counter].Childs.Add(_inputList[vertex2 - 1]);
						}
					}
					else
					{
						throw new ArgumentException("Неверный формат входного файла");
					}
				}
			}

			return _inputList[0];
		}
    }
}
