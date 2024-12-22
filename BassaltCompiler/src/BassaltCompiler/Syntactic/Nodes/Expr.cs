
using System.Linq;

namespace BassaltCompiler.Syntactic.Nodes
{
	abstract class Expr
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
			return string.Concat(Enumerable.Repeat(" ", indent)) + $"Expr[{EvalType}]: {ToString1(indent)}";
		}

		protected abstract string ToString1(int indent);
	}
}
