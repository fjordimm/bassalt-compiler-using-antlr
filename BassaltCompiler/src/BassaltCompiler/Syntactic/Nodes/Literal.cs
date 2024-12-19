
using System.Collections.Generic;

namespace BassaltCompiler.Syntactic.Nodes
{
	enum LiteralType
	{
		Boolean, Integer, Fractional, String
	}

	enum LiteralSuffix
	{
		None, C8, C, C32, SB, S, I, L, B, US, UI, UL, F, D, II, UII, III, UIII, R, T
	}

	class Literal
	{
		private static readonly Dictionary<string, LiteralSuffix> suffixDict = new Dictionary<string, LiteralSuffix>
		{
			{"", LiteralSuffix.None},
			{"c8", LiteralSuffix.C8},
			{"c", LiteralSuffix.C},
			{"c32", LiteralSuffix.C32},
			{"sb", LiteralSuffix.SB},
			{"s", LiteralSuffix.S},
			{"i", LiteralSuffix.I},
			{"l", LiteralSuffix.L},
			{"b", LiteralSuffix.B},
			{"us", LiteralSuffix.US},
			{"ui", LiteralSuffix.UI},
			{"ul", LiteralSuffix.UL},
			{"f", LiteralSuffix.F},
			{"d", LiteralSuffix.D},
			{"ii", LiteralSuffix.II},
			{"uii", LiteralSuffix.UII},
			{"iii", LiteralSuffix.III},
			{"uiii", LiteralSuffix.UIII},
			{"r", LiteralSuffix.R},
			{"t", LiteralSuffix.T}
		};

		public LiteralType Type { get; }
		public string Val { get; }
		public bool IsNegative { get; }
		public LiteralSuffix Suffix { get; }

		public Literal(LiteralType type, string val, bool isNegative = false, string suffixStr = null, LiteralSuffix suffix = LiteralSuffix.None)
		{
			Type = type;
			Val = val;
			IsNegative = isNegative;

			if (suffixStr is not null)
			{
				Suffix = suffixDict[suffixStr.ToLower()];
			}
			else
			{
				Suffix = suffix;
			}
		}
	}
}

