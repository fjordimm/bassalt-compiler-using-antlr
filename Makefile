
.PHONY: all clean antlr compiler out run

all:
	make antlr
	make compiler
	make out

antlr:
	@antlr4 -Dlanguage=CSharp -visitor ./antlr_grammar/Bassalt.g4 -o ./BassaltCompiler/antlr_out

compiler:
	@cd ./BassaltCompiler; dotnet run; cd ..

out:
	@cd ./TestIO/out; clang out.c -o out; cd ../..

run:
	@cd ./TestIO/out; ./out; cd ../..

clean:
	@cd ./TestIO/out; rm out.c out; cd ../..
