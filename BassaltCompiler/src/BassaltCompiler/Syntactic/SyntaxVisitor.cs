
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Antlr4.Runtime;
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
		private readonly IVocabulary lexerVocab;
		private readonly BassaltSyntaxErrorHandler bassaltSyntaxErrorHandler;
		// private readonly BassaltSemanticErrorHandler bassaltSemanticErrorHandler;

		public SyntaxVisitor(IVocabulary vocab, BassaltSyntaxErrorHandler syntaxErrorHandler)
		{
			syntaxTree = new SyntaxTree();
			lexerVocab = vocab;
			bassaltSyntaxErrorHandler = syntaxErrorHandler;
		}

		public SyntaxTree GetSyntaxTree()
		{
			return syntaxTree;
		}

		protected override object AggregateResult(object aggregate, object nextResult)
		{
			if (aggregate is null)
			{
				return nextResult;
			}
			else
			{
				AggregateObj aggregateR = aggregate as AggregateObj;

				if (aggregateR is not null)
				{
					if (nextResult is null)
					{ return null; }

					IDebugStringable nextResultS = nextResult as IDebugStringable;
					System.Diagnostics.Debug.Assert(nextResultS is not null);

					aggregateR.Items.Add(nextResultS);
					return aggregateR;
				}
				else
				{
					if (nextResult is null)
					{ return null; }

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
					ret.Append("\n" + item.ToString(indent + 2));
				}

				return ret.ToString();
			}
		}

		public override Terminal VisitTerminal(ITerminalNode node)
		{
			return new Terminal(node, lexerVocab);
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
			object children = base.VisitStatementPrint(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);
			
			Console.WriteLine("print thingy is...");
			Console.WriteLine(childrenR);

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

		public override object VisitExprSum_plus([NotNull] BassaltParser.ExprSum_plusContext context)
		{
			object children = base.VisitExprSum_plus(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			IDebugStringable lhs = childrenR.Items[0];
			System.Diagnostics.Debug.Assert(lhs is not null);
			Terminal op = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(op is not null);
			IDebugStringable rhs = childrenR.Items[2];
			System.Diagnostics.Debug.Assert(rhs is not null);

			return new ExprBinaryOp(op, lhs, rhs);
		}

		public override Identifier VisitExprBase_identifier([NotNull] BassaltParser.ExprBase_identifierContext context)
		{
			Identifier ret = new Identifier(context.Identifier().GetText());
			base.VisitExprBase_identifier(context);
			return ret;
		}

		public override object VisitExprBase_literal([NotNull] BassaltParser.ExprBase_literalContext context)
		{
			return base.VisitExprBase_literal(context);
		}

		public override object VisitExprBase_parenthesis([NotNull] BassaltParser.ExprBase_parenthesisContext context)
		{
			object children = base.VisitExprBase_parenthesis(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			Terminal openParen = childrenR.Items[0] as Terminal;
			System.Diagnostics.Debug.Assert(openParen is not null);
			IDebugStringable innerExpr = childrenR.Items[1] as IDebugStringable;
			System.Diagnostics.Debug.Assert(innerExpr is not null);
			Terminal closeParen = childrenR.Items[2] as Terminal;
			System.Diagnostics.Debug.Assert(closeParen is not null);

			return innerExpr;
		}

		public override Literal VisitLiteral_boolean([NotNull] BassaltParser.Literal_booleanContext context)
		{
			Literal ret = new Literal(LiteralType.Boolean, context.literalBoolean().GetText());
			base.VisitLiteral_boolean(context);
			return ret;
		}

		public override Literal VisitLiteral_integer([NotNull] BassaltParser.Literal_integerContext context)
		{
			Literal ret = base.VisitLiteral_integer(context) as Literal;
			System.Diagnostics.Debug.Assert(ret is not null);

			return ret;
		}

		public override Literal VisitLiteralInteger_decInt([NotNull] BassaltParser.LiteralInteger_decIntContext context)
		{
			Literal ret = Reparsing.ReparseDecInt(context.DecIntLiteral().GetText(), bassaltSyntaxErrorHandler, context.Stop.Line, context.Stop.Column);
			base.VisitLiteralInteger_decInt(context);
			return ret;
		}

		public override Literal VisitLiteralInteger_hexInt([NotNull] BassaltParser.LiteralInteger_hexIntContext context)
		{
			Literal ret = Reparsing.ReparseHexInt(context.HexIntLiteral().GetText(), bassaltSyntaxErrorHandler, context.Stop.Line, context.Stop.Column);
			base.VisitLiteralInteger_hexInt(context);
			return ret;
		}

		public override Literal VisitLiteralInteger_octalInt([NotNull] BassaltParser.LiteralInteger_octalIntContext context)
		{
			Literal ret = Reparsing.ReparseOctalInt(context.OctalIntLiteral().GetText(), bassaltSyntaxErrorHandler, context.Stop.Line, context.Stop.Column);
			base.VisitLiteralInteger_octalInt(context);
			return ret;
		}

		public override Literal VisitLiteralInteger_binaryInt([NotNull] BassaltParser.LiteralInteger_binaryIntContext context)
		{
			Literal ret = Reparsing.ReparseBinaryInt(context.BinaryIntLiteral().GetText(), bassaltSyntaxErrorHandler, context.Stop.Line, context.Stop.Column);
			base.VisitLiteralInteger_binaryInt(context);
			return ret;
		}

		public override Literal VisitLiteralInteger_char([NotNull] BassaltParser.LiteralInteger_charContext context)
		{
			Literal ret = Reparsing.ReparseChar(context.CharLiteral().GetText(), bassaltSyntaxErrorHandler, context.Stop.Line, context.Stop.Column);
			base.VisitLiteralInteger_char(context);
			return ret;
		}
	}
}
