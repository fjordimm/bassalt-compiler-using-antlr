
using System;
using System.Collections.Generic;
using System.Linq;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class Namespaced : IDebuggable
	{
		public IDebuggable Namespace { get; }
		public IDebuggable Inner { get; }

		public Namespaced(IDebuggable namespacee, IDebuggable inner)
		{
			Namespace = namespacee;
			Inner = inner;
		}

		string IDebuggable.StringTreeName()
		{
			return "Namespaced";
		}

		IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
		{
			return new List<IDebuggable>{ Namespace, Inner };
		}
	}
}
