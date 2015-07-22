package controllers

import (
	"fmt"
	"github.com/gorilla/context"
	"github.com/gorilla/mux"
	"github.com/jinzhu/gorm"
	"github.com/mholt/binding"
	"github.com/unrolled/render"
	"net/http"
	"restcompare/config"
	"restcompare/models"
)

func GetDb(req *http.Request) *gorm.DB {
	db := context.Get(req, config.DbKey)

	gorm := db.(*gorm.DB)

	return gorm
}

func ProductsList(rw http.ResponseWriter, req *http.Request) {
	r := render.New()
	db := GetDb(req)
	vars := mux.Vars(req)
	id := vars["id"]
	products := []models.Product{}
	if id != "" {
		if err := db.Where("CategoryId = ?", id).Find(&products).Error; err != nil {
			fmt.Println("Bailing out")
			panic(err)
		}
	} else {
		err := db.Find(&products).Error
		if err != nil {
			fmt.Println("Bailing out")
			panic(err)
		}
	}

	r.JSON(rw, http.StatusOK, &products)
}

func ProductsGet(rw http.ResponseWriter, req *http.Request) {
	db := GetDb(req)
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

func ProductsPost(rw http.ResponseWriter, req *http.Request) {
	product := new(models.Product)
	if binding.Bind(req, product).Handle(rw) {
		return
	}
	db := GetDb(req)
	if db.NewRecord(product) {
		db.Create(&product)
	} else {
		db.Save(&product)
	}
	r := render.New()
	r.JSON(rw, http.StatusOK, product)
}

func ProductsPut(rw http.ResponseWriter, req *http.Request) {

}

func ProductsDelete(rw http.ResponseWriter, req *http.Request) {

}
