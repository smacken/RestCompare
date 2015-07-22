package main

import (
	"github.com/codegangsta/negroni"
	"os"
	"restcompare/config"
)

func main() {
	config := config.NewConfig()
	router := NewRouter()
	app := NewApp()

	app.Use(negroni.HandlerFunc(config.ConfigContext()))
	app.UseHandler(router)
	app.Run(":" + os.Getenv("port"))
}
