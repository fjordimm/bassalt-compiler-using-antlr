
using System.Linq;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	abstract class Expr : IDebugStringable
	{
		public Types.Datatype EvalType { get; set; }

		public Expr()
		{
			EvalType = Types.TUnset;
		}

		public sealed override string ToString()
		{
			return ToString(0);
		}

		public string ToString(int indent)
		{
			return string.Concat(Enumerable.Repeat(" ", indent)) + $"Expr::{ToStringName()} with evalType={EvalType}{ToStringChildren(indent + 2)}";
		}

		protected abstract string ToStringName();
		protected abstract string ToStringChildren(int indent);
	}
}
