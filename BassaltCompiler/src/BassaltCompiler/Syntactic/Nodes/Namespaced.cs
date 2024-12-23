
using System.Collections.Generic;
using System.Linq;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class Namespaced : IDebuggable
	{
		public string Namespace { get; }
		public IDebuggable Item { get; }

		public Namespaced(string namespacee, IDebuggable item)
		{
			Namespace = namespacee;
			Item = item;
		}

		string IDebuggable.StringTreeName()
		{
			return $"Namespaced('{Namespace}')";
		}

		IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
		{
			return new List<IDebuggable>{ Item };
		}
	}
}
