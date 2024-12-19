
using System;
using Antlr4.Runtime.Misc;
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
			{ ret = new Literal(LiteralType.Boolean, literalBoolean.LiteralBool().GetText()); }
			else if (literalInteger is not null)
			{ ret = new Literal(LiteralType.Integer, "uh"); }

			base.VisitLiteral(context);

			return ret;
		}
	}
}
