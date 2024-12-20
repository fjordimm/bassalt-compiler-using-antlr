
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using BassaltCompiler.Syntactic;

namespace BassaltCompiler
{
	class Program
	{
		const string inFilePath = @"../TestIO/in/in.bslt";
		const string outFilePath = @"../TestIO/out/out.c";
		
		static void Main(string[] args)
		{
			// System.Environment.Exit(1);

			(string, TextReader)[] inFiles = new (string, TextReader)[]
			{ (inFilePath, File.OpenText(inFilePath)) };
			
			using TextWriter outFile = new StreamWriter(File.Open(outFilePath, FileMode.Create));

			Compiler.Compile(inFiles, outFile, Console.Out);
		}
	}
}
