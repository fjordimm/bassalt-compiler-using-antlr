
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class ExprDriftingDatatype : Expr
	{
		public Datatype Val { get; }

		public ExprDriftingDatatype(Datatype val)
		{
			Val = val;
		}

		protected override string StringTreeName1()
		{
			return $"DriftingDatatype";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			return new List<IDebuggable>{ Val };
		}
	}
}
