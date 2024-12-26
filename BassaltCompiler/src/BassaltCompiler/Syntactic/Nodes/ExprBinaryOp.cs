
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class ExprBinaryOp : Expr
	{
		public string Op { get; }
		public IDebuggable Lhs { get; }
		public IDebuggable Rhs { get; }

		public ExprBinaryOp(SyntaxVisitor.DebuggableTerminal op, IDebuggable lhs, IDebuggable rhs)
		{
			Op = op.Text;
			Lhs = lhs;
			Rhs = rhs;
		}

		protected override string StringTreeName1()
		{
			return $"BinaryOp({Op})";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			return new List<IDebuggable>{ Lhs, Rhs };
		}
	}
}
