
using System;
using System.Collections.Generic;
using System.Linq;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class Namespaced : IDebuggable
	{
		public IDebuggable Namespace { get; }
		public IDebuggable Item { get; }

		public Namespaced(IDebuggable namespacee, IDebuggable item)
		{
			// if (!(namespacee as DatatypeLang is not null || namespacee as LangVar is not null || namespacee as Identifier is not null))
			// { throw new ArgumentException("The namespacee argument must be a DatatypeLang, LangVar, or Identifier."); }

			Namespace = namespacee;
			Item = item;
		}

		string IDebuggable.StringTreeName()
		{
			// if (Namespace as DatatypeLang is not null)
			// { return $"Namespaced(DatatypeLang: {(Namespace as DatatypeLang).Name})"; }
			// else if (Namespace as LangVar is not null)
			// { return $"Namespaced(LangVar: {(Namespace as LangVar).Name})"; }
			// else if (Namespace as Identifier is not null)
			// { return $"Namespaced(Identifier: '{(Namespace as Identifier).Name}')"; }
			// else
			// { System.Diagnostics.Trace.Fail("Namespace member was invalid."); return null; }
			return "Namespaced";
		}

		IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
		{
			return new List<IDebuggable>{ Namespace, Item };
		}
	}
}
