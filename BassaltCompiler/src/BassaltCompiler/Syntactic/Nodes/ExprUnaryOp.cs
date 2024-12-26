
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class ExprUnaryOp : Expr
	{
		public string Op { get; }
		public IDebuggable Inner { get; }

		public ExprUnaryOp(SyntaxVisitor.DebuggableTerminal op, IDebuggable inner)
		{
			Op = op.Text;
			Inner = inner;
		}

		protected override string StringTreeName1()
		{
			return $"UnaryOp({Op})";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			return new List<IDebuggable>{ Inner };
		}
	}
}
