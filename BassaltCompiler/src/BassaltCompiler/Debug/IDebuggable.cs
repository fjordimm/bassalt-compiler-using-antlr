
using System;
using System.Collections.Generic;
using System.Text;

namespace BassaltCompiler.Debug
{
	interface IDebuggable
	{
		public static string ToStringTree(IDebuggable obj)
		{
			return $"{obj.StringTreeName()}{ChildrenToStringTree("", obj.StringTreeChildren())}";
		}

		protected string StringTreeName();
		protected IReadOnlyList<IDebuggable> StringTreeChildren();

		private static string ChildrenToStringTree(string indents, IReadOnlyList<IDebuggable> children)
		{
			if (children is null)
			{
				return "";
			}
			else
			{
				StringBuilder ret = new StringBuilder();

				for (int i = 0; i < children.Count; i++)
				{
					if (i < children.Count - 1)
					{
						ret.AppendLine();
						ret.Append(indents);
						ret.Append("├─");
						ret.Append(children[i].StringTreeName());
						ret.Append(ChildrenToStringTree($"{indents}│ ", children[i].StringTreeChildren()));
					}
					else
					{
						ret.AppendLine();
						ret.Append(indents);
						ret.Append("└─");
						ret.Append(children[i].StringTreeName());
						ret.Append(ChildrenToStringTree($"{indents}  ", children[i].StringTreeChildren()));
					}
				}

				return ret.ToString();
			}
		}
	}
}
