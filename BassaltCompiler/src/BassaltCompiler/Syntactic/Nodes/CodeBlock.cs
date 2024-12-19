
using System.Collections.Generic;

namespace BassaltCompiler.Syntactic.Nodes
{
	class CodeBlock
	{
		public List<Statement> Statements { get; }

		public CodeBlock()
		{
			Statements = new List<Statement>();
		}
	}
}
