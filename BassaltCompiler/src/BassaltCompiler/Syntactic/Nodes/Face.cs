
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class FaceList : IDebuggable
	{
		private readonly List<Face> _faces;
		public IReadOnlyList<Face> Faces { get => _faces.AsReadOnly(); }

		public FaceList(IEnumerable<Face> faces)
		{
			_faces = faces.ToList();
		}

		public void Add(Face face)
		{
			_faces.Add(face);
		}

		public override string ToString()
		{
			StringBuilder ret = new StringBuilder();

			ret.Append("Faces[ ");
			for (int i = 0; i < Faces.Count; i++)
			{
				if (i == 0)
				{ ret.Append($"{Faces[i]}"); }
				else
				{ ret.Append($", {Faces[i]}"); }
			}
			ret.Append(" ]");

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
			return "!";
		}
	}

	// sealed class FaceAccessModifier : Face
	// {

	// }
}
