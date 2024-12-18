
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

			Console.WriteLine(parser.root());
			System.Environment.Exit(0);
			
			// IParseTree tree = parser.program();

			// GenerateIntermediateC generator = new GenerateIntermediateC(outFile);
			// generator.Visit(tree);
		}
	}
}
