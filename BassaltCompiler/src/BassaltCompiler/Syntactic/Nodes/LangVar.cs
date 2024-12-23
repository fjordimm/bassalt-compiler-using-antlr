
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using BassaltCompiler.Debug;

namespace BassaltCompiler.Syntactic.Nodes
{
	class LangVar : IDebuggable
	{
		public static readonly LangVar LvThis = new LangVar("this");
		public static readonly LangVar LvBase = new LangVar("base");
		public static readonly LangVar LvStdout = new LangVar("stdout");
		public static readonly LangVar LvStdin = new LangVar("stdin");
		public static readonly LangVar LvStderr = new LangVar("stderr");
		public static readonly LangVar LvPlaceholder = new LangVar("placeholder");

		private static readonly ReadOnlyDictionary<string, LangVar> LangVarDict = new Dictionary<string, LangVar>
		{
			{"this", LvThis},
			{"base", LvBase},
			{"stdout", LvStdout},
			{"stdin", LvStdin},
			{"stderr", LvStderr},
			{"placeholder", LvPlaceholder}
		}.AsReadOnly();

		public static LangVar Get(string name)
		{
			if (LangVarDict.TryGetValue(name, out LangVar tryGetVal))
			{ return tryGetVal; }
			else
			{ throw new ArgumentException("argument was not valid."); }
		}

		public string Name { get; }

		private LangVar(string name)
		{
			Name = name;
		}

		string IDebuggable.StringTreeName()
		{
			return $"LangVar({Name})";
		}

		IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
		{
			return null;
		}
	}
}
