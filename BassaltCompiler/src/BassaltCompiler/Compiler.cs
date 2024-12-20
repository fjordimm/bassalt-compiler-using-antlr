
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using BassaltCompiler.ErrorHandling;
using BassaltCompiler.Syntactic;

namespace BassaltCompiler
{
	static class Compiler
	{
		public static void Compile(IEnumerable<(string, TextReader)> inFiles, TextWriter outFile, TextWriter errorOut)
		{
			foreach ((string filename, TextReader reader) in inFiles)
			{
				Compile(filename, reader, outFile, errorOut);
			}
		}
		
		private static bool Compile(string inFilename, TextReader inFile, TextWriter outFile, TextWriter errorOut)
		{
			BassaltSyntaxErrorHandler bassaltSyntaxErrorHandler = new BassaltSyntaxErrorHandler();
			BassaltSemanticErrorHandler bassaltSemanticErrorHandler = new BassaltSemanticErrorHandler();

			AntlrInputStream input = new AntlrInputStream(inFile);
			BassaltLexer lexer = new BassaltLexer(input);
			CommonTokenStream tokens = new CommonTokenStream(lexer);
			BassaltParser parser = new BassaltParser(tokens);
			parser.RemoveErrorListeners();
			parser.AddErrorListener(new BassaltSyntaxErrorListener(bassaltSyntaxErrorHandler));
			
			SyntaxVisitor syntaxVisitor = new SyntaxVisitor(bassaltSyntaxErrorHandler, bassaltSemanticErrorHandler);
			IParseTree parseTree = parser.program();
			syntaxVisitor.Visit(parseTree);
			if (bassaltSyntaxErrorHandler.Errors.Count > 0)
			{
				bassaltSyntaxErrorHandler.PrintErrors(inFilename, errorOut);
				return false;
			}

			SyntaxTree syntaxTree = syntaxVisitor.GetSyntaxTree();

			return true;
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
