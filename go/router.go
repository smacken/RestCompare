package main

import (
	"github.com/gorilla/mux"
	"restcompare/controllers"
)

func NewRouter() *mux.Router {
	router := mux.NewRouter().StrictSlash(false)
	router.HandleFunc("/", controllers.HomeHandler)
	router.HandleFunc("/stats", controllers.StatsHandler)

	auth := router.PathPrefix("/auth").Subrouter()
	auth.Path("/login").HandlerFunc(controllers.LoginHandler)
	auth.Path("/logout").HandlerFunc(controllers.LogoutHandler)
	auth.Path("/signup").HandlerFunc(controllers.SignupHandler)

	api := router.PathPrefix("/api").Subrouter()
	api.Path("/docs").HandlerFunc(controllers.DocsHandler)
	api.Path("/categories/{id}/products").HandlerFunc(controllers.ProductsList).Methods("GET")
	api.Path("/products").HandlerFunc(controllers.ProductsList).Methods("GET")
	api.Path("/products/{id}").HandlerFunc(controllers.ProductsGet).Methods("GET")
	api.Path("/products").HandlerFunc(controllers.ProductsPost).Methods("POST")
	api.Path("/products").HandlerFunc(controllers.ProductsPut).Methods("PUT")
	api.Path("/products/{id}").HandlerFunc(controllers.ProductsDelete).Methods("DELETE")

	return router
}
