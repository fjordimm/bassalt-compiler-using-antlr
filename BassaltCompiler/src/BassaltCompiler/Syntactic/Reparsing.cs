
using System;
using System.Text.RegularExpressions;
using BassaltCompiler.Syntactic.Nodes;

namespace BassaltCompiler.Syntactic
{
	static partial class Reparsing
	{
		// private static readonly char[] baseChars = "0123456789ABCDEFGHIJKLMNOPQRSTUV"

		public static Literal ReparseBool(string input)
		{
			if (input == "true")
			{ return new Literal(LiteralType.Boolean, "1"); }
			else if (input == "false")
			{ return new Literal(LiteralType.Boolean, "0"); }
			else
			{ throw new ArgumentException("Input must be valid."); }
		}

		[GeneratedRegex(@"([-+]?)([0-9]+)(?:_?([a-zA-Z0-9]))?")]
		private static partial Regex DecIntRegex();

		public static Literal ReparseDecInt(string input)
		{
			Match match = DecIntRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				bool isNegative = match.Groups[1].Value == "-";
				string value = match.Groups[2].Value;
				string suffixStr = match.Groups[3].Value;

				return new Literal(LiteralType.Integer, value, isNegative: isNegative, suffixStr: suffixStr);
			}
		}

		[GeneratedRegex(@"([-+]?)0[xX]([0-9a-fA-F]+)(?:_?([a-zA-Z0-9]))?")]
		private static partial Regex HexIntRegex();

		public static Literal ReparseHexInt(string input)
		{
			Match match = HexIntRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				bool isNegative = match.Groups[1].Value == "-";
				string value = Convert.ToString(Convert.ToUInt64(match.Groups[2].Value, 16));
				string suffixStr = match.Groups[3].Value;
				
				return new Literal(LiteralType.Integer, value, isNegative: isNegative, suffixStr: suffixStr);
			}
		}

		[GeneratedRegex(@"([-+]?)0[oO]([0-7]+)(?:_?([a-zA-Z0-9]))?")]
		private static partial Regex OctalIntRegex();

		public static Literal ReparseOctalInt(string input)
		{
			Match match = OctalIntRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				bool isNegative = match.Groups[1].Value == "-";
				string value = Convert.ToString(Convert.ToUInt64(match.Groups[2].Value, 8));
				string suffixStr = match.Groups[3].Value;
				
				return new Literal(LiteralType.Integer, value, isNegative: isNegative, suffixStr: suffixStr);
			}
		}

		[GeneratedRegex(@"([-+]?)0[bB]([0-1]+)(?:_?([a-zA-Z0-9]))?")]
		private static partial Regex BinaryIntRegex();

		public static Literal ReparseBinaryInt(string input)
		{
			Match match = BinaryIntRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				bool isNegative = match.Groups[1].Value == "-";
				string value = Convert.ToString(Convert.ToUInt64(match.Groups[2].Value, 2));
				string suffixStr = match.Groups[3].Value;
				
				return new Literal(LiteralType.Integer, value, isNegative: isNegative, suffixStr: suffixStr);
			}
		}

		[GeneratedRegex(@"([-+]?)([0-9]+\.[0-9]+)(?:_?([a-zA-Z0-9]))?")]
		private static partial Regex PlainFracRegex();

		public static Literal ReparsePlainFrac(string input)
		{
			Match match = PlainFracRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				bool isNegative = match.Groups[1].Value == "-";
				string value = match.Groups[2].Value;
				string suffixStr = match.Groups[3].Value;
				
				return new Literal(LiteralType.Fractional, value, isNegative: isNegative, suffixStr: suffixStr);
			}
		}

		[GeneratedRegex(@"([-+]?)([0-9]+\.[0-9]+[eE][-+]?[0-9]+)(?:_?([a-zA-Z0-9]))?")]
		private static partial Regex ScientificFracRegex();

		public static Literal ReparseScientificFrac(string input)
		{
			Match match = ScientificFracRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				bool isNegative = match.Groups[1].Value == "-";
				string value = match.Groups[2].Value;
				string suffixStr = match.Groups[3].Value;
				
				return new Literal(LiteralType.Fractional, value, isNegative: isNegative, suffixStr: suffixStr);
			}
		}

		[GeneratedRegex(@"([-+]?)([0-9]+[eE][-+]?[0-9]+)(?:_?([a-zA-Z0-9]))?")]
		private static partial Regex ScientificWholeNumRegex();

		public static Literal ReparseScientificWholeNum(string input)
		{
			Match match = ScientificWholeNumRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				bool isNegative = match.Groups[1].Value == "-";
				string value = match.Groups[2].Value;
				string suffixStr = match.Groups[3].Value;
				
				return new Literal(LiteralType.Fractional, value, isNegative: isNegative, suffixStr: suffixStr);
			}
		}

		[GeneratedRegex(@"'((?:[^'\\]|\\.)+)'(?:_?([a-zA-Z0-9]))?")]
		private static partial Regex CharRegex();

		public static Literal ReparseChar(string input)
		{
			Match match = CharRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				string value = match.Groups[1].Value;
				string suffixStr = match.Groups[2].Value;

				return new Literal(LiteralType.Integer, value, suffixStr: suffixStr);
			}
		}
	}
}
