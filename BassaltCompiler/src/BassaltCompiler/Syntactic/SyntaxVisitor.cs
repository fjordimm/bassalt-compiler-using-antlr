
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
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

		protected override List<object> AggregateResult(object aggregate, object nextResult)
		{
			if (aggregate is null)
			{
				return new List<object>{ nextResult };
			}
			else
			{
				List<object> aggregateR = aggregate as List<object>;
				Debug.Assert(aggregateR is not null);

				aggregateR.Add(nextResult);
				return aggregateR;
			}
		}

		public override string VisitTerminal(ITerminalNode node)
		{
			return $"Terminal{{{node.GetText()}}}";
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
			List<object> childrenNodes = base.VisitStatementPrint(context) as List<object>;
			
			Console.WriteLine("children...");
			foreach (object child in childrenNodes)
			{ Console.WriteLine($"  {child}"); }

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

		public override Literal VisitLiteral([NotNull] BassaltParser.LiteralContext context)
		{
			BassaltParser.LiteralBooleanContext literalBoolean = context.literalBoolean();
			BassaltParser.LiteralIntegerContext literalInteger = context.literalInteger();
			BassaltParser.LiteralFractionalContext literalFractional = context.literalFractional();
			BassaltParser.LiteralStringContext literalString = context.literalString();

			Literal ret = null;

			if (literalBoolean is not null)
			{
				ret = new Literal(LiteralType.Boolean, literalBoolean.GetText());
			}
			else if (literalInteger is not null)
			{
				ITerminalNode decInt = literalInteger.DecIntLiteral();
				ITerminalNode hexInt = literalInteger.HexIntLiteral();
				ITerminalNode octalInt = literalInteger.OctalIntLiteral();
				ITerminalNode binaryInt = literalInteger.BinaryIntLiteral();
				ITerminalNode charr = literalInteger.CharLiteral();

				if (decInt is not null)
				{ ret = Reparsing.ReparseDecInt(decInt.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
				else if (hexInt is not null)
				{ ret = Reparsing.ReparseHexInt(hexInt.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
				else if (octalInt is not null)
				{ ret = Reparsing.ReparseOctalInt(octalInt.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
				else if (binaryInt is not null)
				{ ret = Reparsing.ReparseBinaryInt(binaryInt.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
				else if (charr is not null)
				{ ret = Reparsing.ReparseChar(charr.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
			}
			else if (literalFractional is not null)
			{
				ITerminalNode plainFrac = literalFractional.PlainFracLiteral();
				ITerminalNode scientificFrac = literalFractional.ScientificFracLiteral();
				ITerminalNode scientificWholeNum = literalFractional.ScientificWholeNumLiteral();

				if (plainFrac is not null)
				{ ret = Reparsing.ReparsePlainFrac(plainFrac.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
				else if (scientificFrac is not null)
				{ ret = Reparsing.ReparseScientificFrac(scientificFrac.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
				else if (scientificWholeNum is not null)
				{ ret = Reparsing.ReparseScientificWholeNum(scientificWholeNum.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
			}
			else if (literalString is not null)
			{
				ITerminalNode str = literalString.StringLiteral();

				if (str is not null)
				{ ret = Reparsing.ReparseString(str.GetText(), bassaltSyntaxErrorHandler, context.Start.Line, context.Start.Column); }
			}

			base.VisitLiteral(context);

			if (ret is null)
			{ throw new ArgumentException("This should not happen."); }

			return ret;
		}
	}
}
