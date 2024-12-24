
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class DatatypeFaced : Datatype
	{
		public Face TheFace { get; }
		public Datatype Inner { get; }

		public DatatypeFaced(Face theFace, Datatype inner)
		{
			TheFace = theFace;
			Inner = inner;
		}

		protected override string StringTreeName1()
		{
			return $"Faced({TheFace})";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			return new List<IDebuggable>{ Inner };
		}
	}

	abstract class Face
	{
		public static readonly FaceImmutable FcImmutable = FaceImmutable.FcImmutable_;

		public override string ToString()
		{
			return ToString1();
		}

		protected abstract string ToString1();
	}

	sealed class FaceImmutable : Face
	{
		public static readonly FaceImmutable FcImmutable_ = new FaceImmutable();

		private FaceImmutable()
		{ }

		protected override string ToString1()
		{
			return "Immutable";
		}
	}

	sealed class FaceAccessModifier : Face
	{
		protected override string ToString1()
		{
			throw new NotImplementedException();
		}
	}

	sealed class FaceIdentifier : Face
	{
		protected override string ToString1()
		{
			throw new NotImplementedException();
		}
	}

	// abstract class Face
	// {
	// 	public static readonly FaceImmutable FcImmutable = FaceImmutable.FcImmutable_;

	// 	public override string ToString()
	// 	{
	// 		return ToString1();
	// 	}

	// 	protected abstract string ToString1();
	// }

	// sealed class FaceImmutable : Face
	// {
	// 	public static readonly FaceImmutable FcImmutable_ = new FaceImmutable();

	// 	private FaceImmutable()
	// 	{ }

	// 	protected override string ToString1()
	// 	{
	// 		return "!";
	// 	}
	// }

	// sealed class FaceAccessModifier : Face
	// {

	// }
}
