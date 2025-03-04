
using System;
using System.Text.RegularExpressions;
using BassaltCompiler.ErrorHandling;
using BassaltCompiler.Syntactic.Nodes;

namespace BassaltCompiler.Syntactic
{
	static partial class Reparsing
	{
		public static ExprLiteral ReparseBool(string input, BassaltSyntaxErrorHandler errorHandler, int errorLine, int errorCharPos)
		{
			if (input == "true")
			{ return new ExprLiteral(ExprLiteralType.Boolean, "true"); }
			else if (input == "false")
			{ return new ExprLiteral(ExprLiteralType.Boolean, "false"); }
			else
			{ throw new ArgumentException("Input must be valid."); }
		}

		public static ExprLiteral ReparseNull(string input, BassaltSyntaxErrorHandler errorHandler, int errorLine, int errorCharPos)
		{
			if (input == "null")
			{ return new ExprLiteral(ExprLiteralType.Null, "null"); }
			else
			{ throw new ArgumentException("Input must be valid."); }
		}

		[GeneratedRegex(@"([0-9]+)(?:_?([a-zA-Z0-9]+))?")]
		private static partial Regex DecIntRegex();

		public static ExprLiteral ReparseDecInt(string input, BassaltSyntaxErrorHandler errorHandler, int errorLine, int errorCharPos)
		{
			Match match = DecIntRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				string value = match.Groups[1].Value;
				string suffixStr = match.Groups[2].Value;

				return new ExprLiteral(ExprLiteralType.Integer, value, suffixStr: suffixStr);
			}
		}

		[GeneratedRegex(@"0[xX]([0-9a-fA-F]+)(?:_?([a-zA-Z0-9]+))?")]
		private static partial Regex HexIntRegex();

