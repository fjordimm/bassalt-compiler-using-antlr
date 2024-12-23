
using System.Linq;
using System.Text;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	enum BinaryOp
	{
		Or, And, BitOr, BitXor, BitAnd, Equals, NotEquals, LessThan, GreaterThan, LessOrEqual, GreaterOrEqual, BitShiftLeft, BitShiftRight, BitLogicalShiftLeft, BitLogicalShiftRight, Plus, Minus, Times, Div, Mod, Dot, Tilde, DoubleColon
	}

	class ExprBinaryOp : Expr
	{
		public string Op { get; }
		public IDebugStringable Lhs { get; }
		public IDebugStringable Rhs { get; }

		public ExprBinaryOp(Terminal op, IDebugStringable lhs, IDebugStringable rhs)
		{
			Op = op.Text;
			Lhs = lhs;
			Rhs = rhs;
		}

		protected override string ToString1(int indent)
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(string.Concat(Enumerable.Repeat(" ", indent)) + $"BinaryOp({Op})");
			ret.Append("\n" + Lhs.ToString(indent + 2));
			ret.Append("\n" + Rhs.ToString(indent + 2));

			return ret.ToString();
		}
	}
}
