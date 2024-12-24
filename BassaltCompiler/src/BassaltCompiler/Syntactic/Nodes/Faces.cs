
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class Faces : IDebuggable
	{
		private readonly List<Face> _val;
		public IReadOnlyList<Face> Val { get => _val.AsReadOnly(); }

		public Faces(IEnumerable<Face> faces)
		{
			_val = faces.ToList();
		}

		public override string ToString()
		{
			StringBuilder ret = new StringBuilder();

			ret.Append("Faces[");
			for (int i = 0; i < Val.Count; i++)
			{
				if (i == 0)
				{ ret.Append($"{Val[i]}"); }
				else
				{ ret.Append($",{Val[i]}"); }
			}
			ret.Append(']');

			return ret.ToString();
		}

		string IDebuggable.StringTreeName()
		{
			return ToString();
		}

		IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
		{
			return null;
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
