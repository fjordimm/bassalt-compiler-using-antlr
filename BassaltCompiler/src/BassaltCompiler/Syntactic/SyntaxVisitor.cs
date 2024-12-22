
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using BassaltCompiler.Debug;
using BassaltCompiler.ErrorHandling;
using BassaltCompiler.Syntactic.Nodes;

namespace BassaltCompiler.Syntactic
{
	class SyntaxVisitor : BassaltBaseVisitor<object>
	{
		private readonly SyntaxTree syntaxTree;
		private readonly BassaltSyntaxErrorHandler bassaltSyntaxErrorHandler;
		private readonly BassaltSemanticErrorHandler bassaltSemanticErrorHandler;

		public SyntaxVisitor(BassaltSyntaxErrorHandler syntaxErrorHandler, BassaltSemanticErrorHandler semanticErrorHandler)
		{
			syntaxTree = new SyntaxTree();
			bassaltSyntaxErrorHandler = syntaxErrorHandler;
			bassaltSemanticErrorHandler = semanticErrorHandler;
		}

		public SyntaxTree GetSyntaxTree()
		{
			return syntaxTree;
		}

		protected override object AggregateResult(object aggregate, object nextResult)
		{
			if (aggregate is null)
			{
				// Console.WriteLine($"heoheoeh: {nextResult}");
				// System.Environment.Exit(1);

				// IDebugStringable nextResultS = nextResult as IDebugStringable;
				// System.Diagnostics.Debug.Assert(nextResultS is not null);

				// AggregateObj ret = new AggregateObj();
				// ret.Items.Add(nextResultS);
				// return ret;

				return nextResult;
			}

			AggregateObj aggregateR = aggregate as AggregateObj;

			if (aggregateR is not null)
			{
				IDebugStringable nextResultS = nextResult as IDebugStringable;
				System.Diagnostics.Debug.Assert(nextResultS is not null);

				aggregateR.Items.Add(nextResultS);
				return aggregateR;
			}
			else
			{
				IDebugStringable aggregateS = aggregate as IDebugStringable;
				System.Diagnostics.Debug.Assert(aggregateS is not null);

				IDebugStringable nextResultS = nextResult as IDebugStringable;
				System.Diagnostics.Debug.Assert(nextResultS is not null);

				AggregateObj ret = new AggregateObj();
				ret.Items.Add(aggregateS);
				ret.Items.Add(nextResultS);

				return ret;
			}
		}

		protected class AggregateObj : IDebugStringable
		{
			public List<IDebugStringable> Items { get; }

			public AggregateObj()
			{
				Items = new List<IDebugStringable>();
			}

			public override string ToString()
			{
				return ToString(0);
			}

			public string ToString(int indent)
			{
				StringBuilder ret = new StringBuilder();

				ret.Append(string.Concat(Enumerable.Repeat(" ", indent)) + "Aggregate");
				foreach (IDebugStringable item in Items)
				{
					ret.Append("\n" + item.ToString(indent+2));
				}

				return ret.ToString();
			}
		}

		public override Terminal VisitTerminal(ITerminalNode node)
		{
			return new Terminal(node.GetText());
		}

		public override object VisitProgram([NotNull] BassaltParser.ProgramContext context)
		{
			// outFile.WriteLine("#include <stdio.h>");
			// outFile.WriteLine("int main(void) {");

			base.VisitProgram(context);

			// outFile.WriteLine("return 0;");
			// outFile.WriteLine("}");

			return null;
		}

		public override object VisitStatementPrint([NotNull] BassaltParser.StatementPrintContext context)
		{
			AggregateObj childrenNodes = base.VisitStatementPrint(context) as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenNodes is not null);
			
			Console.WriteLine("children...");
			foreach (IDebugStringable child in childrenNodes.Items)
			{
				Console.WriteLine(child.ToString(2));
			}

			// AggregateObj the = childrenNodes[1] as List<object>;

			// Console.WriteLine("the...");
			// foreach (object item in the)
			// {
			// 	Console.WriteLine($"  {the}");
			// }

			return null;
		}

		public override object VisitExpr([NotNull] BassaltParser.ExprContext context)
		{
			return base.VisitExpr(context);
		}

		public override object VisitExprLambda([NotNull] BassaltParser.ExprLambdaContext context)
		{
			return base.VisitExprLambda(context);
		}

		public override object VisitExprBase_identifier([NotNull] BassaltParser.ExprBase_identifierContext context)
		{
			return base.VisitExprBase_identifier(context);
		}

		public override Literal VisitLiteral_boolean([NotNull] BassaltParser.Literal_booleanContext context)
		{
			Literal ret = new Literal(LiteralType.Boolean, context.literalBoolean().GetText());
			base.VisitLiteral_boolean(context);
			return ret;
		}

