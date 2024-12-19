
using System;
using System.Text.RegularExpressions;
using BassaltCompiler.Syntactic.Nodes;

namespace BassaltCompiler.Syntactic
{
	static class Reparsing
	{
		public static Literal ReparseDecInt(string input)
		{
			bool isNegative = false;
			string value = null;
			string suffixStr = null;

			Regex regex = new Regex(@"([-+]?)([0-9]+)(?:_?([a-zA-Z0-9]))?");
			Match match = regex.Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid"); }
			else
			{
				isNegative = match.Groups[1].Value == "-";
				value = match.Groups[2].Value;
				suffixStr = match.Groups[3].Value;
			}

			Console.WriteLine(isNegative);
			Console.WriteLine(value);
			Console.WriteLine(suffixStr);

			return new Literal(LiteralType.Integer, value, isNegative: isNegative, suffixStr: suffixStr);
		}
	}
}
