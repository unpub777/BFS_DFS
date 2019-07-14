using System.Collections.Generic;

namespace BFS_DFS
{
	public class DFS : ISearchService
	{
		private List<int> _result;
		public void Iterate(Node tree)
		{
			if (tree != null)
			{
				_result.Add(tree.Value);
				if (tree.Childs.Count > 0)
				{
					foreach(var child in tree.Childs)
					{
						Iterate(child);
					}
				}
			}
		}

		public List<int> Start(Node tree)
		{
			_result = new List<int>();
			Iterate(tree);
			return _result;
		}
	}
}
