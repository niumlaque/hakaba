package main

import (
	"errors"
	"fmt"
	"io/ioutil"
	"os"
	"strings"

	"github.com/adrg/xdg"
	"github.com/nlopes/slack"
	"gopkg.in/yaml.v2"
)

type Config struct {
	Token string
	Name  string
}

func exitOnFail(err error) {
	if err != nil {
		fmt.Fprintf(os.Stderr, "%s\n", err.Error())
		os.Exit(1)
	}
}

func loadConfig() (*Config, error) {
	configFilePath, err := xdg.SearchConfigFile("echo-slack/config.yaml")
	if err != nil {
		fmt.Fprintf(os.Stderr, "Config file:\n```yaml\ntoken: <slack-token>\nname: <user-name-to-notify>\n```\n")
		return nil, err
	}

	buf, err := ioutil.ReadFile(configFilePath)
	if err != nil {
		return nil, err
	}

	var config Config
	err = yaml.Unmarshal(buf, &config)
	if err != nil {
		return nil, err
	}

	return &config, nil
}

func main() {
	config, err := loadConfig()
	exitOnFail(err)

	api := slack.New(config.Token)
	users, err := api.GetUsers()
	exitOnFail(err)

	var userId string
	for _, x := range users {
		if x.Name == config.Name {
			userId = x.ID
			break
		}
	}

	if len(userId) == 0 {
		exitOnFail(errors.New("User ID Not Found"))
	}

	_, _, channelId, err := api.OpenIMChannel(userId)
	exitOnFail(err)

	text := "echo"
	if len(os.Args) > 1 {
		text = strings.Join(os.Args[1:], " ")
	}

	api.PostMessage(channelId, slack.MsgOptionText(text, false))
}
