package main

import (
	"github.com/codegangsta/negroni"
	"gopkg.in/tylerb/graceful.v1"
	"os"
	"restcompare/config"
)

func main() {
	config := config.NewConfig()
	router := NewRouter()
	app := NewApp()

	app.Use(negroni.HandlerFunc(config.ConfigContext()))
	app.UseHandler(router)
	graceful.Run(":"+os.Getenv("port"), 10*time.Second, app)
}
