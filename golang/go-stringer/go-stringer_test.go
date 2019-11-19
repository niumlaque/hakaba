package main

import (
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
	return fmt.Sprintf("mode=%x", p.mode)
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
	return fmt.Sprintf("private=%x, Public=%x", p.private, p.Public)
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
		t.Errorf("function only must occurred error")
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
	return fmt.Sprintf("private=%x, Public=%x", p.private, p.Public)
}`

	actual, err := generate(src)
	if err != nil {
		t.Error(err)
	}

	if expected != actual {
		t.Errorf("\nexpected:\n\"%s\", \nactual:\n\"%s\"", expected, actual)
	}
}
