
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	abstract class Datatype : IDebuggable
	{
		public static readonly DatatypeLang DtVoid = DatatypeLang.DtVoid_;
		public static readonly DatatypeLang DtFunc = DatatypeLang.DtFunc_;
		public static readonly DatatypeLang DtSptr = DatatypeLang.DtSptr_;
		public static readonly DatatypeLang DtWsptr = DatatypeLang.DtWsptr_;

		public static readonly DatatypeLang DtBool = DatatypeLang.DtBool_;
		public static readonly DatatypeLang DtChar = DatatypeLang.DtChar_;
		public static readonly DatatypeLang DtChar8 = DatatypeLang.DtChar8_;
		public static readonly DatatypeLang DtChar16 = DatatypeLang.DtChar16_;
		public static readonly DatatypeLang DtChar32 = DatatypeLang.DtChar32_;
		public static readonly DatatypeLang DtSbyte = DatatypeLang.DtSbyte_;
		public static readonly DatatypeLang DtByte = DatatypeLang.DtByte_;
		public static readonly DatatypeLang DtShort = DatatypeLang.DtShort_;
		public static readonly DatatypeLang DtUshort = DatatypeLang.DtUshort_;
		public static readonly DatatypeLang DtInt = DatatypeLang.DtInt_;
		public static readonly DatatypeLang DtUint = DatatypeLang.DtUint_;
		public static readonly DatatypeLang DtLong = DatatypeLang.DtLong_;
		public static readonly DatatypeLang DtUlong = DatatypeLang.DtUlong_;
		public static readonly DatatypeLang DtFloat = DatatypeLang.DtFloat_;
		public static readonly DatatypeLang DtDouble = DatatypeLang.DtDouble_;

		public static readonly DatatypeLang DtString = DatatypeLang.DtString_;
		public static readonly DatatypeLang DtInt128 = DatatypeLang.DtInt128_;
		public static readonly DatatypeLang DtUint128 = DatatypeLang.DtUint128_;
		public static readonly DatatypeLang DtInt256 = DatatypeLang.DtInt256_;
		public static readonly DatatypeLang DtUint256 = DatatypeLang.DtUint256_;
		public static readonly DatatypeLang DtFloat128 = DatatypeLang.DtFloat128_;
		public static readonly DatatypeLang DtFloat256 = DatatypeLang.DtFloat256_;

		string IDebuggable.StringTreeName()
		{
			return $"Datatype.{StringTreeName1()}";
		}

		protected abstract string StringTreeName1();

		IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
		{
			return StringTreeChildren1();
		}

		protected abstract IReadOnlyList<IDebuggable> StringTreeChildren1();
	}

	sealed class DatatypeLang : Datatype
	{
		public static readonly DatatypeLang DtVoid_ = new DatatypeLang("void");
		public static readonly DatatypeLang DtFunc_ = new DatatypeLang("func");
		public static readonly DatatypeLang DtSptr_ = new DatatypeLang("sptr");
		public static readonly DatatypeLang DtWsptr_ = new DatatypeLang("wsptr");

		public static readonly DatatypeLang DtBool_ = new DatatypeLang("bool");
		public static readonly DatatypeLang DtChar_ = new DatatypeLang("char");
		public static readonly DatatypeLang DtChar8_ = new DatatypeLang("char8");
		public static readonly DatatypeLang DtChar16_ = new DatatypeLang("char16");
		public static readonly DatatypeLang DtChar32_ = new DatatypeLang("char32");
		public static readonly DatatypeLang DtSbyte_ = new DatatypeLang("sbyte");
		public static readonly DatatypeLang DtByte_ = new DatatypeLang("byte");
		public static readonly DatatypeLang DtShort_ = new DatatypeLang("short");
		public static readonly DatatypeLang DtUshort_ = new DatatypeLang("ushort");
		public static readonly DatatypeLang DtInt_ = new DatatypeLang("int");
		public static readonly DatatypeLang DtUint_ = new DatatypeLang("uint");
		public static readonly DatatypeLang DtLong_ = new DatatypeLang("long");
		public static readonly DatatypeLang DtUlong_ = new DatatypeLang("ulong");
		public static readonly DatatypeLang DtFloat_ = new DatatypeLang("float");
		public static readonly DatatypeLang DtDouble_ = new DatatypeLang("double");

		public static readonly DatatypeLang DtString_ = new DatatypeLang("string");
		public static readonly DatatypeLang DtInt128_ = new DatatypeLang("int128");
		public static readonly DatatypeLang DtUint128_ = new DatatypeLang("uint128");
		public static readonly DatatypeLang DtInt256_ = new DatatypeLang("int256");
		public static readonly DatatypeLang DtUint256_ = new DatatypeLang("uint256");
		public static readonly DatatypeLang DtFloat128_ = new DatatypeLang("float128");
		public static readonly DatatypeLang DtFloat256_ = new DatatypeLang("float256");

		private static readonly ReadOnlyDictionary<string, DatatypeLang> datatypeLangDict = new Dictionary<string, DatatypeLang>
		{
			{"void", DtVoid_},
			{"func", DtFunc_},
			{"sptr", DtSptr_},
			{"wsptr", DtWsptr_},
			{"bool", DtBool_},
			{"char", DtChar_},
			{"char8", DtChar8_},
			{"char16", DtChar16_},
			{"char32", DtChar32_},
			{"sbyte", DtSbyte_},
			{"byte", DtByte_},
			{"short", DtShort_},
			{"ushort", DtUshort_},
			{"int", DtInt_},
			{"uint", DtUint_},
			{"long", DtLong_},
			{"ulong", DtUlong_},
			{"float", DtFloat_},
			{"double", DtDouble_},
			{"string", DtString_},
			{"int128", DtInt128_},
			{"uint128", DtUint128_},
			{"int256", DtInt256_},
			{"uint256", DtUint256_},
			{"float128", DtFloat128_},
			{"float256", DtFloat256_}
		}.AsReadOnly();

		public static DatatypeLang Get(string str)
		{
			if (datatypeLangDict.TryGetValue(str, out DatatypeLang tryGetVal))
			{ return tryGetVal; }
			else
			{ throw new ArgumentException("argument was not valid."); }
		}

		public string Name { get; }

		private DatatypeLang(string name)
		{
			Name = name;
		}

		protected override string StringTreeName1()
		{
			return $"Lang({Name})";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			return null;
		}
	}

	sealed class DatatypeIdentifier : Datatype
	{
		public string Name { get; }

		public DatatypeIdentifier(string name)
		{
			Name = name;
		}

		protected override string StringTreeName1()
		{
			return $"Identifier('{Name}')";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			return null;
		}
	}

	sealed class DatatypeTuple : Datatype
	{
		public ImmutableList<Datatype> Items { get; }

		public DatatypeTuple(IEnumerable<Datatype> items)
		{
			Items = items.ToImmutableList();
		}

		protected override string StringTreeName1()
		{
			return "Tuple";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			return Items;
		}
	}

	sealed class DatatypeGenericed : Datatype
	{
		public Datatype MainType { get; }
		public ImmutableList<Datatype> Generics { get; }

		public DatatypeGenericed(Datatype mainType, IEnumerable<Datatype> generics)
		{
			MainType = mainType;
			Generics = generics.ToImmutableList();
		}

		protected override string StringTreeName1()
		{
			return "Genericed";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			List<IDebuggable> ret = new List<IDebuggable>{ MainType };
			foreach (IDebuggable item in Generics)
			{ ret.Add(item); }
			
			return ret;
		}
	}

	sealed class DatatypeNamespaced : Datatype
	{
		public Expr Namespace { get; }
		public Datatype Inner { get; }

		public DatatypeNamespaced(Expr namespacee, Datatype inner)
		{
			Namespace = namespacee;
			Inner = inner;
		}

		protected override string StringTreeName1()
		{
			return "Namespaced";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			return new List<IDebuggable>{ Namespace, Inner };
		}
	}
}
