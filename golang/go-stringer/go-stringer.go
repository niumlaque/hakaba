package main

import (
	"errors"
	"fmt"
	"go/ast"
	"go/parser"
	"go/token"
	"os"
	"strings"
)

func exitOnFail(err error) {
	if err != nil {
		fmt.Fprintf(os.Stderr, "%s\n", err.Error())
		os.Exit(1)
	}
}

func makeStringer(structName string, fields []*ast.Field, lf string) string {
	stringer := "func (p *" + structName + ") String() string {" + lf

	fmt := make([]string, 0)
	args := make([]string, 0)
	for _, x := range fields {
		if len(x.Names) > 0 {
			fmt = append(fmt, x.Names[0].Name+"=%x")
			args = append(args, "p."+x.Names[0].Name)
		}
	}

	if len(fmt) == 0 {
		stringer += "\treturn \"\""
	} else {
		stringer += "\treturn fmt.Sprintf(\"" + strings.Join(fmt, ", ") + "\", " + strings.Join(args, ", ") + ")"
	}

	return stringer + lf + "}"
}

func findFirstStructTypeSpecFromGenDecl(d *ast.GenDecl) (*ast.TypeSpec, *ast.StructType, bool) {
	for _, x := range d.Specs {
		typeSpec, ok := x.(*ast.TypeSpec)
		if !ok {
			continue
		}

		structType, ok := typeSpec.Type.(*ast.StructType)
		if ok {
			return typeSpec, structType, true
		}
	}

	return nil, nil, false
}

func findFirstStructTypeFromFileSet(f *ast.File) (*ast.TypeSpec, *ast.StructType, bool) {
	for _, x := range f.Decls {
		gendecl, ok := x.(*ast.GenDecl)

		if !ok {
			continue
		}

		typeSpec, structType, ok := findFirstStructTypeSpecFromGenDecl(gendecl)
		if ok {
			return typeSpec, structType, true
		}
	}

	return nil, nil, false
}

func generate(src string) (string, error) {
	f, err := parser.ParseFile(token.NewFileSet(), "", "package p;"+src, 0)
	if err != nil {
		return "", err
	}

	typeSpec, structType, ok := findFirstStructTypeFromFileSet(f)
	if !ok {
		return "", errors.New("Failed to parse src")
	}

	return makeStringer(typeSpec.Name.Name, structType.Fields.List, "\n"), nil
}

func main() {
	if len(os.Args) < 2 {
		exitOnFail(errors.New("Invalid argument"))
	}

	stringer, err := generate(os.Args[1])
	exitOnFail(err)
	fmt.Println(stringer)
}
