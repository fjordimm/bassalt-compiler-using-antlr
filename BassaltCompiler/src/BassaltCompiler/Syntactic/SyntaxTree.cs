
using BassaltCompiler.Syntactic.Nodes;

namespace BassaltCompiler.Syntactic
{
	class SyntaxTree
	{
		public CodeBlock MainCodeBlock { get; }

		public SyntaxTree()
		{
			MainCodeBlock = new CodeBlock();
		}
	}
}
