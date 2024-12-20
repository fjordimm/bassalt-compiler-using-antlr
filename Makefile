
MAKEFLAGS += --no-print-directory

.PHONY: all clean antlr compiler out comp_out run_out

all:
	make antlr
	make compiler
	make out

antlr:
	@antlr4 -Dlanguage=CSharp -visitor ./antlr_grammar/Bassalt.g4 -o ./BassaltCompiler/src/antlr_out

compiler:
	@cd ./BassaltCompiler; dotnet run --configuration Debug; cd ..

out:
	make comp_out
	make run_out

comp_out:
	@cd ./TestIO/out; clang out.c -o out; cd ../..

run_out:
	@cd ./TestIO/out; ./out; cd ../..

clean:
	@cd ./TestIO/out; rm out.c out; cd ../..
