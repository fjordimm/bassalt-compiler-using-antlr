
using System.Collections.Generic;
using System.Linq;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class ExprIdentifier : Expr
	{
		public string Name { get; }

		public ExprIdentifier(string name)
		{
			Name = name;
		}

		protected override string StringTreeName1()
		{
			return $"Identifier('{Name}')";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			return null;
		}
	}
}
