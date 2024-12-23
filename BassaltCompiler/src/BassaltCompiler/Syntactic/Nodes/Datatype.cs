
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	abstract class Datatype : IDebuggable
	{
		public static DatatypeUnset DtUnset = new DatatypeUnset();
		public static readonly DatatypeLang DtVoid = DatatypeLang.DtVoid_;
		// TODO: more
		// public static LangDatatypeTEMP LtLangtypeTEMP = new LangDatatypeTEMP();

		string IDebuggable.StringTreeName()
		{
			return StringTreeName1();
		}

		protected abstract string StringTreeName1();

		IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
		{
			return null;
		}
	}

	sealed class DatatypeUnset : Datatype
	{
		protected override string StringTreeName1()
		{
			return "DatatypeUnset";
		}
	}

	class DatatypeLang : Datatype
	{
		public static readonly DatatypeLang DtVoid_ = new DatatypeLang("void");
		public static readonly DatatypeLang DtFunc_ = new DatatypeLang("func");
		public static readonly DatatypeLang DtSptr_ = new DatatypeLang("sptr");
		public static readonly DatatypeLang DtWsptr_ = new DatatypeLang("wsptr");
		public static readonly DatatypeLang DtBool_ = new DatatypeLang("bool");

		protected static readonly ReadOnlyDictionary<string, DatatypeLang> DatatypeLangDict = new Dictionary<string, DatatypeLang>
		{
			{"void", DtVoid_},
			{"func", DtFunc_},
			{"sptr", DtSptr_},
			{"wsptr", DtWsptr_},
			{"bool", DtBool_},
		}.AsReadOnly();

		public static DatatypeLang Get(string name)
		{
			if (DatatypeLangDict.TryGetValue(name, out DatatypeLang tryGetVal))
			{ return tryGetVal; }
			else
			{ throw new ArgumentException("argument was not valid."); }
		}

		public string Name { get; }

		protected DatatypeLang(string name)
		{
			Name = name;
		}

		protected override string StringTreeName1()
		{
			return $"DatatypeLang({Name})";
		}
	}

	// class LangDatatypeTEMP : LangDatatype
	// {
	// 	protected override string StringTreeName1()
	// 	{
	// 		return "LangtypeTEMP";
	// 	}
	// }

	// static class Types
	// {
	// 	public static readonly UnsetDatatype LtUnset = new UnsetDatatype(0);
	// 	public static readonly PrimitiveDatatype LtBool = new PrimitiveDatatype(1, PrimType.Bool);
	// 	public static readonly PrimitiveDatatype LtInt = new PrimitiveDatatype(4, PrimType.Int);
	// 	public static readonly PrimitiveDatatype LtFloat = new PrimitiveDatatype(4, PrimType.Float);

	// 	public enum PrimType
	// 	{
	// 		Bool, Int, Float
	// 	}

	// 	public abstract class Datatype
	// 	{
	// 		public ulong Size { get; } // In bytes

	// 		public Datatype(ulong size)
	// 		{
	// 			Size = size;
	// 		}

	// 		public abstract override string ToString();
	// 	}

	// 	public sealed class UnsetDatatype : Datatype
	// 	{
	// 		public UnsetDatatype(ulong size) : base(size)
	// 		{ }

	// 		public override string ToString()
	// 		{
	// 			return "UnsetDatatype";
	// 		}
	// 	}

	// 	public class PrimitiveDatatype : Datatype
	// 	{
	// 		public PrimType Prim { get; }

	// 		public PrimitiveDatatype(ulong size, PrimType prim)
	// 			: base(size)
	// 		{
	// 			Prim = prim;
	// 		}

	// 		public override string ToString()
	// 		{
	// 			return Prim.ToString();
	// 		}
	// 	}
	// }
}
