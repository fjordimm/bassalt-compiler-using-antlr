
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
	: KPrint literal
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

KPrint: 'print' ; // temporary

KIf: 'if' ;
KElse: 'else' ;
KLoop: 'loop' ;
KWhile: 'while' ;
KDo: 'do' ;
KFor: 'for' ;
KBreak: 'break' ;
KContinue: 'continue' ;
KIn: 'in' ;
KSwitch: 'switch' ;
KCase: 'case' ;
KReturn: 'return' ;

KUsing: 'using' ;
KAssert: 'assert' ;
KThrow: 'throw' ;
KSizeof: 'sizeof' ;
KTypeof: 'typeof' ;
KNew: 'new' ;
KRnew: 'rnew' ;
KSnew: 'snew' ;
KRrealloc: 'rrealloc' ;
KRfree: 'rfree' ;
KCede: 'cede' ;
KBitcast: 'bitcast' ;

KNamespace: 'namespace' ;
KModule: 'module' ;
KInterface: 'interface' ;
KClass: 'class' ;
KStruct: 'struct' ;
KEnum: 'enum' ;
KFace: 'face' ;
KSingleton: 'singleton' ;

KPublic: 'public' ;
KPrivate: 'private' ;
KProtected: 'protected' ;
KPassable: 'passable' ;
KVirtual: 'virtual' ;
KImplement: 'implement' ;
KOverride: 'override' ;
KOperator: 'operator' ;
KUnsafe: 'unsafe' ;
KExtern: 'extern' ;
KConst: 'const' ;
KInline: 'inline' ;
KOwner: 'owner' ;
KNamed: 'named' ;
KRef: 'ref' ;
KInref: 'inref' ;
KOutref: 'outref' ;

KThis: 'this' ;
KBase: 'base' ;
KStdout: 'stdout' ;
KStdin: 'stdin' ;
KStderr: 'stderr' ;
KPlaceholder: 'placeholder' ;

KTrue: 'true' ;
KFalse: 'false' ;
KNull: 'null' ;

KVoid: 'void' ;
KFunc: 'func' ;
KSptr: 'sptr' ;
KWsptr: 'wsptr' ;

KBool: 'bool' ;
KChar: 'char' ;
KChar8: 'char8' ;
KChar16: 'char16' ;
KSbyte: 'sbyte' ;
KByte: 'byte' ;
KShort: 'short' ;
KUshort: 'ushort' ;
KInt: 'int' ;
KUint: 'uint' ;
KLong: 'long' ;
KUlong: 'ulong' ;
KFloat: 'float' ;
KDouble: 'double' ;

KString: 'string' ;
KInt128: 'int128' ;
KUint128: 'uint128' ;
KInt256: 'int256' ;
KUint256: 'uint256' ;
KFloat128: 'float128' ;
KFloat256: 'float256' ;

Identifier: [a-zA-Z_][a-zA-Z_0-9]* ;

// Operators

// Problems with operator ambiguity:
///// &= vs & =
// maybe &= = vs & ==
// != vs ! =
///// >= vs a<a> =

// LParen: '(' ;
// RParen: ')' ;
// LBracket: '[' ;
// RBracket: ']' ;
// LBrace: '{' ;
// RBrace: '}' ;

// Plus: '+' ;
// Minus: '-' ;
// Star: '*' ;
// Div: '/' ;
// Percent: '%' ;
// Caret: '^' ;
// And: '&' ;
// Or: '|' ;
// Tilde: '~' ;
// Bang: '!' ;
// Assign: '=' ;
// Less: '<' ;
// Greater: '>' ;

// PlusAssign: '+=' ;
// MinusAssign: '-=' ;
// StarAssign: '*=' ;
// DivAssign: '/=' ;
// PercentAssign: '%=' ;
// CaretAssign: '^=' ;
// AndAssign: '&=' ;
// OrAssign: '|=' ;

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
