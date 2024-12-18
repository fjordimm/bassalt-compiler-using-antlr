
using System;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace BassaltCompiler
{
	class Program
	{
		const string inFilePath = @"../TestIO/in/in.bslt";
		const string outFilePath = @"../TestIO/out/out.c";
		
		static void Main(string[] args)
		{
			using TextReader inFile = File.OpenText(inFilePath);
			using TextWriter outFile = new StreamWriter(File.Open(outFilePath, FileMode.Create));

			AntlrInputStream input = new AntlrInputStream(inFile);
			BassaltLexer lexer = new BassaltLexer(input);
			CommonTokenStream tokens = new CommonTokenStream(lexer);
			BassaltParser parser = new BassaltParser(tokens);

			PrintAllTokens(lexer, tokens, parser);
			System.Environment.Exit(0);
			
			// IParseTree tree = parser.program();

			// GenerateIntermediateC generator = new GenerateIntermediateC(outFile);
			// generator.Visit(tree);
		}

		private static void PrintAllTokens(BassaltLexer lexer, CommonTokenStream tokens, BassaltParser parser)
		{
			for (IToken tok = lexer.NextToken(); tok.Type != -1; tok = lexer.NextToken())
			{
				Console.WriteLine($"{lexer.Vocabulary.GetSymbolicName(tok.Type), -25} {tok.Text}");
			}
		}
	}
}
