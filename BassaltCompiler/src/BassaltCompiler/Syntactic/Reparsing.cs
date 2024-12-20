
using System;
using System.Text.RegularExpressions;
using BassaltCompiler.Syntactic.Nodes;

namespace BassaltCompiler.Syntactic
{
	static class Reparsing
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

		public static Literal ReparseDecInt(string input)
		{
			bool isNegative = false;
			string value = null;
			string suffixStr = null;

			Regex regex = new Regex(@"([-+]?)([0-9]+)(?:_?([a-zA-Z0-9]))?");
			Match match = regex.Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				isNegative = match.Groups[1].Value == "-";
				value = match.Groups[2].Value;
				suffixStr = match.Groups[3].Value;
			}

			return new Literal(LiteralType.Integer, value, isNegative: isNegative, suffixStr: suffixStr);
		}

		public static Literal ReparseHexInt(string input)
		{
			bool isNegative = false;
			string value = null;
			string suffixStr = null;

			Regex regex = new Regex(@"([-+]?)0[xX]([0-9a-fA-F]+)(?:_?([a-zA-Z0-9]))?");
			Match match = regex.Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				isNegative = match.Groups[1].Value == "-";
				value = Convert.ToString(Convert.ToUInt64(match.Groups[2].Value, 16));
				suffixStr = match.Groups[3].Value;
			}

			return new Literal(LiteralType.Integer, value, isNegative: isNegative, suffixStr: suffixStr);
		}

		public static Literal ReparseOctalInt(string input)
		{
			bool isNegative = false;
			string value = null;
			string suffixStr = null;

			Regex regex = new Regex(@"([-+]?)0[oO]([0-7]+)(?:_?([a-zA-Z0-9]))?");
			Match match = regex.Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				isNegative = match.Groups[1].Value == "-";
				value = Convert.ToString(Convert.ToUInt64(match.Groups[2].Value, 8));
				suffixStr = match.Groups[3].Value;
			}

			return new Literal(LiteralType.Integer, value, isNegative: isNegative, suffixStr: suffixStr);
		}

		public static Literal ReparseBinaryInt(string input)
		{
			bool isNegative = false;
			string value = null;
			string suffixStr = null;

			Regex regex = new Regex(@"([-+]?)0[bB]([0-1]+)(?:_?([a-zA-Z0-9]))?");
			Match match = regex.Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				isNegative = match.Groups[1].Value == "-";
				value = Convert.ToString(Convert.ToUInt64(match.Groups[2].Value, 2));
				suffixStr = match.Groups[3].Value;
			}

			return new Literal(LiteralType.Integer, value, isNegative: isNegative, suffixStr: suffixStr);
		}

		public static Literal ReparsePlainFrac(string input)
		{
			bool isNegative = false;
			string value = null;
			string suffixStr = null;

			Regex regex = new Regex(@"([-+]?)([0-9]+\.[0-9]+)(?:_?([a-zA-Z0-9]))?");
			Match match = regex.Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				isNegative = match.Groups[1].Value == "-";
				value = match.Groups[2].Value;
				suffixStr = match.Groups[3].Value;
			}

			return new Literal(LiteralType.Fractional, value, isNegative: isNegative, suffixStr: suffixStr);
		}

		public static Literal ReparseScientificFrac(string input)
		{
			bool isNegative = false;
			string value = null;
			string suffixStr = null;

			Regex regex = new Regex(@"([-+]?)([0-9]+\.[0-9]+[eE][-+]?[0-9]+)(?:_?([a-zA-Z0-9]))?");
			Match match = regex.Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				isNegative = match.Groups[1].Value == "-";
				value = match.Groups[2].Value;
				suffixStr = match.Groups[3].Value;
			}

			return new Literal(LiteralType.Fractional, value, isNegative: isNegative, suffixStr: suffixStr);
		}

		public static Literal ReparseScientificWholeNum(string input)
		{
			bool isNegative = false;
			string value = null;
			string suffixStr = null;

			Regex regex = new Regex(@"([-+]?)([0-9]+[eE][-+]?[0-9]+)(?:_?([a-zA-Z0-9]))?");
			Match match = regex.Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				isNegative = match.Groups[1].Value == "-";
				value = match.Groups[2].Value;
				suffixStr = match.Groups[3].Value;
			}

			return new Literal(LiteralType.Fractional, value, isNegative: isNegative, suffixStr: suffixStr);
		}
	}
}
