grammar Dazel; // Defines grammar

WS  :   [ \t\r\n]+ -> skip;

/* PARSER RULES */
start: game_object;

game_object             : game_object_type IDENTIFIER L_BRACES game_object_contents R_BRACES
                        ;

empty                   : 
                        ;

game_object_type        : 'Screen ' | 'Entity ' | 'MovePattern'
                        ;

game_object_contents    : game_object_content
                        | game_object_content game_object_contents
                        | empty
                        ;

game_object_content     : content_type L_BRACES statement_list R_BRACES
                        ;

content_type            : 'Map'
                        | 'OnScreenEntered'
                        | 'Entities'
                        | 'Exits'
                        | 'Data'
                        | 'Pattern'
                        ;

statement_list          : statement ';'
                        | statement ';' statement_list
                        | empty
                        ;

statement               : repeat_loop
                        | assignment
                        | function_invocation
                        | if_statement
                        | game_object_creation
                        ;

repeat_loop             : 'repeat' L_BRACES statement_list R_BRACES
                        ;

if_statement            : 'if' expression L_BRACES statement_list R_BRACES
                        ;

assignment              : IDENTIFIER ASSIGN_OP expression
                        | IDENTIFIER ASSIGN_OP game_object_creation
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

game_object_creation    : 'Create' function_invocation
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