grammar Dazel; // Defines grammar

WS  :   [ \t\r\n]+ -> skip;

/* PARSER RULES */
start: gameObject;

gameObject              : gameObjectType IDENTIFIER L_BRACES gameObjectContents R_BRACES
                        ;

empty                   : 
                        ;

gameObjectType          : 'Screen ' | 'Entity ' | 'MovePattern' 
                        ;

gameObjectContents      : gameObjectContent
                        | gameObjectContent gameObjectContents
                        | empty
                        ;

gameObjectContent       : contentScreenType L_BRACES statementList R_BRACES
                        | contentEntityType L_BRACES statementList R_BRACES
                        | contentMovePatternType L_BRACES statementList R_BRACES
                        ;

contentScreenType       : 'Map'                 
                        | 'OnScreenEntered'
                        | 'Entities'
                        | 'Exits'
                        ;

contentEntityType       : 'Data'
                        ;


contentMovePatternType  : 'Pattern'
                        ;

statementList           : statement ';'
                        | statement ';' statementList
                        | empty
                        ;

statement               : repeatLoop
                        | assignment
                        | functionInvocation
                        | ifStatement
                        ;

repeatLoop              : 'repeat' L_BRACES statementList R_BRACES
                        ;

ifStatement             : 'if' expression L_BRACES statementList R_BRACES
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
                        | functionInvocation
                        | memberAccess
                        ;

array                   : L_BRACKET valueList R_BRACKET
                        ;


/* Lexer rules */
IDENTIFIER              : [a-zA-Z][a-zA-Z_0-9]*;
INT                     : [0-9]+;
FLOAT                   : [0-9]+'.'[0-9]+;
            
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