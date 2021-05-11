grammar Dazel; // Defines grammar

WS  :   [ \t\r\n]+ -> skip;
COMMENT : '//' ~[\r\n]* -> skip;

//SPACING : [\t\r\n]+?;
/* PARSER RULES */
start                   : gameObject;

gameObject              : gameObjectType=(SCREEN | ENTITY | MOVE_PATTERN) IDENTIFIER gameObjectBlock
                        ;
                        
gameObjectBlock         : L_BRACES gameObjectContents R_BRACES
                        ;

empty                   : 
                        ;


gameObjectContents      : gameObjectContent
                        | gameObjectContent gameObjectContents
                        | empty
                        ;

gameObjectContent       : gameObjectContentType=(MAP|ONSCREENENTERED|ENTITIES|EXITS|DATA|PATTERN) statementBlock 
                        ;
                       

statementList           : statement ';'
                        | statement ';' statementList
                        | statementBlock statementList
                        | empty
                        ;
                        
statementBlock          : L_BRACES statementList R_BRACES
                        ;

statement               : repeatLoop
                        | ifStatement
                        | statementExpression
                        ;

repeatLoop              : 'repeat' L_BRACES statementList R_BRACES
                        ;

ifStatement             : 'if' expression L_BRACES statementList R_BRACES
                        ;

// https://cs.au.dk/~amoeller/RegAut/JavaBNF.html
statementExpression     : assignment
                        | functionInvocation
                        ;

assignment              : IDENTIFIER ASSIGN_OP expression
                        | memberAccess ASSIGN_OP expression
                        ;

expression              : sumExpression;

sumExpression           : sumExpression sumOperation factorExpression
                        | factorExpression
                        ;

factorExpression        : factorExpression factorOperation terminalExpression
                        | terminalExpression
                        ;

terminalExpression      : value
	                    | L_PARANTHESIS expression R_PARANTHESIS
                        ;

sumOperation            : PLUS_OP 
                        | MINUS_OP;

factorOperation         : MULTIPLICATION_OP 
                        | DIVISION_OP
                        ;

functionInvocation      : IDENTIFIER L_PARANTHESIS valueList R_PARANTHESIS
                        ;

memberAccess            : IDENTIFIER '.' type=(MAP|ONSCREENENTERED|ENTITIES|EXITS|DATA|PATTERN) '.' IDENTIFIER
                        | IDENTIFIER '.' IDENTIFIER
                        ;

valueList               : value
                        | value ',' valueList
                        | empty
                        ;

value                   : terminalValue=(IDENTIFIER|INT|FLOAT|STRING)
                        | array
                        | memberAccess
                        | functionInvocation
                        ;

array                   : L_BRACKET valueList R_BRACKET
                        ;


/* Lexer rules */
SCREEN                  : 'Screen';
ENTITY                  : 'Entity';
MOVE_PATTERN            : 'MovePattern';
MAP                     : 'Map';
ONSCREENENTERED         : 'OnScreenEntered';
ENTITIES                : 'Entities';
EXITS                   : 'Exits';
DATA                    : 'Data';
PATTERN                 : 'Pattern';

IDENTIFIER              : [a-zA-Z][a-zA-Z_0-9]*;
INT                     : [0-9]+;
FLOAT                   : [0-9]+'.'[0-9]+;
STRING                  : '"' .*? '"';
            
L_PARANTHESIS           : '(';
R_PARANTHESIS           : ')';
L_BRACKET               : '[';
R_BRACKET               : ']';
L_BRACES                : '{';
R_BRACES                : '}';
ASSIGN_OP               : '=';
LESSTHAN_OP             : '<';
GREATERTHAN_OP          : '>';
PLUS_OP                 : '+';
MINUS_OP                : '-';
MULTIPLICATION_OP       : '*';
DIVISION_OP             : '/';