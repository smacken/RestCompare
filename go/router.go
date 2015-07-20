package main

import (
	"github.com/gorilla/mux"
	"goweb/controllers"
)

func NewRouter() *mux.Router {
	router := mux.NewRouter().StrictSlash(false)
	router.HandleFunc("/", controllers.HomeHandler)
	router.HandleFunc("/stats", StatsHandler)

	auth := router.PathPrefix("/auth").Subrouter()
	auth.Path("/login").HandlerFunc(controllers.LoginHandler)
	auth.Path("/logout").HandlerFunc(controllers.LogoutHandler)
	auth.Path("/signup").HandlerFunc(controllers.SignupHandler)

	api := router.PathPrefix("/api").Subrouter()
	api.Path("/docs").HandlerFunc(controllers.DocsHandler)

	return router
}
