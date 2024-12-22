
using System.Linq;

namespace BassaltCompiler.Syntactic.Nodes
{
	class Identifier
	{
		public string Name { get; }

		public Identifier(string name)
		{
			Name = name;
		}
	}
}
