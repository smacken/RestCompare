package config

import (
	"github.com/codegangsta/negroni"
	"github.com/gorilla/context"
	"github.com/jinzhu/gorm"
	"github.com/joho/godotenv"
	_ "github.com/lib/pq"
	"log"
	"net/http"
	"os"
	"restcompare/models"
)

type Config struct {
	DB *gorm.DB
}

type dbKey int

const DbKey dbKey = 1

func NewConfig() *Config {
	// env vars
	err := godotenv.Load()
	if err != nil {
		log.Fatal("Error loading .env file")
	}

	database := os.Getenv("DB_NAME")
	connString := os.Getenv("ConnString")

	db, err := gorm.Open("postgres", connString)

	if err != nil {
		panic(err)
	}

	db.LogMode(true)
	db.AutoMigrate(&models.Product{}, &models.Category{})

	config := &Config{
		DB: &db,
	}

	return config
}

func (c *Config) ConfigContext() negroni.HandlerFunc {
	return negroni.HandlerFunc(func(rw http.ResponseWriter, r *http.Request, next http.HandlerFunc) {
		context.Set(r, DbKey, c.DB)
		next(rw, r)
	})
}
