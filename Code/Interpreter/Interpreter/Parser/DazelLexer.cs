//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.9.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Dazel.g4 by ANTLR 4.9.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.9.2")]
[System.CLSCompliant(false)]
public partial class DazelLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, WS=15, IDENTIFIER=16, 
		INT=17, FLOAT=18, L_PARANTHESIS=19, R_PARANTHESIS=20, L_BRACKET=21, R_BRACKET=22, 
		L_BRACES=23, R_BRACES=24, ASSIGN_OP=25, LESSTHAN_OP=26, GREATERTHAN_OP=27, 
		PLUS_OP=28, MINUS_OP=29, MULTIPLICATION_OP=30, DIVISION_OP=31, QUOTATION_MARK=32;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "WS", "IDENTIFIER", "INT", 
		"FLOAT", "L_PARANTHESIS", "R_PARANTHESIS", "L_BRACKET", "R_BRACKET", "L_BRACES", 
		"R_BRACES", "ASSIGN_OP", "LESSTHAN_OP", "GREATERTHAN_OP", "PLUS_OP", "MINUS_OP", 
		"MULTIPLICATION_OP", "DIVISION_OP", "QUOTATION_MARK"
	};


	public DazelLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public DazelLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'Screen '", "'Entity '", "'MovePattern'", "'Map'", "'OnScreenEntered'", 
		"'Entities'", "'Exits'", "'Data'", "'Pattern'", "';'", "'repeat'", "'if'", 
		"'.'", "','", null, null, null, null, "'('", "')'", "'['", "']'", "'{'", 
		"'}'", "'='", "'<'", "'>'", "'+'", "'-'", "'*'", "'/'", "'\"'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, "WS", "IDENTIFIER", "INT", "FLOAT", "L_PARANTHESIS", 
		"R_PARANTHESIS", "L_BRACKET", "R_BRACKET", "L_BRACES", "R_BRACES", "ASSIGN_OP", 
		"LESSTHAN_OP", "GREATERTHAN_OP", "PLUS_OP", "MINUS_OP", "MULTIPLICATION_OP", 
		"DIVISION_OP", "QUOTATION_MARK"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "Dazel.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static DazelLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x2', '\"', '\xD9', '\b', '\x1', '\x4', '\x2', '\t', '\x2', 
		'\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', 
		'\x5', '\x4', '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', 
		'\t', '\b', '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', 
		'\t', '\v', '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', '\xE', 
		'\t', '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', '\x4', 
		'\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x4', '\x13', '\t', 
		'\x13', '\x4', '\x14', '\t', '\x14', '\x4', '\x15', '\t', '\x15', '\x4', 
		'\x16', '\t', '\x16', '\x4', '\x17', '\t', '\x17', '\x4', '\x18', '\t', 
		'\x18', '\x4', '\x19', '\t', '\x19', '\x4', '\x1A', '\t', '\x1A', '\x4', 
		'\x1B', '\t', '\x1B', '\x4', '\x1C', '\t', '\x1C', '\x4', '\x1D', '\t', 
		'\x1D', '\x4', '\x1E', '\t', '\x1E', '\x4', '\x1F', '\t', '\x1F', '\x4', 
		' ', '\t', ' ', '\x4', '!', '\t', '!', '\x3', '\x2', '\x3', '\x2', '\x3', 
		'\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', 
		'\x2', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x4', '\x3', 
		'\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', 
		'\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', 
		'\x4', '\x3', '\x5', '\x3', '\x5', '\x3', '\x5', '\x3', '\x5', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', 
		'\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\b', '\x3', 
		'\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\t', 
		'\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\n', '\x3', 
		'\n', '\x3', '\n', '\x3', '\n', '\x3', '\n', '\x3', '\n', '\x3', '\n', 
		'\x3', '\n', '\x3', '\v', '\x3', '\v', '\x3', '\f', '\x3', '\f', '\x3', 
		'\f', '\x3', '\f', '\x3', '\f', '\x3', '\f', '\x3', '\f', '\x3', '\r', 
		'\x3', '\r', '\x3', '\r', '\x3', '\xE', '\x3', '\xE', '\x3', '\xF', '\x3', 
		'\xF', '\x3', '\x10', '\x6', '\x10', '\xA1', '\n', '\x10', '\r', '\x10', 
		'\xE', '\x10', '\xA2', '\x3', '\x10', '\x3', '\x10', '\x3', '\x11', '\x3', 
		'\x11', '\a', '\x11', '\xA9', '\n', '\x11', '\f', '\x11', '\xE', '\x11', 
		'\xAC', '\v', '\x11', '\x3', '\x12', '\x6', '\x12', '\xAF', '\n', '\x12', 
		'\r', '\x12', '\xE', '\x12', '\xB0', '\x3', '\x13', '\x6', '\x13', '\xB4', 
		'\n', '\x13', '\r', '\x13', '\xE', '\x13', '\xB5', '\x3', '\x13', '\x3', 
		'\x13', '\x6', '\x13', '\xBA', '\n', '\x13', '\r', '\x13', '\xE', '\x13', 
		'\xBB', '\x3', '\x14', '\x3', '\x14', '\x3', '\x15', '\x3', '\x15', '\x3', 
		'\x16', '\x3', '\x16', '\x3', '\x17', '\x3', '\x17', '\x3', '\x18', '\x3', 
		'\x18', '\x3', '\x19', '\x3', '\x19', '\x3', '\x1A', '\x3', '\x1A', '\x3', 
		'\x1B', '\x3', '\x1B', '\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1D', '\x3', 
		'\x1D', '\x3', '\x1E', '\x3', '\x1E', '\x3', '\x1F', '\x3', '\x1F', '\x3', 
		' ', '\x3', ' ', '\x3', '!', '\x3', '!', '\x2', '\x2', '\"', '\x3', '\x3', 
		'\x5', '\x4', '\a', '\x5', '\t', '\x6', '\v', '\a', '\r', '\b', '\xF', 
		'\t', '\x11', '\n', '\x13', '\v', '\x15', '\f', '\x17', '\r', '\x19', 
		'\xE', '\x1B', '\xF', '\x1D', '\x10', '\x1F', '\x11', '!', '\x12', '#', 
		'\x13', '%', '\x14', '\'', '\x15', ')', '\x16', '+', '\x17', '-', '\x18', 
		'/', '\x19', '\x31', '\x1A', '\x33', '\x1B', '\x35', '\x1C', '\x37', '\x1D', 
		'\x39', '\x1E', ';', '\x1F', '=', ' ', '?', '!', '\x41', '\"', '\x3', 
		'\x2', '\x6', '\x5', '\x2', '\v', '\f', '\xF', '\xF', '\"', '\"', '\x4', 
		'\x2', '\x43', '\\', '\x63', '|', '\x6', '\x2', '\x32', ';', '\x43', '\\', 
		'\x61', '\x61', '\x63', '|', '\x3', '\x2', '\x32', ';', '\x2', '\xDD', 
		'\x2', '\x3', '\x3', '\x2', '\x2', '\x2', '\x2', '\x5', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\a', '\x3', '\x2', '\x2', '\x2', '\x2', '\t', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\v', '\x3', '\x2', '\x2', '\x2', '\x2', '\r', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\xF', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x11', '\x3', '\x2', '\x2', '\x2', '\x2', '\x13', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x15', '\x3', '\x2', '\x2', '\x2', '\x2', '\x17', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x19', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x1B', '\x3', '\x2', '\x2', '\x2', '\x2', '\x1D', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x1F', '\x3', '\x2', '\x2', '\x2', '\x2', '!', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '#', '\x3', '\x2', '\x2', '\x2', '\x2', '%', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\'', '\x3', '\x2', '\x2', '\x2', '\x2', 
		')', '\x3', '\x2', '\x2', '\x2', '\x2', '+', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '-', '\x3', '\x2', '\x2', '\x2', '\x2', '/', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x31', '\x3', '\x2', '\x2', '\x2', '\x2', '\x33', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x35', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x37', '\x3', '\x2', '\x2', '\x2', '\x2', '\x39', '\x3', '\x2', '\x2', 
		'\x2', '\x2', ';', '\x3', '\x2', '\x2', '\x2', '\x2', '=', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '?', '\x3', '\x2', '\x2', '\x2', '\x2', '\x41', '\x3', 
		'\x2', '\x2', '\x2', '\x3', '\x43', '\x3', '\x2', '\x2', '\x2', '\x5', 
		'K', '\x3', '\x2', '\x2', '\x2', '\a', 'S', '\x3', '\x2', '\x2', '\x2', 
		'\t', '_', '\x3', '\x2', '\x2', '\x2', '\v', '\x63', '\x3', '\x2', '\x2', 
		'\x2', '\r', 's', '\x3', '\x2', '\x2', '\x2', '\xF', '|', '\x3', '\x2', 
		'\x2', '\x2', '\x11', '\x82', '\x3', '\x2', '\x2', '\x2', '\x13', '\x87', 
		'\x3', '\x2', '\x2', '\x2', '\x15', '\x8F', '\x3', '\x2', '\x2', '\x2', 
		'\x17', '\x91', '\x3', '\x2', '\x2', '\x2', '\x19', '\x98', '\x3', '\x2', 
		'\x2', '\x2', '\x1B', '\x9B', '\x3', '\x2', '\x2', '\x2', '\x1D', '\x9D', 
		'\x3', '\x2', '\x2', '\x2', '\x1F', '\xA0', '\x3', '\x2', '\x2', '\x2', 
		'!', '\xA6', '\x3', '\x2', '\x2', '\x2', '#', '\xAE', '\x3', '\x2', '\x2', 
		'\x2', '%', '\xB3', '\x3', '\x2', '\x2', '\x2', '\'', '\xBD', '\x3', '\x2', 
		'\x2', '\x2', ')', '\xBF', '\x3', '\x2', '\x2', '\x2', '+', '\xC1', '\x3', 
		'\x2', '\x2', '\x2', '-', '\xC3', '\x3', '\x2', '\x2', '\x2', '/', '\xC5', 
		'\x3', '\x2', '\x2', '\x2', '\x31', '\xC7', '\x3', '\x2', '\x2', '\x2', 
		'\x33', '\xC9', '\x3', '\x2', '\x2', '\x2', '\x35', '\xCB', '\x3', '\x2', 
		'\x2', '\x2', '\x37', '\xCD', '\x3', '\x2', '\x2', '\x2', '\x39', '\xCF', 
		'\x3', '\x2', '\x2', '\x2', ';', '\xD1', '\x3', '\x2', '\x2', '\x2', '=', 
		'\xD3', '\x3', '\x2', '\x2', '\x2', '?', '\xD5', '\x3', '\x2', '\x2', 
		'\x2', '\x41', '\xD7', '\x3', '\x2', '\x2', '\x2', '\x43', '\x44', '\a', 
		'U', '\x2', '\x2', '\x44', '\x45', '\a', '\x65', '\x2', '\x2', '\x45', 
		'\x46', '\a', 't', '\x2', '\x2', '\x46', 'G', '\a', 'g', '\x2', '\x2', 
		'G', 'H', '\a', 'g', '\x2', '\x2', 'H', 'I', '\a', 'p', '\x2', '\x2', 
		'I', 'J', '\a', '\"', '\x2', '\x2', 'J', '\x4', '\x3', '\x2', '\x2', '\x2', 
		'K', 'L', '\a', 'G', '\x2', '\x2', 'L', 'M', '\a', 'p', '\x2', '\x2', 
		'M', 'N', '\a', 'v', '\x2', '\x2', 'N', 'O', '\a', 'k', '\x2', '\x2', 
		'O', 'P', '\a', 'v', '\x2', '\x2', 'P', 'Q', '\a', '{', '\x2', '\x2', 
		'Q', 'R', '\a', '\"', '\x2', '\x2', 'R', '\x6', '\x3', '\x2', '\x2', '\x2', 
		'S', 'T', '\a', 'O', '\x2', '\x2', 'T', 'U', '\a', 'q', '\x2', '\x2', 
		'U', 'V', '\a', 'x', '\x2', '\x2', 'V', 'W', '\a', 'g', '\x2', '\x2', 
		'W', 'X', '\a', 'R', '\x2', '\x2', 'X', 'Y', '\a', '\x63', '\x2', '\x2', 
		'Y', 'Z', '\a', 'v', '\x2', '\x2', 'Z', '[', '\a', 'v', '\x2', '\x2', 
		'[', '\\', '\a', 'g', '\x2', '\x2', '\\', ']', '\a', 't', '\x2', '\x2', 
		']', '^', '\a', 'p', '\x2', '\x2', '^', '\b', '\x3', '\x2', '\x2', '\x2', 
		'_', '`', '\a', 'O', '\x2', '\x2', '`', '\x61', '\a', '\x63', '\x2', '\x2', 
		'\x61', '\x62', '\a', 'r', '\x2', '\x2', '\x62', '\n', '\x3', '\x2', '\x2', 
		'\x2', '\x63', '\x64', '\a', 'Q', '\x2', '\x2', '\x64', '\x65', '\a', 
		'p', '\x2', '\x2', '\x65', '\x66', '\a', 'U', '\x2', '\x2', '\x66', 'g', 
		'\a', '\x65', '\x2', '\x2', 'g', 'h', '\a', 't', '\x2', '\x2', 'h', 'i', 
		'\a', 'g', '\x2', '\x2', 'i', 'j', '\a', 'g', '\x2', '\x2', 'j', 'k', 
		'\a', 'p', '\x2', '\x2', 'k', 'l', '\a', 'G', '\x2', '\x2', 'l', 'm', 
		'\a', 'p', '\x2', '\x2', 'm', 'n', '\a', 'v', '\x2', '\x2', 'n', 'o', 
		'\a', 'g', '\x2', '\x2', 'o', 'p', '\a', 't', '\x2', '\x2', 'p', 'q', 
		'\a', 'g', '\x2', '\x2', 'q', 'r', '\a', '\x66', '\x2', '\x2', 'r', '\f', 
		'\x3', '\x2', '\x2', '\x2', 's', 't', '\a', 'G', '\x2', '\x2', 't', 'u', 
		'\a', 'p', '\x2', '\x2', 'u', 'v', '\a', 'v', '\x2', '\x2', 'v', 'w', 
		'\a', 'k', '\x2', '\x2', 'w', 'x', '\a', 'v', '\x2', '\x2', 'x', 'y', 
		'\a', 'k', '\x2', '\x2', 'y', 'z', '\a', 'g', '\x2', '\x2', 'z', '{', 
		'\a', 'u', '\x2', '\x2', '{', '\xE', '\x3', '\x2', '\x2', '\x2', '|', 
		'}', '\a', 'G', '\x2', '\x2', '}', '~', '\a', 'z', '\x2', '\x2', '~', 
		'\x7F', '\a', 'k', '\x2', '\x2', '\x7F', '\x80', '\a', 'v', '\x2', '\x2', 
		'\x80', '\x81', '\a', 'u', '\x2', '\x2', '\x81', '\x10', '\x3', '\x2', 
		'\x2', '\x2', '\x82', '\x83', '\a', '\x46', '\x2', '\x2', '\x83', '\x84', 
		'\a', '\x63', '\x2', '\x2', '\x84', '\x85', '\a', 'v', '\x2', '\x2', '\x85', 
		'\x86', '\a', '\x63', '\x2', '\x2', '\x86', '\x12', '\x3', '\x2', '\x2', 
		'\x2', '\x87', '\x88', '\a', 'R', '\x2', '\x2', '\x88', '\x89', '\a', 
		'\x63', '\x2', '\x2', '\x89', '\x8A', '\a', 'v', '\x2', '\x2', '\x8A', 
		'\x8B', '\a', 'v', '\x2', '\x2', '\x8B', '\x8C', '\a', 'g', '\x2', '\x2', 
		'\x8C', '\x8D', '\a', 't', '\x2', '\x2', '\x8D', '\x8E', '\a', 'p', '\x2', 
		'\x2', '\x8E', '\x14', '\x3', '\x2', '\x2', '\x2', '\x8F', '\x90', '\a', 
		'=', '\x2', '\x2', '\x90', '\x16', '\x3', '\x2', '\x2', '\x2', '\x91', 
		'\x92', '\a', 't', '\x2', '\x2', '\x92', '\x93', '\a', 'g', '\x2', '\x2', 
		'\x93', '\x94', '\a', 'r', '\x2', '\x2', '\x94', '\x95', '\a', 'g', '\x2', 
		'\x2', '\x95', '\x96', '\a', '\x63', '\x2', '\x2', '\x96', '\x97', '\a', 
		'v', '\x2', '\x2', '\x97', '\x18', '\x3', '\x2', '\x2', '\x2', '\x98', 
		'\x99', '\a', 'k', '\x2', '\x2', '\x99', '\x9A', '\a', 'h', '\x2', '\x2', 
		'\x9A', '\x1A', '\x3', '\x2', '\x2', '\x2', '\x9B', '\x9C', '\a', '\x30', 
		'\x2', '\x2', '\x9C', '\x1C', '\x3', '\x2', '\x2', '\x2', '\x9D', '\x9E', 
		'\a', '.', '\x2', '\x2', '\x9E', '\x1E', '\x3', '\x2', '\x2', '\x2', '\x9F', 
		'\xA1', '\t', '\x2', '\x2', '\x2', '\xA0', '\x9F', '\x3', '\x2', '\x2', 
		'\x2', '\xA1', '\xA2', '\x3', '\x2', '\x2', '\x2', '\xA2', '\xA0', '\x3', 
		'\x2', '\x2', '\x2', '\xA2', '\xA3', '\x3', '\x2', '\x2', '\x2', '\xA3', 
		'\xA4', '\x3', '\x2', '\x2', '\x2', '\xA4', '\xA5', '\b', '\x10', '\x2', 
		'\x2', '\xA5', ' ', '\x3', '\x2', '\x2', '\x2', '\xA6', '\xAA', '\t', 
		'\x3', '\x2', '\x2', '\xA7', '\xA9', '\t', '\x4', '\x2', '\x2', '\xA8', 
		'\xA7', '\x3', '\x2', '\x2', '\x2', '\xA9', '\xAC', '\x3', '\x2', '\x2', 
		'\x2', '\xAA', '\xA8', '\x3', '\x2', '\x2', '\x2', '\xAA', '\xAB', '\x3', 
		'\x2', '\x2', '\x2', '\xAB', '\"', '\x3', '\x2', '\x2', '\x2', '\xAC', 
		'\xAA', '\x3', '\x2', '\x2', '\x2', '\xAD', '\xAF', '\t', '\x5', '\x2', 
		'\x2', '\xAE', '\xAD', '\x3', '\x2', '\x2', '\x2', '\xAF', '\xB0', '\x3', 
		'\x2', '\x2', '\x2', '\xB0', '\xAE', '\x3', '\x2', '\x2', '\x2', '\xB0', 
		'\xB1', '\x3', '\x2', '\x2', '\x2', '\xB1', '$', '\x3', '\x2', '\x2', 
		'\x2', '\xB2', '\xB4', '\t', '\x5', '\x2', '\x2', '\xB3', '\xB2', '\x3', 
		'\x2', '\x2', '\x2', '\xB4', '\xB5', '\x3', '\x2', '\x2', '\x2', '\xB5', 
		'\xB3', '\x3', '\x2', '\x2', '\x2', '\xB5', '\xB6', '\x3', '\x2', '\x2', 
		'\x2', '\xB6', '\xB7', '\x3', '\x2', '\x2', '\x2', '\xB7', '\xB9', '\v', 
		'\x2', '\x2', '\x2', '\xB8', '\xBA', '\t', '\x5', '\x2', '\x2', '\xB9', 
		'\xB8', '\x3', '\x2', '\x2', '\x2', '\xBA', '\xBB', '\x3', '\x2', '\x2', 
		'\x2', '\xBB', '\xB9', '\x3', '\x2', '\x2', '\x2', '\xBB', '\xBC', '\x3', 
		'\x2', '\x2', '\x2', '\xBC', '&', '\x3', '\x2', '\x2', '\x2', '\xBD', 
		'\xBE', '\a', '*', '\x2', '\x2', '\xBE', '(', '\x3', '\x2', '\x2', '\x2', 
		'\xBF', '\xC0', '\a', '+', '\x2', '\x2', '\xC0', '*', '\x3', '\x2', '\x2', 
		'\x2', '\xC1', '\xC2', '\a', ']', '\x2', '\x2', '\xC2', ',', '\x3', '\x2', 
		'\x2', '\x2', '\xC3', '\xC4', '\a', '_', '\x2', '\x2', '\xC4', '.', '\x3', 
		'\x2', '\x2', '\x2', '\xC5', '\xC6', '\a', '}', '\x2', '\x2', '\xC6', 
		'\x30', '\x3', '\x2', '\x2', '\x2', '\xC7', '\xC8', '\a', '\x7F', '\x2', 
		'\x2', '\xC8', '\x32', '\x3', '\x2', '\x2', '\x2', '\xC9', '\xCA', '\a', 
		'?', '\x2', '\x2', '\xCA', '\x34', '\x3', '\x2', '\x2', '\x2', '\xCB', 
		'\xCC', '\a', '>', '\x2', '\x2', '\xCC', '\x36', '\x3', '\x2', '\x2', 
		'\x2', '\xCD', '\xCE', '\a', '@', '\x2', '\x2', '\xCE', '\x38', '\x3', 
		'\x2', '\x2', '\x2', '\xCF', '\xD0', '\a', '-', '\x2', '\x2', '\xD0', 
		':', '\x3', '\x2', '\x2', '\x2', '\xD1', '\xD2', '\a', '/', '\x2', '\x2', 
		'\xD2', '<', '\x3', '\x2', '\x2', '\x2', '\xD3', '\xD4', '\a', ',', '\x2', 
		'\x2', '\xD4', '>', '\x3', '\x2', '\x2', '\x2', '\xD5', '\xD6', '\a', 
		'\x31', '\x2', '\x2', '\xD6', '@', '\x3', '\x2', '\x2', '\x2', '\xD7', 
		'\xD8', '\a', '$', '\x2', '\x2', '\xD8', '\x42', '\x3', '\x2', '\x2', 
		'\x2', '\b', '\x2', '\xA2', '\xAA', '\xB0', '\xB5', '\xBB', '\x3', '\b', 
		'\x2', '\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}