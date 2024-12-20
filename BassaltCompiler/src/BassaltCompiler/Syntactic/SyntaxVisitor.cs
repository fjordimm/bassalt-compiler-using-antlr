
using System;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using BassaltCompiler.Syntactic.Nodes;

namespace BassaltCompiler.Syntactic
{
	class SyntaxVisitor : BassaltBaseVisitor<object>
	{
		private readonly SyntaxTree syntaxTree;

		public SyntaxVisitor()
		{
			syntaxTree = new SyntaxTree();
		}

		public SyntaxTree GetSyntaxTree()
		{
			return syntaxTree;
		}

		// public override object VisitInvalid([NotNull] BassaltParser.InvalidContext context)
		// {
		// 	Console.WriteLine("AAAAHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHh");
		// 	return base.VisitInvalid(context);
		// }

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
			



			Console.WriteLine($"ahhhhhh ::: {context.literal().literalInteger().GetText()}");
			// outFile.WriteLine($"printf(\"%d\\n\", {context.ConstantDecInt().GetText()});");

			base.VisitStatementPrint(context);

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

			Console.WriteLine(ret);

			base.VisitLiteral(context);

			return ret;
		}
	}
}
