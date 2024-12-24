
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class ExprConditional : Expr
	{
		public IDebuggable Condition { get; }
		public IDebuggable ExpressionA { get; }
		public IDebuggable ExpressionB { get; }

		public ExprConditional(IDebuggable condition, IDebuggable expressionA, IDebuggable expressionB)
		{
			Condition = condition;
			ExpressionA = expressionA;
			ExpressionB = expressionB;
		}

		protected override string StringTreeName1()
		{
			return "Conditional";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			return new List<IDebuggable>{ Condition, ExpressionA, ExpressionB };
		}
	}
}
