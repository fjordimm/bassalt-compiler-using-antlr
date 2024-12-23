
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
			Namespace = namespacee;
			Item = item;
		}

		string IDebuggable.StringTreeName()
		{
			if (Namespace as LangDatatype is not null)
			{ return $"Namespaced(LangDatatype: {IDebuggable.ToStringTree(Namespace as LangDatatype)})"; }
			else if (Namespace as LangVar is not null)
			{ return $"Namespaced(LangVar: {(Namespace as LangVar).Name})"; }
			else if (Namespace as Identifier is not null)
			{ return $"Namespaced(Identifier: '{(Namespace as Identifier).Name}')"; }
			else
			{ System.Diagnostics.Trace.Fail("Namespace member was invalid."); return null; }
		}

		IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
		{
			return new List<IDebuggable>{ Item };
		}
	}
}