		public static ExprLiteral ReparseHexInt(string input, BassaltSyntaxErrorHandler errorHandler, int errorLine, int errorCharPos)
		{
			Match match = HexIntRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				string value = Convert.ToString(Convert.ToUInt64(match.Groups[1].Value, 16));
				string suffixStr = match.Groups[2].Value;
				
				return new ExprLiteral(ExprLiteralType.Integer, value, suffixStr: suffixStr);
			}
		}

		[GeneratedRegex(@"0[oO]([0-7]+)(?:_?([a-zA-Z0-9]+))?")]
		private static partial Regex OctalIntRegex();

		public static ExprLiteral ReparseOctalInt(string input, BassaltSyntaxErrorHandler errorHandler, int errorLine, int errorCharPos)
		{
			Match match = OctalIntRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				string value = Convert.ToString(Convert.ToUInt64(match.Groups[1].Value, 8));
				string suffixStr = match.Groups[2].Value;
				
				return new ExprLiteral(ExprLiteralType.Integer, value, suffixStr: suffixStr);
			}
		}

		[GeneratedRegex(@"0[bB]([0-1]+)(?:_?([a-zA-Z0-9]+))?")]
		private static partial Regex BinaryIntRegex();

		public static ExprLiteral ReparseBinaryInt(string input, BassaltSyntaxErrorHandler errorHandler, int errorLine, int errorCharPos)
		{
			Match match = BinaryIntRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				string value = Convert.ToString(Convert.ToUInt64(match.Groups[1].Value, 2));
				string suffixStr = match.Groups[2].Value;
				
				return new ExprLiteral(ExprLiteralType.Integer, value, suffixStr: suffixStr);
			}
		}

		[GeneratedRegex(@"([0-9]+\.[0-9]+)(?:_?([a-zA-Z0-9]+))?")]
		private static partial Regex PlainFracRegex();

		public static ExprLiteral ReparsePlainFrac(string input, BassaltSyntaxErrorHandler errorHandler, int errorLine, int errorCharPos)
		{
			Match match = PlainFracRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				string value = match.Groups[1].Value;
				string suffixStr = match.Groups[2].Value;
				
				return new ExprLiteral(ExprLiteralType.Fractional, value, suffixStr: suffixStr);
			}
		}

		[GeneratedRegex(@"([0-9]+\.[0-9]+[eE][-+]?[0-9]+)(?:_?([a-zA-Z0-9]+))?")]
		private static partial Regex ScientificFracRegex();

		public static ExprLiteral ReparseScientificFrac(string input, BassaltSyntaxErrorHandler errorHandler, int errorLine, int errorCharPos)
		{
			Match match = ScientificFracRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				string value = match.Groups[1].Value;
				string suffixStr = match.Groups[2].Value;
				
				return new ExprLiteral(ExprLiteralType.Fractional, value, suffixStr: suffixStr);
			}
		}

		[GeneratedRegex(@"([0-9]+[eE][-+]?[0-9]+)(?:_?([a-zA-Z0-9]+))?")]
		private static partial Regex ScientificWholeNumRegex();

		public static ExprLiteral ReparseScientificWholeNum(string input, BassaltSyntaxErrorHandler errorHandler, int errorLine, int errorCharPos)
		{
			Match match = ScientificWholeNumRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				string value = match.Groups[1].Value;
				string suffixStr = match.Groups[2].Value;
				
				return new ExprLiteral(ExprLiteralType.Fractional, value, suffixStr: suffixStr);
			}
		}

		[GeneratedRegex(@"'((?:[^'\\]|\\.)*)'(?:_?([a-zA-Z0-9]+))?")]
		private static partial Regex CharRegex();

		[GeneratedRegex(@"\\u([0-9a-fA-F]{4})")]
		private static partial Regex CharUnicodeSmallRegex();

		[GeneratedRegex(@"\\U([0-9a-fA-F]{8})")]
		private static partial Regex CharUnicodeLargeRegex();

		public static ExprLiteral ReparseChar(string input, BassaltSyntaxErrorHandler errorHandler, int errorLine, int errorCharPos)
		{
			Match match = CharRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				string prevalue = match.Groups[1].Value;

				string value;
				if (prevalue.Length == 0)
				{
					errorHandler.Add(errorLine, errorCharPos, "char literals must contain a single character.");
					return null;
				}
				else if (prevalue.Length == 1)
				{
					value = Convert.ToString((uint)prevalue[0]);
				}
				else
				{
					if (prevalue == "\\a")
					{ value = Convert.ToString((ulong)'\a'); }
					else if (prevalue == "\\b")
					{ value = Convert.ToString((ulong)'\b'); }
					else if (prevalue == "\\f")
					{ value = Convert.ToString((ulong)'\f'); }
					else if (prevalue == "\\n")
					{ value = Convert.ToString((ulong)'\n'); }
					else if (prevalue == "\\r")
					{ value = Convert.ToString((ulong)'\r'); }
					else if (prevalue == "\\t")
					{ value = Convert.ToString((ulong)'\t'); }
					else if (prevalue == "\\v")
					{ value = Convert.ToString((ulong)'\v'); }
					else if (prevalue == "\\\\")
					{ value = Convert.ToString((ulong)'\\'); }
					else if (prevalue == "\\\'")
					{ value = Convert.ToString((ulong)'\''); }
					else if (prevalue == "\\\"")
					{ value = Convert.ToString((ulong)'\"'); }
					else
					{
						Match matchSmall = CharUnicodeSmallRegex().Match(prevalue);
						Match matchLarge = CharUnicodeLargeRegex().Match(prevalue);

						if (matchSmall.Success)
						{
							value = Convert.ToString(Convert.ToUInt64(matchSmall.Groups[1].Value, 16));
						}
						else if (matchLarge.Success)
						{
							value = Convert.ToString(Convert.ToUInt64(matchLarge.Groups[1].Value, 16));
						}
						else
						{
							errorHandler.Add(errorLine, errorCharPos, "char literals must contain a single character.");
							return null;
						}
					}
				}

				string suffixStr = match.Groups[2].Value;

				return new ExprLiteral(ExprLiteralType.Integer, value, suffixStr: suffixStr);
			}
		}

		[GeneratedRegex(@"""((?:[^""\\]|\\.)*)""")]
		private static partial Regex StringRegex();

		public static ExprLiteral ReparseString(string input, BassaltSyntaxErrorHandler errorHandler, int errorLine, int errorCharPos)
		{
			Match match = StringRegex().Match(input);
			if (!match.Success)
			{ throw new ArgumentException("Input must be valid."); }
			else
			{
				string value = match.Groups[1].Value;
				
				return new ExprLiteral(ExprLiteralType.String, value);
			}
		}
	}
}
