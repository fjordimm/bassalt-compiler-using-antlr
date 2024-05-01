
using System;
using System.IO;

namespace BassaltCompiler
{
	class Program
	{
		const string inFilePath = @"..\TestIO\in\in.bslt";
		
		static void Main(string[] args)
		{
			using TextReader inFile = File.OpenText(inFilePath);
			Console.WriteLine(inFile.ReadToEnd());
		}
	}
}
