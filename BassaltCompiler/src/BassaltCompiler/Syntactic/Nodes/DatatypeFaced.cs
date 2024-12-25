
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
			return $"Faced";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			return new List<IDebuggable>{ TheFace, Inner };
		}
	}

	abstract class Face : IDebuggable
	{
		public static readonly FaceImmutable FcImmutable = FaceImmutable.FcImmutable_;

		// public override string ToString()
		// {
		// 	return ToString1();
		// }

		// protected abstract string ToString1();

		string IDebuggable.StringTreeName()
		{
			return $"Face.{StringTreeName1()}";
		}

		protected abstract string StringTreeName1();

		IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
		{
			return StringTreeChildren1();
		}

		protected abstract IReadOnlyList<IDebuggable> StringTreeChildren1();
	}

	sealed class FaceImmutable : Face
	{
		public static readonly FaceImmutable FcImmutable_ = new FaceImmutable();

		private FaceImmutable()
		{ }

		protected override string StringTreeName1()
		{
			return "Immutable";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			throw new NotImplementedException();
		}
	}

	// sealed class FaceNamespaced : Face
	// {
	// 	public IDebuggable Namespace { get; }
	// 	public Datatype Inner { get; }

	// 	public FaceNamespaced(IDebuggable namespacee, Datatype inner)
	// 	{
	// 		Namespace = namespacee;
	// 		Inner = inner;
	// 	}

	// 	protected override string StringTreeName1()
	// 	{
	// 		return "Namespaced";
	// 	}

	// 	protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
	// 	{
	// 		return new List<IDebuggable>{ Namespace, Inner };
	// 	}
	// }

	// sealed class FaceAccessModifier : Face
	// {
	// 	protected override string ToString1()
	// 	{
	// 		throw new NotImplementedException();
	// 	}
	// }

	// sealed class FaceIdentifier : Face
	// {
	// 	protected override string ToString1()
	// 	{
	// 		throw new NotImplementedException();
	// 	}
	// }
}
