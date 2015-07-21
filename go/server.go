package main

import (
	"os"
)

func main() {
	config := NewConfig()
	router := NewRouter()
	app := NewApp()

	app.Use(config.ConfigContext)
	app.UseHandler(router)
	app.Run(":" + os.Getenv("port"))
}
