grammar Bassalt;

program
	: Kprogram statementList
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
	: Kprint ConstantDecInt
	;


// Tokens

Kprogram : 'program' ;
Kprint : 'print' ;

Identifier : [a-zA-Z_][a-zA-Z_0-9]* ;

LeftParen : '(' ;
RightParen : ')' ;

LeftBracket : '[' ;
RightBracket : ']' ;

LeftBrace : '{' ;
RightBrace : '}' ;

Assign : '=' ;
Semi : ';' ;

ConstantDecInt : [0-9]+ ;

ConstantHexInt : '0' [xX] [0-9a-fA-F]+ ;

ConstantDecFrac : [0-9]+ '.' [0-9]+ ;


// To be ignored

Whitespace : [ \t\r\n]+ -> skip ;