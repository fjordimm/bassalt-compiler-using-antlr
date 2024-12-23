
using System.Linq;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class Identifier : IDebugStringable
	{
		public string Name { get; }

		public Identifier(string name)
		{
			Name = name;
		}

		public override string ToString()
		{
			return ToString(0);
		}

		public string ToString(int indent)
		{
			return string.Concat(Enumerable.Repeat(" ", indent)) + $"Identifier('{Name}')";
		}
	}
}
