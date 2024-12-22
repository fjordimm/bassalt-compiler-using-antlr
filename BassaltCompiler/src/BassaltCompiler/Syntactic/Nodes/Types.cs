
namespace BassaltCompiler.Syntactic.Nodes
{
	static class Types
	{
		public static readonly UnsetDatatype TUnset = new UnsetDatatype(0);
		public static readonly PrimitiveDatatype TBool = new PrimitiveDatatype(1, PrimType.Bool);
		public static readonly PrimitiveDatatype TInt = new PrimitiveDatatype(4, PrimType.Int);
		public static readonly PrimitiveDatatype TFloat = new PrimitiveDatatype(4, PrimType.Float);

		public enum PrimType
		{
			Bool, Int, Float
		}

		public abstract class Datatype
		{
			public ulong Size { get; } // In bytes

			public Datatype(ulong size)
			{
				Size = size;
			}

			public abstract override string ToString();
		}

		public sealed class UnsetDatatype : Datatype
		{
			public UnsetDatatype(ulong size) : base(size)
			{ }

			public override string ToString()
			{
				return "UnsetDatatype";
			}
		}

		public class PrimitiveDatatype : Datatype
		{
			public PrimType Prim { get; }

			public PrimitiveDatatype(ulong size, PrimType prim)
				: base(size)
			{
				Prim = prim;
			}

			public override string ToString()
			{
				return Prim.ToString();
			}
		}
	}
}
