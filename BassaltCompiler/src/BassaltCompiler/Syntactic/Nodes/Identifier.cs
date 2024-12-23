
using System.Collections.Generic;
using System.Linq;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class Identifier : IDebuggable
	{
		public string Name { get; }

		public Identifier(string name)
		{
			Name = name;
		}

		string IDebuggable.StringTreeName()
		{
			return $"Identifier('{Name}')";
		}

		IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
		{
			return null;
		}
	}
}
