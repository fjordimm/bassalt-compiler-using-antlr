
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
			outFile.WriteLine("int main(void) {");
			base.VisitProgram(context);
			outFile.WriteLine("return 0; }");

			return null;
		}

		public override object VisitUnit([NotNull] BassaltParser.UnitContext context)
		{
			outFile.WriteLine($"  unito({context.GetText()})");
			base.VisitUnit(context);

			return null;
		}
	}
}
