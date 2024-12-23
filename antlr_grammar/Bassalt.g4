
grammar Bassalt;

///// Rules /////

// Useful Things

// TODO
argumentList
	: Identifier
	;

datatypeList
	: datatype? (',' datatype)*
	;

datatype
	: datatype '!'
	| datatype '~' face
	| '(' datatypeList ')'
	| datatype '<' datatypeList '>'
	| datatype '[' exprList ']'
	| datatype '*'
	| datatype '&'
	| datatype '^'
	| datatypeNamespaced
	;

datatypeNamespaced
	: datatypeBase '::' datatypeNamespaced
	| datatypeBase
	;

datatypeBase
	: langDatatype		#datatypeBase_langtype
	| Identifier		#datatypeBase_identifier
	;

face
	: datatypeNamespaced
	| KPublic
	| KPrivate
	| KProtected
	;

langDatatype
	: KVoid
	| KFunc
	| KSptr
	| KWsptr
	| KBool
	| KChar
	| KChar8
	| KChar16
	| KSbyte
	| KByte
	| KShort
	| KUshort
	| KInt
	| KUint
	| KLong
	| KUlong
	| KFloat
	| KDouble
	| KString
	| KInt128
	| KUint128
	| KInt256
	| KUint256
	| KFloat128
	| KFloat256
	;

langVar
	: KThis
	| KBase
	| KStdout
	| KStdin
	| KStderr
	| KPlaceholder
	;

// Literals

literal
	: literalBoolean		#literal_boolean
	| literalNull			#literal_null
	| literalInteger		#literal_integer
	| literalFractional		#literal_fractional
	| literalString			#literal_string
	;

literalBoolean
	: KTrue | KFalse
	;

literalNull
	: KNull
	;

literalInteger
	: DecIntLiteral		#literalInteger_decInt
	| HexIntLiteral		#literalInteger_hexInt
	| OctalIntLiteral	#literalInteger_octalInt
	| BinaryIntLiteral	#literalInteger_binaryInt
	| CharLiteral		#literalInteger_char
	;

literalFractional
	: PlainFracLiteral				#literalFractional_plainFrac
	| ScientificFracLiteral			#literalFractional_scientificFrac
	| ScientificWholeNumLiteral		#literalFractional_scientificWholeNum
	;

literalString
	: StringLiteral
	;

// Expressions

exprList
	: expr? (',' expr)*
	;

expr
	: exprLambda
	;

exprLambda
	: '(' argumentList ')' '->' '{' statementList '}'
	| exprConditional
	;

exprConditional
	: exprOr '?' expr ':' exprConditional
	| exprOr
	;

exprOr
	: exprOr '||' exprAnd
	| exprAnd
	;

exprAnd
	: exprAnd '&&' exprBitOr
	| exprBitOr
	;

exprBitOr
	: exprBitOr '|' exprBitXor
	| exprBitXor
	;

exprBitXor
	: exprBitXor '^' exprBitAnd
	| exprBitAnd
	;

exprBitAnd
	: exprBitAnd '&' exprEquality
	| exprEquality
	;

exprEquality
	: exprEquality '==' exprComparison
	| exprEquality '!=' exprComparison
	| exprComparison
	;

exprComparison
	: exprComparison '<' exprBitshift
	| exprComparison '>' exprBitshift
	| exprComparison '<=' exprBitshift
	| exprComparison '>=' exprBitshift
	| exprBitshift
	;

exprBitshift
	: exprBitshift '<<' exprSum
	| exprBitshift '>>' exprSum
	| exprBitshift '<<<' exprSum
	| exprBitshift '>>>' exprSum
	| exprSum
	;

exprSum
	: exprSum '+' exprProduct	#exprSum_plus
	| exprSum '-' exprProduct	#exprSum_minus
	| exprProduct				#exprSum_other
	;

exprProduct
	: exprProduct '*' exprUnaryPrefix
	| exprProduct '/' exprUnaryPrefix
	| exprProduct '%' exprUnaryPrefix
	| exprUnaryPrefix
	;

exprUnaryPrefix
	: '+' exprUnaryPrefix
	| '-' exprUnaryPrefix
	| '!' exprUnaryPrefix
	| '~' exprUnaryPrefix
	| '%' exprUnaryPrefix
	| '%' '<' datatype '>' exprUnaryPrefix
	| KRfree exprUnaryPrefix
	| KCede exprUnaryPrefix
	| KRef exprUnaryPrefix
	| KInref exprUnaryPrefix
	| KOutref exprUnaryPrefix
	| exprUnarySuffix
	;

exprUnarySuffix
	: exprUnarySuffix '++'
	| exprUnarySuffix '--'
	| exprUnarySuffix '(' exprList ')'
	| exprUnarySuffix '[' exprList ']'
	| exprUnarySuffix '@'
	| exprUnarySuffix '&'
	| exprUnarySuffix '!'
	| exprNew
	;

exprNew
	: KNew datatype '{' expr '}'
	| KNew datatype '(' exprList ')'
	| KRnew datatype '{' expr '}'
	| KRnew datatype '(' exprList ')'
	| KSnew datatype '{' expr '}'
	| KSnew datatype '(' exprList ')'
	| exprDotAndVia
	;

exprDotAndVia
	: exprDotAndVia '.' exprNamespaceRes
	| exprDotAndVia '~' face
	| exprNamespaceRes
	;

exprNamespaceRes
	: langDatatype '::' exprNamespaceRes	#exprNamespaceRes_langtype
	| langVar '::' exprNamespaceRes			#exprNamespaceRes_langvar
	| Identifier '::' exprNamespaceRes		#exprNamespaceRes_identifier
	| exprBase								#exprNamespaceRes_other
	;

exprBase
	: langVar				#exprBase_langVar
	| Identifier			#exprBase_identifier
	| literal				#exprBase_literal
	| '(' expr ')'			#exprBase_parenthesis
	;

// Statements

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
	: KPrint expr
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
KRef: 'ref' ;
KInref: 'inref' ;
KOutref: 'outref' ;

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
KAbstract: 'abstract' ;
KImplement: 'implement' ;
KOverride: 'override' ;
KOperator: 'operator' ;
KUnsafe: 'unsafe' ;
KExtern: 'extern' ;
KConst: 'const' ;
KInline: 'inline' ;
KOwner: 'owner' ;
KNamed: 'named' ;

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

// Identifiers

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
