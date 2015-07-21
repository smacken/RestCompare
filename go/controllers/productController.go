package controllers

import (
	"github.com/gorilla/context"
	"github.com/jinzhu/gorm"
	"github.com/mholt/binding"
	"github.com/unrolled/render"
	"net/http"
	"restcompare/models"
)

func List(rw http.ResponseWriter, req *http.Request) {
	r := render.New()
	db := context.Get(req, "db")
	products := []models.Product{}
	err := db.Find(&products).Error
	if err != nil {
		panic(err)
	}
	r.JSON(rw, http.StatusOK, &products)
}

func Get(rw http.ResponseWriter, req *http.Request) {
	db := context.Get(req, "db")
	vars := mux.Vars(req)
	id := vars["id"]
	product := models.Product{}
	err := db.First(&product, id).Error
	if err != nil {
		panic(err)
	}
	r := render.New()
	r.JSON(rw, http.StatusOK, product)
}

func Post(rw http.ResponseWriter, req *http.Request) {
	product := new(Product)
	if binding.Bind(req, product).Handle(res) {
		return
	}
	db := context.Get(req, "db")
	if db.NewRecord(product) {
		db.Create(&product)
	} else {
		db.Save(&product)
	}
	r := render.New()
	r.JSON(rw, http.StatusOK, product)
}

func Put(rw http.ResponseWriter, req *http.Request) {

}

func Delete(rw http.ResponseWriter, req *http.Request) {

}
