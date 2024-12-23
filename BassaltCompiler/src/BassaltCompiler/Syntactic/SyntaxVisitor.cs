
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
		////////////////////////////////////////////////////////////////////////
		
		///// Class Stuff /////

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

					IDebuggable nextResultS = nextResult as IDebuggable;
					System.Diagnostics.Debug.Assert(nextResultS is not null);

					aggregateR.Items.Add(nextResultS);
					return aggregateR;
				}
				else
				{
					if (nextResult is null)
					{ return null; }

					IDebuggable aggregateS = aggregate as IDebuggable;
					System.Diagnostics.Debug.Assert(aggregateS is not null);

					IDebuggable nextResultS = nextResult as IDebuggable;
					System.Diagnostics.Debug.Assert(nextResultS is not null);

					AggregateObj ret = new AggregateObj();
					ret.Items.Add(aggregateS);
					ret.Items.Add(nextResultS);

					return ret;
				}
			}
		}

		public class AggregateObj : IDebuggable
		{
			public List<IDebuggable> Items { get; }

			public AggregateObj()
			{
				Items = new List<IDebuggable>();
			}

			string IDebuggable.StringTreeName()
			{
				return "AGGREGATE";
			}

			IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
			{
				return Items;
			}
		}

		////////////////////////////////////////////////////////////////////////
		
		///// Terminals /////

		public override Terminal VisitTerminal(ITerminalNode node)
		{
			return new Terminal(node, lexerVocab);
		}

		////////////////////////////////////////////////////////////////////////
		
		///// Useful Things /////
		
		public override object VisitLangDatatype([NotNull] BassaltParser.LangDatatypeContext context)
		{
			return base.VisitLangDatatype(context);
		}
		
		////////////////////////////////////////////////////////////////////////
		
		///// Literals /////

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

		////////////////////////////////////////////////////////////////////////
		
		///// Expressions /////

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

			IDebuggable lhs = childrenR.Items[0];
			System.Diagnostics.Debug.Assert(lhs is not null);
			Terminal op = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(op is not null);
			IDebuggable rhs = childrenR.Items[2];
			System.Diagnostics.Debug.Assert(rhs is not null);

			return new ExprBinaryOp(op, lhs, rhs);
		}

		// public override object VisitExprNamespaceRes_main([NotNull] BassaltParser.ExprNamespaceRes_mainContext context)
		// {
		// 	object children = base.VisitExprNamespaceRes_main(context);
		// 	if (children is null)
		// 	{
		// 		bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
		// 		return null;
		// 	}

		// 	AggregateObj childrenR = children as AggregateObj;
		// 	System.Diagnostics.Debug.Assert(childrenR is not null);


		// }

		public override object VisitExprNamespaceRes_other([NotNull] BassaltParser.ExprNamespaceRes_otherContext context)
		{
			return base.VisitExprNamespaceRes_other(context);
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
			IDebuggable innerExpr = childrenR.Items[1] as IDebuggable;
			System.Diagnostics.Debug.Assert(innerExpr is not null);
			Terminal closeParen = childrenR.Items[2] as Terminal;
			System.Diagnostics.Debug.Assert(closeParen is not null);

			return innerExpr;
		}

		////////////////////////////////////////////////////////////////////////
		
		///// Statements /////

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
			Console.WriteLine(IDebuggable.ToStringTree(childrenR));

			return null;
		}

		////////////////////////////////////////////////////////////////////////
	}
}
