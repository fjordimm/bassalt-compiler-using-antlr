
.PHONY: all clean run_antlr run_compiler out_comp out

all:
	make run_antlr
	make run_compiler
	make out_comp

run_antlr:
	@antlr4 -Dlanguage=CSharp -visitor ./antlr_grammar/Bassalt.g4 -o ./BassaltCompiler/antlr_out

run_compiler:
	@cd ./BassaltCompiler; dotnet run; cd ..

out_comp:
	@cd ./TestIO/out; clang out.c -o out; cd ../..

out:
	@cd ./TestIO/out; ./out; cd ../..

clean:
	@cd ./TestIO/out; rm out.c out; cd ../..
