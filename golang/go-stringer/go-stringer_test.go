package main

import (
	"errors"
	"testing"
)

func TestEmptyStructOnly(t *testing.T) {
	src := `type Foo struct {}`
	expected := `func (p *Foo) String() string {
	return ""
}`

	actual, err := generate(src)
	if err != nil {
		t.Error(err)
	}

	if expected != actual {
		t.Errorf("\nexpected:\n\"%s\", \nactual:\n\"%s\"", expected, actual)
	}
}

func TestOneMemberStructOnly(t *testing.T) {
	src := `
type Foo struct {
	mode int
}`
	expected := `func (p *Foo) String() string {
	return fmt.Sprintf("mode=%v", p.mode)
}`

	actual, err := generate(src)
	if err != nil {
		t.Error(err)
	}

	if expected != actual {
		t.Errorf("\nexpected:\n\"%s\", \nactual:\n\"%s\"", expected, actual)
	}
}

func TestTwoMemberStructOnly(t *testing.T) {
	src := `
type Foo struct {
	private int
	Public string
}`
	expected := `func (p *Foo) String() string {
	return fmt.Sprintf("private=%v, Public=%v", p.private, p.Public)
}`

	actual, err := generate(src)
	if err != nil {
		t.Error(err)
	}

	if expected != actual {
		t.Errorf("\nexpected:\n\"%s\", \nactual:\n\"%s\"", expected, actual)
	}
}

func TestFunctionOnly(t *testing.T) {
	src := `
func f() {}
`
	_, err := generate(src)
	if err == nil {
		t.Errorf("function only must be error occurred")
	}
}

func TestStructAfterFunction(t *testing.T) {
	src := `
func f() {
}

type Foo struct {
	private int
	Public string
}`
	expected := `func (p *Foo) String() string {
	return fmt.Sprintf("private=%v, Public=%v", p.private, p.Public)
}`

	actual, err := generate(src)
	if err != nil {
		t.Error(err)
	}

	if expected != actual {
		t.Errorf("\nexpected:\n\"%s\", \nactual:\n\"%s\"", expected, actual)
	}
}

func TestOtherLangCode(t *testing.T) {
	_, err := generate("int f() {}")
	if err == nil {
		t.Error("other language code must be error occurred")
	}
}

func TestExitOnFail(t *testing.T) {
	exited := false
	exit = func(n int) {
		exited = true
	}

	exited = false
	exitOnFail(errors.New(""))
	if !exited {
		t.Error("\nexpected: true, actual: false")
	}

	exited = false
	exitOnFail(nil)
	if exited {
		t.Error("\nexpected: false, actual: true")
	}
}
