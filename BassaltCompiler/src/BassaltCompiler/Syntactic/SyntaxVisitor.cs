
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

		private readonly BassaltSemanticErrorHandler errorHandler;

		public SyntaxVisitor()
		{
			syntaxTree = new SyntaxTree();
			errorHandler = new BassaltSemanticErrorHandler();
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
			// Console.WriteLine($"ahhhhhh ::: {context.literal().literalInteger().GetText()}");
			// outFile.WriteLine($"printf(\"%d\\n\", {context.ConstantDecInt().GetText()});");

			List<object> thing = base.VisitStatementPrint(context) as List<object>;
			Console.WriteLine($"thing: {thing}");
			foreach (var a in thing)
			{
				Console.WriteLine($"  {a}");
			}

			// this.AggregateResult

			return null;
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
				ret = new Literal(LiteralType.Boolean, literalBoolean.LiteralBool().GetText());
			}
			else if (literalInteger is not null)
			{
				ITerminalNode decInt = literalInteger.LiteralDecInt();
				ITerminalNode hexInt = literalInteger.LiteralHexInt();
				ITerminalNode octalInt = literalInteger.LiteralOctalInt();
				ITerminalNode binaryInt = literalInteger.LiteralBinaryInt();
				ITerminalNode charr = literalInteger.LiteralChar();
				ITerminalNode rgba = literalInteger.LiteralRgba();
				ITerminalNode datetime = literalInteger.LiteralDatetime();

				if (decInt is not null)
				{ ret = Reparsing.ReparseDecInt(decInt.GetText()); }
				else if (hexInt is not null)
				{ ret = Reparsing.ReparseHexInt(hexInt.GetText()); }
				else if (octalInt is not null)
				{ ret = Reparsing.ReparseOctalInt(octalInt.GetText()); }
				else if (binaryInt is not null)
				{ ret = Reparsing.ReparseBinaryInt(binaryInt.GetText()); }
				else if (charr is not null)
				{ /* TODO */ System.Environment.Exit(1); }
				else if (rgba is not null)
				{ /* TODO */ }
				else if (datetime is not null)
				{ /* TODO */ }
			}
			else if (literalFractional is not null)
			{
				ITerminalNode plainFrac = literalFractional.LiteralPlainFrac();
				ITerminalNode scientificFrac = literalFractional.LiteralScientificFrac();
				ITerminalNode scientificWholeNum = literalFractional.LiteralScientificWholeNum();

				if (plainFrac is not null)
				{ ret = Reparsing.ReparsePlainFrac(plainFrac.GetText()); }
				else if (scientificFrac is not null)
				{ ret = Reparsing.ReparseScientificFrac(scientificFrac.GetText()); }
				else if (scientificWholeNum is not null)
				{ ret = Reparsing.ReparseScientificWholeNum(scientificWholeNum.GetText()); }
			}
			else if (literalString is not null)
			{
				// TODO
			}

			// Console.WriteLine(ret);

			base.VisitLiteral(context);

			if (ret is null)
			{
				// errorHandler.Add(context.Start.Line, context.Start.Column, "");
				// return null;
			}

			return ret;
		}
	}
}
