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
func GetContext(req *http.Request) *config.Context {
	ctx := context.Get(req, config.ContextKey)
	conf := ctx.(*config.Context)
	return conf
}

func ProductsList(rw http.ResponseWriter, req *http.Request) {
	r := render.New()
	db := GetDb(req)
	vars := mux.Vars(req)
	id := vars["id"]
	products := []models.Product{}
	if id != "" {
		if err := db.Where("CategoryId = ?", id).Find(&products).Error; err != nil {
			http.Error(rw, err.Error(), http.StatusInternalServerError)
		}
	} else {
		if err := db.Find(&products).Error; err != nil {
			http.Error(rw, err.Error(), http.StatusInternalServerError)
		}
	}

	r.JSON(rw, http.StatusOK, &products)
}

func ProductsGet(rw http.ResponseWriter, req *http.Request) {
	db := GetDb(req)
	vars := mux.Vars(req)
	id := vars["id"]
	product := models.Product{}
	if err := db.First(&product, id).Error; err != nil {
		http.Error(rw, err.Error(), http.StatusInternalServerError)
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
	product := new(models.Product)
	if binding.Bind(req, product).Handle(rw) {
		return
	}
	db := GetDb(req)
	db.Save(&product)
	r := render.New()
	r.JSON(rw, http.StatusOK, product)
}

func ProductsDelete(rw http.ResponseWriter, req *http.Request) {
	vars := mux.Vars(req)
	id := vars["id"]
	db := GetDb(req)
	db.Delete(id)
}
