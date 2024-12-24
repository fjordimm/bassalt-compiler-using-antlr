
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	enum LiteralType
	{
		Boolean, Null, Integer, Fractional, String
	}

	// enum LiteralSuffix
	// {
	// 	None, C8, C, C32, SB, S, I, L, B, US, UI, UL, F, D, II, UII, III, UIII, R, T
	// }

	class Literal : IDebuggable
	{
		private static readonly ReadOnlyDictionary<string, DatatypeLang> suffixDict = new Dictionary<string, DatatypeLang>
		{
			{"", null},
			{"c", Datatype.DtChar},
			{"c8", Datatype.DtChar8},
			{"c16", Datatype.DtChar16},
			{"c32", Datatype.DtChar32},
			{"sb", Datatype.DtSbyte},
			{"s", Datatype.DtShort},
			{"i", Datatype.DtInt},
			{"l", Datatype.DtLong},
			{"b", Datatype.DtByte},
			{"us", Datatype.DtUshort},
			{"ui", Datatype.DtUint},
			{"ul", Datatype.DtUlong},
			{"f", Datatype.DtFloat},
			{"d", Datatype.DtDouble},
			{"ii", Datatype.DtInt128},
			{"uii", Datatype.DtUint128},
			{"iii", Datatype.DtInt256},
			{"uiii", Datatype.DtUint256}
		}.AsReadOnly();

		public LiteralType Type { get; }
		public string Val { get; }
		public bool IsNegative { get; }
		public DatatypeLang Suffix { get; }

		public Literal(LiteralType type, string val, bool isNegative = false, string suffixStr = null, DatatypeLang suffix = null)
		{
			Type = type;
			Val = val;
			IsNegative = isNegative;

			if (suffixStr is not null)
			{
				if (suffixDict.TryGetValue(suffixStr.ToLower(), out DatatypeLang tryGetVal))
				{ Suffix = tryGetVal; }
				else
				{ throw new ArgumentException("The suffixStr argument was not valid."); }
			}
			else
			{
				Suffix = suffix;
			}
		}

		string IDebuggable.StringTreeName()
		{
			return $"Literal({Type}, {(IsNegative ? "-" : "+")}, '{Val}', {(Suffix is null ? "None" : Suffix)})";
		}

		IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
		{
			return null;
		}
	}
}

