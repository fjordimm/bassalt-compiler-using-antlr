
grammar Bassalt;

///// Rules /////

// Useful Things

// TODO
argumentList
	: identifier
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
	: datatypeBase '::' datatypeNamespaced		#datatypeNamespaced_main
	| datatypeBase								#datatypeNamespaced_other
	;

datatypeBase
	: langType
	| identifier
	;

face
	: datatypeNamespaced
	| KPublic
	| KPrivate
	| KProtected
	;

langType
	: KVoid
	| KFunc
	| KSptr
	| KWsptr
	| KBool
	| KChar
	| KChar8
	| KChar16
	| KChar32
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

identifier
	: IdentifierTerminal
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
	: exprOr '?' expr ':' exprConditional		#exprConditional_main
	| exprOr									#exprConditional_other
	;

exprOr
	: exprOr '||' exprAnd		#exprOr_main
	| exprAnd					#exprOr_other
	;

exprAnd
	: exprAnd '&&' exprBitOr	#exprAnd_main
	| exprBitOr					#exprAnd_other
	;

exprBitOr
	: exprBitOr '|' exprBitXor		#exprBitOr_main
	| exprBitXor					#exprBitOr_other
	;

exprBitXor
	: exprBitXor '^' exprBitAnd		#exprBitXor_main
	| exprBitAnd					#exprBitXor_other
	;

exprBitAnd
	: exprBitAnd '&' exprEquality		#exprBitAnd_main
	| exprEquality						#exprBitAnd_other
	;

exprEquality
	: exprEquality '==' exprComparison		#exprEquality_main
	| exprEquality '!=' exprComparison		#exprEquality_main
	| exprComparison						#exprEquality_other
	;

exprComparison
	: exprComparison '<' exprBitshift		#exprComparison_main
	| exprComparison '>' exprBitshift		#exprComparison_main
	| exprComparison '<=' exprBitshift		#exprComparison_main
	| exprComparison '>=' exprBitshift		#exprComparison_main
	| exprBitshift							#exprComparison_other
	;

exprBitshift
	: exprBitshift '<<' exprSum		#exprBitshift_main
	| exprBitshift '>>' exprSum		#exprBitshift_main
	| exprBitshift '<<<' exprSum	#exprBitshift_main
	| exprBitshift '>>>' exprSum	#exprBitshift_main
	| exprSum						#exprBitshift_other
	;

exprSum
	: exprSum '+' exprProduct	#exprSum_main
	| exprSum '-' exprProduct	#exprSum_main
	| exprProduct				#exprSum_other
	;

exprProduct
	: exprProduct '*' exprUnaryPrefix	#exprProduct_main
	| exprProduct '/' exprUnaryPrefix	#exprProduct_main
	| exprProduct '%' exprUnaryPrefix	#exprProduct_main
	| exprUnaryPrefix					#exprProduct_other
	;

exprUnaryPrefix
	: '+' exprUnaryPrefix						#exprUnaryPrefix_main
	| '-' exprUnaryPrefix						#exprUnaryPrefix_main
	| '!' exprUnaryPrefix						#exprUnaryPrefix_main
	| '~' exprUnaryPrefix						#exprUnaryPrefix_main
	| '%' exprUnaryPrefix						#exprUnaryPrefix_main
	| '%' '<' datatype '>' exprUnaryPrefix		#exprUnaryPrefix_explicitcast
	| KRfree exprUnaryPrefix					#exprUnaryPrefix_main
	| KCede exprUnaryPrefix						#exprUnaryPrefix_main
	| KRef exprUnaryPrefix						#exprUnaryPrefix_main
	| KInref exprUnaryPrefix					#exprUnaryPrefix_main
	| KOutref exprUnaryPrefix					#exprUnaryPrefix_main
	| exprUnarySuffix							#exprUnaryPrefix_other
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
	: langType '::' exprNamespaceRes		#exprNamespaceRes_main
	| langVar '::' exprNamespaceRes			#exprNamespaceRes_main
	| identifier '::' exprNamespaceRes		#exprNamespaceRes_main
	| exprBase								#exprNamespaceRes_other
	;

exprBase
	: langVar				#exprBase_langVar
	| identifier			#exprBase_identifier
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
KChar32: 'char32' ;
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

IdentifierTerminal: [a-zA-Z_][a-zA-Z_0-9]* ;

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
