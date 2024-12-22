
using System.Linq;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class Terminal : IDebugStringable
	{
		public string Text { get; }

		public Terminal(string text)
		{
			Text = text;
		}

		public override string ToString()
		{
			return ToString(0);
		}

		public string ToString(int indent)
		{
			return string.Concat(Enumerable.Repeat(" ", indent)) + $"Terminal('{Text}')";
		}
	}
}
