
grammar Bassalt;

///// Rules /////

program
	: statementList
	;

statementList
	: LeftBrace statement* RightBrace
	| statement
	;

statement
	: statementNoSemi Semi
	;

statementNoSemi
	: statementPrint
	;

statementPrint
	: Kprint LiteralDecInt
	;

// Literals

literal
	: literalBool
	| literalInteger
	| literalFractional
	| literalString
	;

literalBool
	: LiteralBool
	;

literalInteger
	: LiteralDecInt
	| LiteralHexInt
	| LiteralOctalInt
	| LiteralBinaryInt
	| LiteralChar
	| LiteralRgba
	| LiteralDatetime
	;

literalFractional
	: LiteralDecInt
	| LiteralPlainFrac
	| LiteralScientificFrac
	;

literalString
	: 'string____________'
	;


///// Tokens /////

Identifier : [a-zA-Z_][a-zA-Z_0-9]* ;

LeftParen : '(' ;
RightParen : ')' ;

LeftBracket : '[' ;
RightBracket : ']' ;

LeftBrace : '{' ;
RightBrace : '}' ;

Blign : '=2' ;
Assign : '=' ;
Semi : ';' ;

// Keywords

Kprint : 'print' ;

// Literals

LiteralSuffix : 'c8' | 'c' | 'c32' | 'sb' | 's' | 'i' | 'l' | 'b' | 'us' | 'ui' | 'ul' | 'f' | 'd' | 'ii' | 'uii' | 'iii' | 'uiii' | 'r' | 't' ;

LiteralBool
	: ConstantBool
	;

LiteralDecInt
	: '-' LiteralDecIntP
	| LiteralDecIntP
	;

LiteralDecIntP
	: ConstantPlainInt '_' LiteralSuffix
	| ConstantPlainInt LiteralSuffix
	| ConstantPlainInt
	;

LiteralHexInt
	: '-' LiteralHexIntP
	| LiteralHexIntP
	;

LiteralHexIntP
	: '0' [xX] ConstantExtendedInt '_' LiteralSuffix
	| '0' [xX] ConstantExtendedInt
	;

LiteralOctalInt
	: '-' LiteralOctalIntP
	| LiteralOctalIntP
	;

LiteralOctalIntP
	: '0' [oO] ConstantExtendedInt '_' LiteralSuffix
	| '0' [oO] ConstantExtendedInt
	;

LiteralBinaryInt
	: '-' LiteralBinaryIntP
	| LiteralBinaryIntP
	;

LiteralBinaryIntP
	: '0' [bB] ConstantExtendedInt '_' LiteralSuffix
	| '0' [bB] ConstantExtendedInt
	;

LiteralPlainFrac
	: '-' LiteralPlainFracP
	| LiteralPlainFracP
	;

LiteralPlainFracP
	: ConstantPlainInt '.' ConstantPlainInt '_' LiteralSuffix
	| ConstantPlainInt '.' ConstantPlainInt LiteralSuffix
	| ConstantPlainInt '.' ConstantPlainInt
	;

LiteralScientificFrac
	: '-' LiteralScientificFracP
	| LiteralScientificFracP
	;

LiteralScientificFracP
	: LiteralScientificFracPP '_' LiteralSuffix
	| LiteralScientificFracPP LiteralSuffix
	| LiteralScientificFracPP
	;

LiteralScientificFracPP
	: ConstantPlainInt '.' ConstantPlainInt [eE] [-+]? ConstantPlainInt
	| ConstantPlainInt [eE] [-+]? ConstantPlainInt
	;

LiteralChar : 'char________' ;
LiteralRgba : 'rgba_________' ;
LiteralDatetime : 'datetime____________' ;

ConstantBool : 'true' | 'false' ;
ConstantPlainInt : [0-9]+ ;
ConstantExtendedInt : [0-9a-fA-F]+ ;


// To be ignored

Whitespace : [ \t\r\n]+ -> skip ;
