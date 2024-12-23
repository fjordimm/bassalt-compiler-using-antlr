
using System;
using System.Collections.Generic;
using System.Linq;
using BassaltCompiler.Debug;

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

	class Literal : IDebuggable
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
				if (suffixDict.TryGetValue(suffixStr.ToLower(), out LiteralSuffix tryGetVal))
				{ Suffix = tryGetVal; }
				else
				{ throw new ArgumentException("suffixStr was not valid."); }
			}
			else
			{
				Suffix = suffix;
			}
		}

		string IDebuggable.StringTreeName()
		{
			return $"Literal({Type}, {(IsNegative ? "-" : "+")}, '{Val}', {Suffix})";
		}

		IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
		{
			return null;
		}
	}
}

