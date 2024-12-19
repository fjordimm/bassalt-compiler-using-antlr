
namespace BassaltCompiler.Syntactic.Nodes
{
	enum LiteralType
	{
		Boolean, Integer, Fractional, String
	}

	class Literal
	{
		public LiteralType Type { get; }
		public string Val { get; }

		public Literal(LiteralType type, string val)
		{
			Type = type;
			Val = val;
		}
	}
}

