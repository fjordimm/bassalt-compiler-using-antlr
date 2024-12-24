
using System;
using System.Collections.Generic;
using System.Linq;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class ExprNamespaced : Expr
	{
		public IDebuggable Namespace { get; }
		public IDebuggable Inner { get; }

		public ExprNamespaced(IDebuggable namespacee, IDebuggable inner)
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
