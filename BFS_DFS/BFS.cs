using System;
using System.Collections.Generic;
using System.Text;

namespace BFS_DFS
{
	public class BFS : ISearchService
	{
		private List<int> _result;
		private Queue<Node> _queue;

		public void Iterate(Node tree)
		{
			_queue.Enqueue(tree);

			while(_queue.Count != 0)
			{
				var e = _queue.Dequeue();
				_result.Add(e.Value);
				foreach(var child in e.Childs)
				{
					_queue.Enqueue(child);
				}
			}
		}

		public List<int> Start(Node tree)
		{
			_result = new List<int>();
			_queue = new Queue<Node>();
			Iterate(tree);
			return _result;
		}
	}
}
