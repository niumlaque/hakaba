package main

import (
	"fmt"
	"os"
	"strconv"
)

func strtol(nptr []rune, endptr *[]rune, base int) int {
	var p rune
	pp := make([]rune, 0)
	i := 0
	for ; i < len(nptr); i++ {
		p = nptr[i]
		if '0' <= p && p <= '9' {
			pp = append(pp, p)
		} else if base == 16 && (p == 'x' || p == 'X') {
			pp = append(pp, p)
		} else {
			break
		}
	}

	if endptr != nil {
		*endptr = nptr[i:]
	}

	if i > 0 {
		v, err := strconv.ParseInt(string(pp), base, 32)
		if err != nil {
			panic(err)
		}

		return int(v)
	}

	return 0
}

func main() {
	if len(os.Args) != 2 {
		fmt.Fprintf(os.Stderr, "Invalid parameter\n")
		os.Exit(1)
	}

	p := []rune(os.Args[1])
	fmt.Fprintf(os.Stdout, ".intel_syntax noprefix\n")
	fmt.Fprintf(os.Stdout, ".global main\n")
	fmt.Fprintf(os.Stdout, "main:\n")
	fmt.Fprintf(os.Stdout, "  mov rax, %d\n", strtol(p, &p, 10))

	for len(p) > 0 {
		if p[0] == '+' {
			p = p[1:]
			fmt.Fprintf(os.Stdout, "  add rax, %v\n", strtol(p, &p, 10))
			continue
		}
		if p[0] == '-' {
			p = p[1:]
			fmt.Fprintf(os.Stdout, "  sub rax, %v\n", strtol(p, &p, 10))
			continue
		}

		fmt.Fprintf(os.Stderr, "Unecpected character: '%v'", p[0])
		os.Exit(1)
	}

	fmt.Fprintf(os.Stdout, "  ret\n")
}
