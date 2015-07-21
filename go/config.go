package main

import (
	"github.com/joho/godotenv"
	"github.com/gorilla/context"
	"github.com/jinzhu/gorm"
	"database/sql"
	_ "github.com/lib/pq"
)

type Config struct {
	DB *gorm.DB
}

func NewConfig() *Config {
	// env vars
	err := godotenv.Load()
	if err != nil {
		log.Fatal("Error loading .env file")
	}

	database := os.Getenv("DB_NAME")

	db, err := gorm.Open("postgres", "user=gorm dbname="+ database + "sslmode=disable")

	if err != nil {
		panic(err)
	}

	config := &Config{
		DB = db
	}

	return config
}

func (c *Config) ConfigContext() negroni.HandlerFunc { 
	return negroni.HandlerFunc(func(rw http.ResponseWriter, r *http.Request, next http.HandlerFunc) {
		context.Set(r, "db", c.DB)
		next(rw, r)
	})
}