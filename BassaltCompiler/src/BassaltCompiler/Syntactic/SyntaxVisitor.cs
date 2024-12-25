
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using BassaltCompiler.Debug;
using BassaltCompiler.ErrorHandling;
using BassaltCompiler.Syntactic.Nodes;

namespace BassaltCompiler.Syntactic
{
	class SyntaxVisitor : BassaltBaseVisitor<object>
	{
		////////////////////////////////////////////////////////////////////////
		
		///// Class Stuff /////

		private readonly SyntaxTree syntaxTree;
		private readonly IVocabulary lexerVocab;
		private readonly BassaltSyntaxErrorHandler bassaltSyntaxErrorHandler;
		// private readonly BassaltSemanticErrorHandler bassaltSemanticErrorHandler;

		public SyntaxVisitor(IVocabulary vocab, BassaltSyntaxErrorHandler syntaxErrorHandler)
		{
			syntaxTree = new SyntaxTree();
			lexerVocab = vocab;
			bassaltSyntaxErrorHandler = syntaxErrorHandler;
		}

		public SyntaxTree GetSyntaxTree()
		{
			return syntaxTree;
		}

		public class Terminal : IDebuggable
		{
			public string Type { get; }
			public string Text { get; }

			public Terminal(ITerminalNode node, IVocabulary vocab)
			{
				Type = vocab.GetSymbolicName(node.Symbol.Type);
				if (Type is null)
				{ Type = "unk"; }

				Text = node.GetText();
			}

			public Terminal(string type, string text)
			{
				Type = type;
				Text = text;
			}

			string IDebuggable.StringTreeName()
			{
				return $"TERMINAL({Type}, '{Text}')";
			}

			IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
			{
				return null;
			}
		}

		protected override object AggregateResult(object aggregate, object nextResult)
		{
			if (aggregate is null)
			{
				return nextResult;
			}
			else
			{
				AggregateObj aggregateR = aggregate as AggregateObj;

				if (aggregateR is not null)
				{
					if (nextResult is null)
					{ return null; }

					IDebuggable nextResultS = nextResult as IDebuggable;
					System.Diagnostics.Debug.Assert(nextResultS is not null);

					aggregateR.Items.Add(nextResultS);
					return aggregateR;
				}
				else
				{
					if (nextResult is null)
					{ return null; }

					IDebuggable aggregateS = aggregate as IDebuggable;
					System.Diagnostics.Debug.Assert(aggregateS is not null);

					IDebuggable nextResultS = nextResult as IDebuggable;
					System.Diagnostics.Debug.Assert(nextResultS is not null);

					AggregateObj ret = new AggregateObj();
					ret.Items.Add(aggregateS);
					ret.Items.Add(nextResultS);

					return ret;
				}
			}
		}

		public class AggregateObj : IDebuggable
		{
			public List<IDebuggable> Items { get; }

			public AggregateObj()
			{
				Items = new List<IDebuggable>();
			}

			string IDebuggable.StringTreeName()
			{
				return "AGGREGATE";
			}

			IReadOnlyList<IDebuggable> IDebuggable.StringTreeChildren()
			{
				return Items;
			}
		}

		////////////////////////////////////////////////////////////////////////
		
		///// Terminals /////

		public override Terminal VisitTerminal(ITerminalNode node)
		{
			return new Terminal(node, lexerVocab);
		}

		////////////////////////////////////////////////////////////////////////

		///// Useful Things /////

		// TODO: move the facename stuff to be right below the datatype stuff

		public override object VisitFacename([NotNull] BassaltParser.FacenameContext context)
		{
			return base.VisitFacename(context);
		}

		public override FaceAccessModifier VisitFacenameAccessModifier([NotNull] BassaltParser.FacenameAccessModifierContext context)
		{
			FaceAccessModifier ret = FaceAccessModifier.Get(context.GetText());
			base.VisitFacenameAccessModifier(context);
			return ret;
		}

