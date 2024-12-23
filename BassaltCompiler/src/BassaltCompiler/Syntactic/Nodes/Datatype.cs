
using System.Collections.Generic;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	abstract class Datatype : IDebuggable
	{
		public static UnsetDatatype LtUnset = new UnsetDatatype();
		public static LangDatatypeTEMP LtLangtypeTEMP = new LangDatatypeTEMP();

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

	sealed class UnsetDatatype : Datatype
	{
		protected override string StringTreeName1()
		{
			return "UnsetDatatype";
		}
	}

	abstract class LangDatatype : Datatype
	{ }

	class LangDatatypeTEMP : LangDatatype
	{
		protected override string StringTreeName1()
		{
			return "LangtypeTEMP";
		}
	}

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
