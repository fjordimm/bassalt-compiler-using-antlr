
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Antlr4.Runtime;

namespace BassaltCompiler.ErrorHandling
{
	class BassaltSemanticErrorHandler
	{
		private readonly List<BassaltSemanticError> _semanticErrors;
		public ReadOnlyCollection<BassaltSemanticError> Errors { get => _semanticErrors.AsReadOnly(); }

		public BassaltSemanticErrorHandler()
		{
			_semanticErrors = new List<BassaltSemanticError>();
		}

		public void PrintErrors(string filename, TextWriter errorOut)
		{
			errorOut.WriteLine($"===== BASSALT SEMANTIC ERRORS ===== in '{filename}'");
			foreach (BassaltSemanticError semanticError in _semanticErrors)
			{
				errorOut.WriteLine($"  ({semanticError.Line}, {semanticError.CharPosition}): {semanticError.Message}.");
			}
		}
	}

	class BassaltSemanticError
	{
		public int Line { get; }
		public int CharPosition { get; }
		public string Message { get; }

		public BassaltSemanticError(int line, int charPosition, string message)
		{
			Line = line;
			CharPosition = charPosition;
			Message = message;
		}
	}
}
