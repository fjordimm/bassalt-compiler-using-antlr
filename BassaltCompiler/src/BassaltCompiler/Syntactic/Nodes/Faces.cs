
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class Faces : IDebuggable
	{
		string IDebuggable.StringTreeName()
		{
			throw new NotImplementedException();
		}

		IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
		{
			throw new NotImplementedException();
		}
	}

	abstract class Face
	{
		// public static readonly FaceNone FcNone = FaceNone.FcNone_;

		public override string ToString()
		{
			return ToString1();
		}

		protected abstract string ToString1();
	}

	// sealed class FaceNone : Face
	// {
	// 	public static readonly FaceNone FcNone_ = new FaceNone();

	// 	private FaceNone()
	// 	{ }

	// 	protected override string ToString1()
	// 	{
	// 		return "FaceNone";
	// 	}
	// }

	sealed class FaceImmutable : Face
	{
		public static readonly FaceImmutable FcImmutable_ = new FaceImmutable();

		private FaceImmutable()
		{ }

		protected override string ToString1()
		{
			return "!";
		}
	}

	// sealed class FaceAccessModifier : Face
	// {

	// }
}