		public override Literal VisitLiteral_integer([NotNull] BassaltParser.Literal_integerContext context)
		{
			// Console.WriteLine($"howdy: {base.VisitLiteral_integer(context)}");
			// System.Environment.Exit(1);

			Literal ret = base.VisitLiteral_integer(context) as Literal;
			System.Diagnostics.Debug.Assert(ret is not null);

			return ret;
		}

		public override Literal VisitLiteralInteger_decInt([NotNull] BassaltParser.LiteralInteger_decIntContext context)
		{
			Literal ret = Reparsing.ReparseDecInt(context.DecIntLiteral().GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column);
			base.VisitLiteralInteger_decInt(context);
			return ret;
		}

		public override Literal VisitLiteralInteger_hexInt([NotNull] BassaltParser.LiteralInteger_hexIntContext context)
		{
			Literal ret = Reparsing.ReparseHexInt(context.HexIntLiteral().GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column);
			base.VisitLiteralInteger_hexInt(context);
			return ret;
		}

		public override Literal VisitLiteralInteger_octalInt([NotNull] BassaltParser.LiteralInteger_octalIntContext context)
		{
			Literal ret = Reparsing.ReparseOctalInt(context.OctalIntLiteral().GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column);
			base.VisitLiteralInteger_octalInt(context);
			return ret;
		}

		public override Literal VisitLiteralInteger_binaryInt([NotNull] BassaltParser.LiteralInteger_binaryIntContext context)
		{
			Literal ret = Reparsing.ReparseBinaryInt(context.BinaryIntLiteral().GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column);
			base.VisitLiteralInteger_binaryInt(context);
			return ret;
		}

		public override Literal VisitLiteralInteger_char([NotNull] BassaltParser.LiteralInteger_charContext context)
		{
			Literal ret = Reparsing.ReparseChar(context.CharLiteral().GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column);
			base.VisitLiteralInteger_char(context);
			return ret;
		}

		// public override Literal VisitLiteral([NotNull] BassaltParser.LiteralContext context)
		// {
		// 	BassaltParser.LiteralBooleanContext literalBoolean = context.literalBoolean();
		// 	BassaltParser.LiteralIntegerContext literalInteger = context.literalInteger();
		// 	BassaltParser.LiteralFractionalContext literalFractional = context.literalFractional();
		// 	BassaltParser.LiteralStringContext literalString = context.literalString();

		// 	Literal ret = null;

		// 	if (literalBoolean is not null)
		// 	{
		// 		ret = new Literal(LiteralType.Boolean, literalBoolean.GetText());
		// 	}
		// 	else if (literalInteger is not null)
		// 	{
		// 		ITerminalNode decInt = literalInteger.DecIntLiteral();
		// 		ITerminalNode hexInt = literalInteger.HexIntLiteral();
		// 		ITerminalNode octalInt = literalInteger.OctalIntLiteral();
		// 		ITerminalNode binaryInt = literalInteger.BinaryIntLiteral();
		// 		ITerminalNode charr = literalInteger.CharLiteral();

		// 		if (decInt is not null)
		// 		{ ret = Reparsing.ReparseDecInt(decInt.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
		// 		else if (hexInt is not null)
		// 		{ ret = Reparsing.ReparseHexInt(hexInt.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
		// 		else if (octalInt is not null)
		// 		{ ret = Reparsing.ReparseOctalInt(octalInt.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
		// 		else if (binaryInt is not null)
		// 		{ ret = Reparsing.ReparseBinaryInt(binaryInt.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
		// 		else if (charr is not null)
		// 		{ ret = Reparsing.ReparseChar(charr.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
		// 	}
		// 	else if (literalFractional is not null)
		// 	{
		// 		ITerminalNode plainFrac = literalFractional.PlainFracLiteral();
		// 		ITerminalNode scientificFrac = literalFractional.ScientificFracLiteral();
		// 		ITerminalNode scientificWholeNum = literalFractional.ScientificWholeNumLiteral();

		// 		if (plainFrac is not null)
		// 		{ ret = Reparsing.ReparsePlainFrac(plainFrac.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
		// 		else if (scientificFrac is not null)
		// 		{ ret = Reparsing.ReparseScientificFrac(scientificFrac.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
		// 		else if (scientificWholeNum is not null)
		// 		{ ret = Reparsing.ReparseScientificWholeNum(scientificWholeNum.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
		// 	}
		// 	else if (literalString is not null)
		// 	{
		// 		ITerminalNode str = literalString.StringLiteral();

		// 		if (str is not null)
		// 		{ ret = Reparsing.ReparseString(str.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
		// 	}

		// 	base.VisitLiteral(context);

		// 	if (ret is null)
		// 	{ throw new ArgumentException("This should not happen."); }

		// 	return ret;
		// }
	}
}
