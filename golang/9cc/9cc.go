package main

import (
	"errors"
	"fmt"
	"os"
	"strconv"
)

type TokenKind int

const (
	Reserved TokenKind = iota
	Num
	EOF
)

type Token struct {
	kind TokenKind
	next *Token
	val  int
	str  []rune
}

func (tk *Token) String() string {
	var nextRune rune
	if len(tk.str) > 0 {
		nextRune = tk.str[0]
	}

	return fmt.Sprintf("Kind=%v, NextRune=%v, Val=%v", tk.kind, string(nextRune), tk.val)
}

func NewToken(kind TokenKind, currentToken *Token, str []rune) *Token {
	v := &Token{}
	v.kind = kind
	v.str = str
	currentToken.next = v
	return v
}

var token *Token

func consume(op rune) bool {
	if token.kind != Reserved || token.str[0] != op {
		return false
	}

	token = token.next
	return true
}

func expect(op rune) {
	if token.kind != Reserved || token.str[0] != op {
		panic(fmt.Errorf("Unexpected operator '%v'", op))
	}

	token = token.next
}

func expectNumber() int {
	if token.kind != Num {
		panic(errors.New("Not a anumber"))
	}

	v := token.val
	token = token.next
	return v
}

func atEOF() bool {
	return token.kind == EOF
}

func tokenize(p []rune) *Token {
	var head Token
	head.next = nil
	currentToken := &head

	for len(p) > 0 {
		if p[0] == ' ' {
			p = p[1:]
			continue
		}

		if p[0] == '+' || p[0] == '-' {
			currentToken = NewToken(Reserved, currentToken, p)
			p = p[1:]
			continue
		}

		if '0' <= p[0] && p[0] <= '9' {
			currentToken = NewToken(Num, currentToken, p)
			currentToken.val = strtol(p, &p, 10)
			continue
		}

		panic(fmt.Errorf("Failed to tokenize text: '%v'", p[0]))
	}

	NewToken(EOF, currentToken, p)
	return head.next
}

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

func dumpToken(v *Token) {
	cur := v
	for i := 0; cur != nil; i++ {
		fmt.Fprintf(os.Stderr, "[%v] %s\n", i+1, cur)
		cur = cur.next
	}
}

func main() {
	if len(os.Args) != 2 {
		fmt.Fprintf(os.Stderr, "Invalid parameter\n")
		os.Exit(1)
	}

	token = tokenize([]rune(os.Args[1]))
	fmt.Fprintf(os.Stdout, ".intel_syntax noprefix\n")
	fmt.Fprintf(os.Stdout, ".global main\n")
	fmt.Fprintf(os.Stdout, "main:\n")
	fmt.Fprintf(os.Stdout, "  mov rax, %v\n", expectNumber())

	for !atEOF() {
		if consume('+') {
			fmt.Fprintf(os.Stdout, "  add rax, %v\n", expectNumber())
			continue
		}

		if consume('-') {
			fmt.Fprintf(os.Stdout, "  sub rax, %v\n", expectNumber())
			continue
		}

		panic(fmt.Errorf("Unexpected character: '%s'", token))
	}

	fmt.Fprintf(os.Stdout, "  ret\n")
}
