
namespace BassaltCompiler.Syntactic.Nodes
{
	class ExprLambda : Expr
	{
		public string Name { get; }

		public ExprLambda(string name)
		{
			Name = name;
		}

		protected override string ToString1(int indent)
		{
			throw new System.NotImplementedException();
		}
	}
}
