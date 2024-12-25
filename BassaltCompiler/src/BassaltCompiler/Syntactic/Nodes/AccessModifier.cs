
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BassaltCompiler.Syntactic.Nodes
{
	enum AccessModifier
	{
		Public, Private, Protected
	}

	static class AccessModifiers
	{
		private static readonly ReadOnlyDictionary<string, AccessModifier> accessModifierDict = new Dictionary<string, AccessModifier>
		{
			{"public", AccessModifier.Public},
			{"private", AccessModifier.Private},
			{"protected", AccessModifier.Protected}
		}.AsReadOnly();

		public static AccessModifier Get(string str)
		{
			if (accessModifierDict.TryGetValue(str, out AccessModifier tryGetVal))
			{ return tryGetVal; }
			else
			{ throw new ArgumentException("argument was not valid."); }
		}
	}
}
