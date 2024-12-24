
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class ExprExplicitCast : Expr
	{
		public Datatype TargetType { get; }
		public IDebuggable Inner { get; }

		public ExprExplicitCast(Datatype targetType, IDebuggable inner)
		{
			TargetType = targetType;
			Inner = inner;
		}

		protected override string StringTreeName1()
		{
			return $"ExplicitCast('{TargetType}')";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			return new List<IDebuggable>{ Inner };
		}
	}
}
