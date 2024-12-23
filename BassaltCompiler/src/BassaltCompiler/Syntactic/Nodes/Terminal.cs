
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class Terminal : IDebugStringable
	{
		public string Type { get; }
		public string Text { get; }

		public Terminal(ITerminalNode node, IVocabulary vocab)
		{
			Type = vocab.GetSymbolicName(node.Symbol.Type);
			if (Type is null)
			{ Type = "unk"; }

			Text = node.GetText();
		}

		public Terminal(string type, string text)
		{
			Type = type;
			Text = text;
		}

		public override string ToString()
		{
			return ToString(0);
		}

		public string ToString(int indent)
		{
			return string.Concat(Enumerable.Repeat(" ", indent)) + $"Terminal({Type}, '{Text}')";
		}
	}
}
