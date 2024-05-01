grammar Bassalt;
program  : 'main' '{' unit unit unit '}' ;
unit : ID ;
ID : [a-z]+ ;
WS : [ \t\r\n]+ -> skip ;