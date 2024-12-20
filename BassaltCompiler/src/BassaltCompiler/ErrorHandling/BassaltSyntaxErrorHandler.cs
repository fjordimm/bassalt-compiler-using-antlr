
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using Antlr4.Runtime;

namespace BassaltCompiler.ErrorHandling
{
	class BassaltSyntaxErrorListener : BaseErrorListener
	{
		private readonly BassaltSyntaxErrorHandler errorHandler;

		public BassaltSyntaxErrorListener(BassaltSyntaxErrorHandler errorHandler)
		{
			this.errorHandler = errorHandler;
		}

		public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
		{
			errorHandler.Add(line, charPositionInLine, $"{msg} (given by Antlr4).");
		}
	}

	class BassaltSyntaxErrorHandler
	{
		private readonly List<BassaltSyntaxError> _syntaxErrors;
		public ReadOnlyCollection<BassaltSyntaxError> Errors { get => _syntaxErrors.AsReadOnly(); }

		public BassaltSyntaxErrorHandler()
		{
			_syntaxErrors = new List<BassaltSyntaxError>();
		}
		
		public void Add(int line, int charPosition, string message)
		{
			_syntaxErrors.Add(new BassaltSyntaxError(line, charPosition, message));
		}

		public void PrintErrors(string filename, TextWriter errorOut)
		{
			errorOut.WriteLine($"===== BASSALT SYNTAX ERRORS ===== in '{filename}':");
			foreach (BassaltSyntaxError syntaxError in _syntaxErrors)
			{
				errorOut.WriteLine($"  ({syntaxError.Line}, {syntaxError.CharPosition}): {syntaxError.Message}");
			}
		}
	}

	class BassaltSyntaxError
	{
		public int Line { get; }
		public int CharPosition { get; }
		public string Message { get; }

		public BassaltSyntaxError(int line, int charPosition, string message)
		{
			Line = line;
			CharPosition = charPosition;
			Message = message;
		}
	}
}
