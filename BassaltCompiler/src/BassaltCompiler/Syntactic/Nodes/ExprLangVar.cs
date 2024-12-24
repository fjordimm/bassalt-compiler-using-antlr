
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class ExprLangVar : Expr
	{
		public static readonly ExprLangVar LvThis = new ExprLangVar("this");
		public static readonly ExprLangVar LvBase = new ExprLangVar("base");
		public static readonly ExprLangVar LvStdout = new ExprLangVar("stdout");
		public static readonly ExprLangVar LvStdin = new ExprLangVar("stdin");
		public static readonly ExprLangVar LvStderr = new ExprLangVar("stderr");
		public static readonly ExprLangVar LvPlaceholder = new ExprLangVar("placeholder");

		private static readonly ReadOnlyDictionary<string, ExprLangVar> langVarDict = new Dictionary<string, ExprLangVar>
		{
			{"this", LvThis},
			{"base", LvBase},
			{"stdout", LvStdout},
			{"stdin", LvStdin},
			{"stderr", LvStderr},
			{"placeholder", LvPlaceholder}
		}.AsReadOnly();

		public static ExprLangVar Get(string name)
		{
			if (langVarDict.TryGetValue(name, out ExprLangVar tryGetVal))
			{ return tryGetVal; }
			else
			{ throw new ArgumentException("argument was not valid."); }
		}

		public string Name { get; }

		private ExprLangVar(string name)
		{
			Name = name;
		}
		
		protected override string StringTreeName1()
		{
			return $"LangVar({Name})";
		}

		protected override IReadOnlyList<IDebuggable> StringTreeChildren1()
		{
			return null;
		}
	}
}
