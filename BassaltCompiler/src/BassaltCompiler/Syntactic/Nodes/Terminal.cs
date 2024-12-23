
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class Terminal : IDebuggable
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

		string IDebuggable.StringTreeName()
		{
			return $"Terminal({Type}, '{Text}')";
		}

		IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
		{
			return null;
		}
	}
}
