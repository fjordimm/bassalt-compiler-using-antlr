
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

		public static readonly FaceAccessModifier FcPublic = FaceAccessModifier.FcPublic_;
		public static readonly FaceAccessModifier FcPrivate = FaceAccessModifier.FcPrivate_;
		public static readonly FaceAccessModifier FcProtected = FaceAccessModifier.FcProtected_;

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
			return null;
		}
	}

	sealed class FaceAccessModifier : Face
	{
		public static readonly FaceAccessModifier FcPublic_ = new FaceAccessModifier(AccessModifier.Public);
		public static readonly FaceAccessModifier FcPrivate_ = new FaceAccessModifier(AccessModifier.Private);
		public static readonly FaceAccessModifier FcProtected_ = new FaceAccessModifier(AccessModifier.Protected);

		private static readonly ReadOnlyDictionary<string, FaceAccessModifier> accessModifierDict = new Dictionary<string, FaceAccessModifier>
		{
			{"public", FcPublic_},
			{"private", FcPrivate_},
			{"protected", FcProtected_}
		}.AsReadOnly();

		public static FaceAccessModifier Get(string str)
		{
			if (accessModifierDict.TryGetValue(str, out FaceAccessModifier tryGetVal))
			{ return tryGetVal; }
			else
			{ throw new ArgumentException("argument was not valid."); }
		}

		public AccessModifier Modifier { get; }

		private FaceAccessModifier(AccessModifier modifier)
		{
			Modifier = modifier;
		}

		protected override string StringTreeName1()
		{
			return $"AccessModifier({Modifier})";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			return null;
		}
	}
	
	// sealed class FaceIdentifier : Face
	// {
	// 	protected override string ToString1()
	// 	{
	// 		throw new NotImplementedException();
	// 	}
	// }

	sealed class FaceNamespaced : Face
	{
		public Expr Namespace { get; }
		public Face Inner { get; }

		public FaceNamespaced(Expr namespacee, Face inner)
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
