grammar Dazel; // Defines grammar

WS  :   [ \t\r\n]+ -> skip;

/* PARSER RULES */
start: gameObject;

gameObject             : gameObjectType IDENTIFIER L_BRACES gameObjectContents R_BRACES
                        ;

empty                   : 
                        ;

gameObjectType        : 'Screen ' | 'Entity ' | 'MovePattern'
                        ;

gameObjectContents    : gameObjectContent
                        | gameObjectContent gameObjectContents
                        | empty
                        ;

gameObjectContent     : content_type L_BRACES statementList R_BRACES
                        ;

content_type            : 'Map'
                        | 'OnScreenEntered'
                        | 'Entities'
                        | 'Exits'
                        | 'Data'
                        | 'Pattern'
                        ;

statementList          : statement ';'
                        | statement ';' statementList
                        | empty
                        ;

statement               : repeat_loop
                        | assignment
                        | function_invocation
                        | if_statement
                        ;

repeat_loop             : 'repeat' L_BRACES statementList R_BRACES
                        ;

if_statement            : 'if' expression L_BRACES statementList R_BRACES
                        ;

assignment              : IDENTIFIER ASSIGN_OP expression
                        ;

expression              : sum_expression;

sum_expression          : sum_expression sum_operation factor_expression
                        | factor_expression
                        ;

factor_expression       : factor_expression factor_operation terminal_expression
                        | terminal_expression
                        ;

terminal_expression     : value
	                    | L_PARANTHESIS expression R_PARANTHESIS
                        ;

sum_operation           : PLUS_OP 
                        | MINUS_OP;

factor_operation        : MULTIPLICATION_OP 
                        | DIVISION_OP
                        ;

function_invocation     : IDENTIFIER L_PARANTHESIS value_list R_PARANTHESIS
                        ;

member_access           : IDENTIFIER '.' IDENTIFIER
                        ;

value_list              : value
                        | value ',' value_list
                        | empty
                        ;

value                   : IDENTIFIER
                        | INT
                        | FLOAT
                        | array
                        | function_invocation
                        | member_access
                        ;

array                   : L_BRACKET value_list R_BRACKET
                        ;


/* Lexer rules */
IDENTIFIER              : [a-zA-Z][a-zA-Z_0-9]*;
INT                     : [0-9]+;
FLOAT                   : [0-9]+.[0-9]+;
            
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