		public override DatatypeFaced VisitDatatype_immutface([NotNull] BassaltParser.Datatype_immutfaceContext context)
		{
			object children = base.VisitDatatype_immutface(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			Datatype inner = childrenR.Items[0] as Datatype;
			System.Diagnostics.Debug.Assert(inner is not null);
			Terminal exclam = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(exclam is not null);

			return new DatatypeFaced(Face.FcImmutable, inner);
		}

		public override object VisitDatatype_facename([NotNull] BassaltParser.Datatype_facenameContext context)
		{
			object children = base.VisitDatatype_facename(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			Datatype inner = childrenR.Items[0] as Datatype;
			System.Diagnostics.Debug.Assert(inner is not null);
			Terminal tilde = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(tilde is not null);
			Face facename = childrenR.Items[2] as Face;
			System.Diagnostics.Debug.Assert(facename is not null);

			return new DatatypeFaced(facename, inner);
		}

		public override object VisitDatatype_other([NotNull] BassaltParser.Datatype_otherContext context)
		{
			return base.VisitDatatype_other(context);
		}

		public override DatatypeNamespaced VisitDatatypeNamespaced_main([NotNull] BassaltParser.DatatypeNamespaced_mainContext context)
		{
			object children = base.VisitDatatypeNamespaced_main(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			Expr namespacee = childrenR.Items[0] as Expr;
			System.Diagnostics.Debug.Assert(namespacee is not null);
			Terminal doubleColon = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(doubleColon is not null);
			Datatype inner = childrenR.Items[2] as Datatype;
			System.Diagnostics.Debug.Assert(inner is not null);

			return new DatatypeNamespaced(namespacee, inner);
		}

		public override object VisitDatatypeNamespaced_other([NotNull] BassaltParser.DatatypeNamespaced_otherContext context)
		{
			return base.VisitDatatypeNamespaced_other(context);
		}

		public override Datatype VisitDatatypeBase_langtype([NotNull] BassaltParser.DatatypeBase_langtypeContext context)
		{
			Datatype ret = base.VisitDatatypeBase_langtype(context) as Datatype;
			System.Diagnostics.Debug.Assert(ret is not null);
			return ret;
		}

		public override DatatypeIdentifier VisitDatatypeBase_identifier([NotNull] BassaltParser.DatatypeBase_identifierContext context)
		{
			DatatypeIdentifier ret = new DatatypeIdentifier(context.IdentifierTerminal().GetText());
			base.VisitDatatypeBase_identifier(context);
			return ret;
		}

		public override DatatypeLang VisitLangType([NotNull] BassaltParser.LangTypeContext context)
		{
			DatatypeLang ret = DatatypeLang.Get(context.GetText());
			base.VisitLangType(context);
			return ret;
		}

		public override ExprLangVar VisitLangVar([NotNull] BassaltParser.LangVarContext context)
		{
			ExprLangVar ret = ExprLangVar.Get(context.GetText());
			base.VisitLangVar(context);
			return ret;
		}

		public override ExprIdentifier VisitIdentifier([NotNull] BassaltParser.IdentifierContext context)
		{
			ExprIdentifier ret = new ExprIdentifier(context.IdentifierTerminal().GetText());
			base.VisitIdentifier(context);
			return ret;
		}

		////////////////////////////////////////////////////////////////////////

		///// Literals /////

		public override ExprLiteral VisitLiteral_boolean([NotNull] BassaltParser.Literal_booleanContext context)
		{
			ExprLiteral ret = Reparsing.ReparseBool(context.literalBoolean().GetText(), bassaltSyntaxErrorHandler, context.Stop.Line, context.Stop.Column);
			base.VisitLiteral_boolean(context);
			return ret;
		}

		public override ExprLiteral VisitLiteral_null([NotNull] BassaltParser.Literal_nullContext context)
		{
			ExprLiteral ret = Reparsing.ReparseNull(context.literalNull().GetText(), bassaltSyntaxErrorHandler, context.Stop.Line, context.Stop.Column);
			base.VisitLiteral_null(context);
			return ret;
		}

		public override ExprLiteral VisitLiteral_integer([NotNull] BassaltParser.Literal_integerContext context)
		{
			ExprLiteral ret = base.VisitLiteral_integer(context) as ExprLiteral;
			System.Diagnostics.Debug.Assert(ret is not null);

			return ret;
		}

		public override ExprLiteral VisitLiteralInteger_decInt([NotNull] BassaltParser.LiteralInteger_decIntContext context)
		{
			ExprLiteral ret = Reparsing.ReparseDecInt(context.DecIntLiteral().GetText(), bassaltSyntaxErrorHandler, context.Stop.Line, context.Stop.Column);
			base.VisitLiteralInteger_decInt(context);
			return ret;
		}

		public override ExprLiteral VisitLiteralInteger_hexInt([NotNull] BassaltParser.LiteralInteger_hexIntContext context)
		{
			ExprLiteral ret = Reparsing.ReparseHexInt(context.HexIntLiteral().GetText(), bassaltSyntaxErrorHandler, context.Stop.Line, context.Stop.Column);
			base.VisitLiteralInteger_hexInt(context);
			return ret;
		}

		public override ExprLiteral VisitLiteralInteger_octalInt([NotNull] BassaltParser.LiteralInteger_octalIntContext context)
		{
			ExprLiteral ret = Reparsing.ReparseOctalInt(context.OctalIntLiteral().GetText(), bassaltSyntaxErrorHandler, context.Stop.Line, context.Stop.Column);
			base.VisitLiteralInteger_octalInt(context);
			return ret;
		}

		public override ExprLiteral VisitLiteralInteger_binaryInt([NotNull] BassaltParser.LiteralInteger_binaryIntContext context)
		{
			ExprLiteral ret = Reparsing.ReparseBinaryInt(context.BinaryIntLiteral().GetText(), bassaltSyntaxErrorHandler, context.Stop.Line, context.Stop.Column);
			base.VisitLiteralInteger_binaryInt(context);
			return ret;
		}

		public override ExprLiteral VisitLiteralInteger_char([NotNull] BassaltParser.LiteralInteger_charContext context)
		{
			ExprLiteral ret = Reparsing.ReparseChar(context.CharLiteral().GetText(), bassaltSyntaxErrorHandler, context.Stop.Line, context.Stop.Column);
			base.VisitLiteralInteger_char(context);
			return ret;
		}

		////////////////////////////////////////////////////////////////////////
		
		///// Expressions /////

		public override object VisitExpr([NotNull] BassaltParser.ExprContext context)
		{
			return base.VisitExpr(context);
		}

		public override object VisitExprLambda([NotNull] BassaltParser.ExprLambdaContext context)
		{
			return base.VisitExprLambda(context);
		}

		public override ExprConditional VisitExprConditional_main([NotNull] BassaltParser.ExprConditional_mainContext context)
		{
			object children = base.VisitExprConditional_main(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			IDebuggable condition = childrenR.Items[0];
			System.Diagnostics.Debug.Assert(condition is not null);
			Terminal questionMark = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(questionMark is not null);
			IDebuggable expressionA = childrenR.Items[2];
			System.Diagnostics.Debug.Assert(expressionA is not null);
			Terminal colon = childrenR.Items[3] as Terminal;
			System.Diagnostics.Debug.Assert(colon is not null);
			IDebuggable expressionB = childrenR.Items[4];
			System.Diagnostics.Debug.Assert(expressionB is not null);

			return new ExprConditional(condition, expressionA, expressionB);
		}

		public override object VisitExprConditional_other([NotNull] BassaltParser.ExprConditional_otherContext context)
		{
			return base.VisitExprConditional_other(context);
		}

		public override ExprBinaryOp VisitExprOr_main([NotNull] BassaltParser.ExprOr_mainContext context)
		{
			object children = base.VisitExprOr_main(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			IDebuggable lhs = childrenR.Items[0];
			System.Diagnostics.Debug.Assert(lhs is not null);
			Terminal op = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(op is not null);
			IDebuggable rhs = childrenR.Items[2];
			System.Diagnostics.Debug.Assert(rhs is not null);

			return new ExprBinaryOp(op, lhs, rhs);
		}

		public override object VisitExprOr_other([NotNull] BassaltParser.ExprOr_otherContext context)
		{
			return base.VisitExprOr_other(context);
		}

		public override ExprBinaryOp VisitExprAnd_main([NotNull] BassaltParser.ExprAnd_mainContext context)
		{
			object children = base.VisitExprAnd_main(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			IDebuggable lhs = childrenR.Items[0];
			System.Diagnostics.Debug.Assert(lhs is not null);
			Terminal op = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(op is not null);
			IDebuggable rhs = childrenR.Items[2];
			System.Diagnostics.Debug.Assert(rhs is not null);

			return new ExprBinaryOp(op, lhs, rhs);
		}

		public override object VisitExprAnd_other([NotNull] BassaltParser.ExprAnd_otherContext context)
		{
			return base.VisitExprAnd_other(context);
		}

		public override ExprBinaryOp VisitExprBitOr_main([NotNull] BassaltParser.ExprBitOr_mainContext context)
		{
			object children = base.VisitExprBitOr_main(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			IDebuggable lhs = childrenR.Items[0];
			System.Diagnostics.Debug.Assert(lhs is not null);
			Terminal op = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(op is not null);
			IDebuggable rhs = childrenR.Items[2];
			System.Diagnostics.Debug.Assert(rhs is not null);

			return new ExprBinaryOp(op, lhs, rhs);
		}

		public override object VisitExprBitOr_other([NotNull] BassaltParser.ExprBitOr_otherContext context)
		{
			return base.VisitExprBitOr_other(context);
		}

		public override ExprBinaryOp VisitExprBitXor_main([NotNull] BassaltParser.ExprBitXor_mainContext context)
		{
			object children = base.VisitExprBitXor_main(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			IDebuggable lhs = childrenR.Items[0];
			System.Diagnostics.Debug.Assert(lhs is not null);
			Terminal op = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(op is not null);
			IDebuggable rhs = childrenR.Items[2];
			System.Diagnostics.Debug.Assert(rhs is not null);

			return new ExprBinaryOp(op, lhs, rhs);
		}

		public override object VisitExprBitXor_other([NotNull] BassaltParser.ExprBitXor_otherContext context)
		{
			return base.VisitExprBitXor_other(context);
		}

		public override ExprBinaryOp VisitExprBitAnd_main([NotNull] BassaltParser.ExprBitAnd_mainContext context)
		{
			object children = base.VisitExprBitAnd_main(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			IDebuggable lhs = childrenR.Items[0];
			System.Diagnostics.Debug.Assert(lhs is not null);
			Terminal op = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(op is not null);
			IDebuggable rhs = childrenR.Items[2];
			System.Diagnostics.Debug.Assert(rhs is not null);

			return new ExprBinaryOp(op, lhs, rhs);
		}

		public override object VisitExprBitAnd_other([NotNull] BassaltParser.ExprBitAnd_otherContext context)
		{
			return base.VisitExprBitAnd_other(context);
		}

		public override ExprBinaryOp VisitExprEquality_main([NotNull] BassaltParser.ExprEquality_mainContext context)
		{
			object children = base.VisitExprEquality_main(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			IDebuggable lhs = childrenR.Items[0];
			System.Diagnostics.Debug.Assert(lhs is not null);
			Terminal op = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(op is not null);
			IDebuggable rhs = childrenR.Items[2];
			System.Diagnostics.Debug.Assert(rhs is not null);

			return new ExprBinaryOp(op, lhs, rhs);
		}

		public override object VisitExprEquality_other([NotNull] BassaltParser.ExprEquality_otherContext context)
		{
			return base.VisitExprEquality_other(context);
		}

		public override ExprBinaryOp VisitExprComparison_main([NotNull] BassaltParser.ExprComparison_mainContext context)
		{
			object children = base.VisitExprComparison_main(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			IDebuggable lhs = childrenR.Items[0];
			System.Diagnostics.Debug.Assert(lhs is not null);
			Terminal op = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(op is not null);
			IDebuggable rhs = childrenR.Items[2];
			System.Diagnostics.Debug.Assert(rhs is not null);

			return new ExprBinaryOp(op, lhs, rhs);
		}

		public override object VisitExprComparison_other([NotNull] BassaltParser.ExprComparison_otherContext context)
		{
			return base.VisitExprComparison_other(context);
		}

		public override ExprBinaryOp VisitExprBitshift_main([NotNull] BassaltParser.ExprBitshift_mainContext context)
		{
			object children = base.VisitExprBitshift_main(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			IDebuggable lhs = childrenR.Items[0];
			System.Diagnostics.Debug.Assert(lhs is not null);
			Terminal op = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(op is not null);
			IDebuggable rhs = childrenR.Items[2];
			System.Diagnostics.Debug.Assert(rhs is not null);

			return new ExprBinaryOp(op, lhs, rhs);
		}

		public override object VisitExprBitshift_other([NotNull] BassaltParser.ExprBitshift_otherContext context)
		{
			return base.VisitExprBitshift_other(context);
		}

		public override ExprBinaryOp VisitExprSum_main([NotNull] BassaltParser.ExprSum_mainContext context)
		{
			object children = base.VisitExprSum_main(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			IDebuggable lhs = childrenR.Items[0];
			System.Diagnostics.Debug.Assert(lhs is not null);
			Terminal op = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(op is not null);
			IDebuggable rhs = childrenR.Items[2];
			System.Diagnostics.Debug.Assert(rhs is not null);

			return new ExprBinaryOp(op, lhs, rhs);
		}

		public override object VisitExprSum_other([NotNull] BassaltParser.ExprSum_otherContext context)
		{
			return base.VisitExprSum_other(context);
		}

		public override ExprBinaryOp VisitExprProduct_main([NotNull] BassaltParser.ExprProduct_mainContext context)
		{
			object children = base.VisitExprProduct_main(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			IDebuggable lhs = childrenR.Items[0];
			System.Diagnostics.Debug.Assert(lhs is not null);
			Terminal op = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(op is not null);
			IDebuggable rhs = childrenR.Items[2];
			System.Diagnostics.Debug.Assert(rhs is not null);

			return new ExprBinaryOp(op, lhs, rhs);
		}

		public override object VisitExprProduct_other([NotNull] BassaltParser.ExprProduct_otherContext context)
		{
			return base.VisitExprProduct_other(context);
		}

		public override ExprUnaryOp VisitExprUnaryPrefix_main([NotNull] BassaltParser.ExprUnaryPrefix_mainContext context)
		{
			object children = base.VisitExprUnaryPrefix_main(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			Terminal op = childrenR.Items[0] as Terminal;
			System.Diagnostics.Debug.Assert(op is not null);
			IDebuggable inner = childrenR.Items[1];
			System.Diagnostics.Debug.Assert(inner is not null);

			return new ExprUnaryOp(op, inner);
		}

		public override ExprExplicitCast VisitExprUnaryPrefix_explicitcast([NotNull] BassaltParser.ExprUnaryPrefix_explicitcastContext context)
		{
			object children = base.VisitExprUnaryPrefix_explicitcast(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			Terminal percentSign = childrenR.Items[0] as Terminal;
			System.Diagnostics.Debug.Assert(percentSign is not null);
			Terminal openAngleBracket = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(openAngleBracket is not null);
			Datatype targetType = childrenR.Items[2] as Datatype;
			System.Diagnostics.Debug.Assert(targetType is not null);
			Terminal closeAngleBracket = childrenR.Items[3] as Terminal;
			System.Diagnostics.Debug.Assert(closeAngleBracket is not null);
			IDebuggable inner = childrenR.Items[4];
			System.Diagnostics.Debug.Assert(inner is not null);

			return new ExprExplicitCast(targetType, inner);
		}

		public override object VisitExprUnaryPrefix_other([NotNull] BassaltParser.ExprUnaryPrefix_otherContext context)
		{
			return base.VisitExprUnaryPrefix_other(context);
		}

		public override ExprNamespaced VisitExprNamespaceRes_main([NotNull] BassaltParser.ExprNamespaceRes_mainContext context)
		{
			object children = base.VisitExprNamespaceRes_main(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			IDebuggable namespacee = childrenR.Items[0];
			System.Diagnostics.Debug.Assert(namespacee is not null);
			Terminal doubleColon = childrenR.Items[1] as Terminal;
			System.Diagnostics.Debug.Assert(doubleColon is not null);
			IDebuggable inner = childrenR.Items[2];
			System.Diagnostics.Debug.Assert(inner is not null);

			return new ExprNamespaced(namespacee, inner);
		}

		public override object VisitExprNamespaceRes_other([NotNull] BassaltParser.ExprNamespaceRes_otherContext context)
		{
			return base.VisitExprNamespaceRes_other(context);
		}

		public override ExprLangVar VisitExprBase_langVar([NotNull] BassaltParser.ExprBase_langVarContext context)
		{
			ExprLangVar ret = base.VisitExprBase_langVar(context) as ExprLangVar;
			System.Diagnostics.Debug.Assert(ret is not null);
			return ret;
		}

		public override ExprDriftingDatatype VisitExprBase_langtype([NotNull] BassaltParser.ExprBase_langtypeContext context)
		{
			DatatypeLang langtype = base.VisitExprBase_langtype(context) as DatatypeLang;
			System.Diagnostics.Debug.Assert(langtype is not null);

			return new ExprDriftingDatatype(langtype);
		}

		public override ExprIdentifier VisitExprBase_identifier([NotNull] BassaltParser.ExprBase_identifierContext context)
		{
			ExprIdentifier ret = base.VisitExprBase_identifier(context) as ExprIdentifier;
			System.Diagnostics.Debug.Assert(ret is not null);
			return ret;
		}

		public override ExprLiteral VisitExprBase_literal([NotNull] BassaltParser.ExprBase_literalContext context)
		{
			ExprLiteral ret = base.VisitExprBase_literal(context) as ExprLiteral;
			System.Diagnostics.Debug.Assert(ret is not null);
			return ret;
		}

		public override object VisitExprBase_parenthesis([NotNull] BassaltParser.ExprBase_parenthesisContext context)
		{
			object children = base.VisitExprBase_parenthesis(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);

			Terminal openParen = childrenR.Items[0] as Terminal;
			System.Diagnostics.Debug.Assert(openParen is not null);
			IDebuggable innerExpr = childrenR.Items[1] as IDebuggable;
			System.Diagnostics.Debug.Assert(innerExpr is not null);
			Terminal closeParen = childrenR.Items[2] as Terminal;
			System.Diagnostics.Debug.Assert(closeParen is not null);

			return innerExpr;
		}

		////////////////////////////////////////////////////////////////////////
		
		///// Statements /////

		public override object VisitProgram([NotNull] BassaltParser.ProgramContext context)
		{
			// outFile.WriteLine("#include <stdio.h>");
			// outFile.WriteLine("int main(void) {");

			base.VisitProgram(context);

			// outFile.WriteLine("return 0;");
			// outFile.WriteLine("}");

			return null;
		}

		public override object VisitStatementPrint([NotNull] BassaltParser.StatementPrintContext context)
		{
			object children = base.VisitStatementPrint(context);
			if (children is null)
			{
				bassaltSyntaxErrorHandler.Add(context.Stop.Line, context.Stop.Column, "unkown error.");
				return null;
			}

			AggregateObj childrenR = children as AggregateObj;
			System.Diagnostics.Debug.Assert(childrenR is not null);
			
			Console.WriteLine("----------- print statement -----------");
			Console.WriteLine(IDebuggable.ToStringTree(childrenR));

			return null;
		}

		////////////////////////////////////////////////////////////////////////
	}
}
