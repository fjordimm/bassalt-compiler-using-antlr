
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
	: BoolLiteral
	;

literalInteger
	: DecIntLiteral
	| HexIntLiteral
	| OctalIntLiteral
	| BinaryIntLiteral
	| CharLiteral
	;

literalFractional
	: PlainFracLiteral
	| ScientificFracLiteral
	| ScientificWholeNumLiteral
	;

literalString
	: StringLiteral
	;


///// Tokens /////

// Keywords

Kprint : 'print' ;

Identifier : [a-zA-Z_][a-zA-Z_0-9]* ;

// Literals

BoolLiteral
	: 'true' | 'false'
	;

DecIntLiteral
	: [-+]? [0-9]+ ('_'? ('c8' | 'c' | 'c32' | 'sb' | 's' | 'i' | 'l' | 'b' | 'us' | 'ui' | 'ul' | 'f' | 'd' | 'ii' | 'uii' | 'iii' | 'uiii' | 'r' | 't'))?
	;

HexIntLiteral
	: [-+]? '0' [xX] [0-9a-fA-F]+ ('_' ('c8' | 'c' | 'c32' | 'sb' | 's' | 'i' | 'l' | 'b' | 'us' | 'ui' | 'ul' | 'f' | 'd' | 'ii' | 'uii' | 'iii' | 'uiii' | 'r' | 't'))?
	;

OctalIntLiteral
	: [-+]? '0' [oO] [0-7]+ ('_'? ('c8' | 'c' | 'c32' | 'sb' | 's' | 'i' | 'l' | 'b' | 'us' | 'ui' | 'ul' | 'f' | 'd' | 'ii' | 'uii' | 'iii' | 'uiii' | 'r' | 't'))?
	;

BinaryIntLiteral
	: [-+]? '0' [bB] [0-1]+ ('_'? ('c8' | 'c' | 'c32' | 'sb' | 's' | 'i' | 'l' | 'b' | 'us' | 'ui' | 'ul' | 'f' | 'd' | 'ii' | 'uii' | 'iii' | 'uiii' | 'r' | 't'))?
	;

PlainFracLiteral
	: [-+]? [0-9]+ '.' [0-9]+ ('_'? ('c8' | 'c' | 'c32' | 'sb' | 's' | 'i' | 'l' | 'b' | 'us' | 'ui' | 'ul' | 'f' | 'd' | 'ii' | 'uii' | 'iii' | 'uiii' | 'r' | 't'))?
	;

ScientificFracLiteral
	: [-+]? [0-9]+ '.' [0-9]+ [eE] [-+]? [0-9]+ ('_'? ('c8' | 'c' | 'c32' | 'sb' | 's' | 'i' | 'l' | 'b' | 'us' | 'ui' | 'ul' | 'f' | 'd' | 'ii' | 'uii' | 'iii' | 'uiii' | 'r' | 't'))?
	;

ScientificWholeNumLiteral
	: [-+]? [0-9]+ [eE] [-+]? [0-9]+ ('_'? ('c8' | 'c' | 'c32' | 'sb' | 's' | 'i' | 'l' | 'b' | 'us' | 'ui' | 'ul' | 'f' | 'd' | 'ii' | 'uii' | 'iii' | 'uiii' | 'r' | 't'))?
	;

CharLiteral
	: '\'' (~['\\] | '\\' .)* '\''
	;

StringLiteral
	: '"' (~["\\] | '\\' .)* '"'
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
