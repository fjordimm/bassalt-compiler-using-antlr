
using System.Collections.Generic;
using System.Linq;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	abstract class Expr : IDebuggable
	{
		public Datatype EvalType { get; set; }

		public Expr()
		{
			EvalType = Datatype.DtUnset;
		}

		string IDebuggable.StringTreeName()
		{
			return $"Expr.{StringTreeName1()} with type [{EvalType}]";
		}

		protected abstract string StringTreeName1();

		IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
		{
			return StringTreeChildren1();
		}

		protected abstract IReadOnlyList<IDebuggable> StringTreeChildren1();
	}
}
