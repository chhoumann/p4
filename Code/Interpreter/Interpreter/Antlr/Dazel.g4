grammar Dazel; // Defines grammar

WS  :   [ \t\r\n]+ -> skip;

/* PARSER RULES */
start: gameObject;

gameObject              : gameObjectType=(SCREEN | ENTITY | MOVE_PATTERN) IDENTIFIER L_BRACES gameObjectContents R_BRACES
                        ;

empty                   : 
                        ;


gameObjectContents      : gameObjectContent
                        | gameObjectContent gameObjectContents
                        | empty
                        ;

gameObjectContent       : screenContentType L_BRACES statementList R_BRACES
                        | entityContentType L_BRACES statementList R_BRACES
                        | movePatternContentType L_BRACES statementList R_BRACES
                        ;

screenContentType       : 'Map'                 
                        | 'OnScreenEntered'
                        | 'Entities'
                        | 'Exits'
                        ;

entityContentType       : 'Data'
                        ;


movePatternContentType  : 'Pattern'
                        ;

statementList           : statement ';'
                        | statement ';' statementList
                        | empty
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

memberAccess            : IDENTIFIER '.' IDENTIFIER
                        ;

valueList               : value
                        | value ',' valueList
                        | empty
                        ;

value                   : IDENTIFIER
                        | INT
                        | FLOAT
                        | array
                        | memberAccess
                        | functionInvocation
                        ;

array                   : L_BRACKET valueList R_BRACKET
                        ;


/* Lexer rules */
IDENTIFIER              : [a-zA-Z][a-zA-Z_0-9]*;
INT                     : [0-9]+;
FLOAT                   : [0-9]+'.'[0-9]+;
            
SCREEN                  : 'Screen ';
ENTITY                  : 'Entity ';
MOVE_PATTERN            : 'MovePattern ';

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
QUOTATION_MARK          : '"';