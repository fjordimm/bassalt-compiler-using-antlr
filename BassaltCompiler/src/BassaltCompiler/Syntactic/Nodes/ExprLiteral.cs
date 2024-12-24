
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	enum ExprLiteralType
	{
		Boolean, Null, Integer, Fractional, String
	}

	class ExprLiteral : Expr
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

		public ExprLiteralType Type { get; }
		public string Val { get; }
		public DatatypeLang Suffix { get; }

		public ExprLiteral(ExprLiteralType type, string val, string suffixStr = null, DatatypeLang suffix = null)
		{
			Type = type;
			Val = val;

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

		protected override string StringTreeName1()
		{
			return $"Literal({Type}, '{Val}', {(Suffix is null ? "None" : Suffix)})";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			return null;
		}
	}
}

