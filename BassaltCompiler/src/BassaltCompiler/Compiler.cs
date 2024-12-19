
using System;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using BassaltCompiler.Syntactic;

namespace BassaltCompiler
{
	static class Compiler
	{
		public static void Compile(TextReader inFile, TextWriter outFile)
		{
			AntlrInputStream input = new AntlrInputStream(inFile);
			BassaltLexer lexer = new BassaltLexer(input);
			CommonTokenStream tokens = new CommonTokenStream(lexer);
			BassaltParser parser = new BassaltParser(tokens);

			// PrintAllTokens(lexer, tokens, parser);
			// System.Environment.Exit(0);
			
			IParseTree parseTree = parser.program();

			SyntaxVisitor visitor = new SyntaxVisitor();
			visitor.Visit(parseTree);
			SyntaxTree syntaxTree = visitor.GetSyntaxTree();

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
