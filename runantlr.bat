@echo off
antlr4 -Dlanguage=CSharp -visitor .\antlr_grammar\Bassalt.g4 -o .\BassaltCompiler\antlr_out