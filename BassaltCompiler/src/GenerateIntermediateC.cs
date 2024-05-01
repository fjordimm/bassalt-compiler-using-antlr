
using System.IO;
using Antlr4.Runtime.Misc;

namespace BassaltCompiler
{
	class GenerateIntermediateC : BassaltBaseVisitor<object>
	{
		private readonly TextWriter outFile;

		public GenerateIntermediateC(TextWriter outFile)
		{
			this.outFile = outFile;
		}

		public override object VisitProgram([NotNull] BassaltParser.ProgramContext context)
		{
			outFile.WriteLine("#include <stdio.h>");
			outFile.WriteLine("int main(void) {");

			base.VisitProgram(context);

			outFile.WriteLine("return 0;");
			outFile.WriteLine("}");

			return null;
		}

		public override object VisitStatementPrint([NotNull] BassaltParser.StatementPrintContext context)
		{
			outFile.WriteLine($"printf(\"%d\\n\", {context.ConstantDecInt().GetText()});");

			base.VisitStatementPrint(context);

			return null;
		}
	}
}
