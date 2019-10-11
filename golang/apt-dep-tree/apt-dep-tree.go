package main

import (
	"bytes"
	"errors"
	"fmt"
	"os"
	"os/exec"
	"strings"
)

func checkDepends(pkg string) ([]string, error) {
	cmd := exec.Command("apt-cache", "depends", pkg)
	var stdout bytes.Buffer
	var stderr bytes.Buffer
	cmd.Stdout = &stdout
	cmd.Stderr = &stderr
	err := cmd.Run()
	if err != nil {
		return nil, errors.New(stderr.String())
	}

	result := make([]string, 0)
	splited := strings.Split(stdout.String(), "\n")
	key := "Depends: "
	for _, x := range splited {
		index := strings.Index(x, key)
		if index < 0 {
			continue
		}

		depPkg := x[index+len(key):]
		result = append(result, depPkg)
	}

	return result, nil
}

func printTree(level int, pkg string, ancestors []string) {
	newAncestors := append(ancestors, pkg)
	indent := strings.Repeat("  ", level)
	fmt.Fprintf(os.Stdout, "%s[%s]\n", indent, pkg)
	depPkgs, err := checkDepends(pkg)
	if err != nil {
		fmt.Fprintf(os.Stderr, "%s\n", err)
		os.Exit(1)
	}

	for _, x := range depPkgs {
		skip := false
		for _, y := range newAncestors {
			if x == y {
				skip = true
				break
			}
		}

		if !skip {
			printTree(level+1, x, newAncestors)
		}
	}
}

func main() {
	if len(os.Args) <= 1 {
		fmt.Fprintf(os.Stderr, "You must give at least one search pattern\n")
		os.Exit(1)
	}

	pkgs := os.Args[1:]
	for _, x := range pkgs {
		ancestors := make([]string, 0)
		printTree(0, x, ancestors)
	}
}
