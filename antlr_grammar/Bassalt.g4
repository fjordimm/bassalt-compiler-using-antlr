
grammar Bassalt;

///// Rules /////

program
	: statementList
	;

statementList
	: '{' statement* '}'
	| statement
	;

statement
	: statementNoSemi ';'
	;

statementNoSemi
	: statementPrint
	;

statementPrint
	: Kprint literal
	;

// Literals

literal
	: literalBoolean
	| literalInteger
	| literalFractional
	| literalString
	;

literalBoolean
	: LiteralBool
	;

literalInteger
	: LiteralDecInt
	| LiteralHexInt
	| LiteralOctalInt
	| LiteralBinaryInt
	| LiteralChar
	;

literalFractional
	: LiteralPlainFrac
	| LiteralScientificFrac
	| LiteralScientificWholeNum
	;

literalString
	: 'string____________'
	;


///// Tokens /////

// Keywords

Kprint : 'print' ;

Identifier : [a-zA-Z_][a-zA-Z_0-9]* ;

// Literals

LiteralBool
	: 'true' | 'false'
	;

LiteralDecInt
	: [-+]? [0-9]+ ('_'? ('c8' | 'c' | 'c32' | 'sb' | 's' | 'i' | 'l' | 'b' | 'us' | 'ui' | 'ul' | 'f' | 'd' | 'ii' | 'uii' | 'iii' | 'uiii' | 'r' | 't'))?
	;

LiteralHexInt
	: [-+]? '0' [xX] [0-9a-fA-F]+ ('_' ('c8' | 'c' | 'c32' | 'sb' | 's' | 'i' | 'l' | 'b' | 'us' | 'ui' | 'ul' | 'f' | 'd' | 'ii' | 'uii' | 'iii' | 'uiii' | 'r' | 't'))?
	;

LiteralOctalInt
	: [-+]? '0' [oO] [0-7]+ ('_'? ('c8' | 'c' | 'c32' | 'sb' | 's' | 'i' | 'l' | 'b' | 'us' | 'ui' | 'ul' | 'f' | 'd' | 'ii' | 'uii' | 'iii' | 'uiii' | 'r' | 't'))?
	;

LiteralBinaryInt
	: [-+]? '0' [bB] [0-1]+ ('_'? ('c8' | 'c' | 'c32' | 'sb' | 's' | 'i' | 'l' | 'b' | 'us' | 'ui' | 'ul' | 'f' | 'd' | 'ii' | 'uii' | 'iii' | 'uiii' | 'r' | 't'))?
	;

LiteralPlainFrac
	: [-+]? [0-9]+ '.' [0-9]+ ('_'? ('c8' | 'c' | 'c32' | 'sb' | 's' | 'i' | 'l' | 'b' | 'us' | 'ui' | 'ul' | 'f' | 'd' | 'ii' | 'uii' | 'iii' | 'uiii' | 'r' | 't'))?
	;

LiteralScientificFrac
	: [-+]? [0-9]+ '.' [0-9]+ [eE] [-+]? [0-9]+ ('_'? ('c8' | 'c' | 'c32' | 'sb' | 's' | 'i' | 'l' | 'b' | 'us' | 'ui' | 'ul' | 'f' | 'd' | 'ii' | 'uii' | 'iii' | 'uiii' | 'r' | 't'))?
	;

LiteralScientificWholeNum
	: [-+]? [0-9]+ [eE] [-+]? [0-9]+ ('_'? ('c8' | 'c' | 'c32' | 'sb' | 's' | 'i' | 'l' | 'b' | 'us' | 'ui' | 'ul' | 'f' | 'd' | 'ii' | 'uii' | 'iii' | 'uiii' | 'r' | 't'))?
	;

LiteralChar
	: '\'' (~['\\] | '\\' .)+ '\''
	;

// Other

Whitespace
	: [ \t\r\n]+ -> skip
	;

BlockComment
	: '/*' .*? '*/' -> skip
	;

LineComment
	: '//' ~[\r\n]* -> skip
	;

Invalid
	: .
	;
