package config

import (
	"github.com/codegangsta/negroni"
	"github.com/gorilla/context"
	"github.com/jinzhu/gorm"
	"github.com/joho/godotenv"
	"github.com/unrolled/render"
	_ "github.com/lib/pq"
	"log"
	"net/http"
	"os"
	"restcompare/models"
)

type Config struct {
	DB *gorm.DB
	View *render.Render
}

type dbKey int
type contextKey int
const DbKey dbKey = 1
const ContextKey contextKey = 2

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

	r := render.New(render.Options{})
	config := &Config{
		DB: &db,
		View: &r
	}

	return config
}

func (c *Config) ConfigContext() negroni.HandlerFunc {
	return negroni.HandlerFunc(func(rw http.ResponseWriter, r *http.Request, next http.HandlerFunc) {
		context.Set(r, DbKey, c.DB)
		context.Set(r, ContextKey, c)
		next(rw, r)
	})
}
