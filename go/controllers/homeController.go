package controllers

import (
	"encoding/json"
	"fmt"
	"github.com/thoas/stats"
	"net/http"
)

// GET "/"
func HomeHandler(res http.ResponseWriter, req *http.Request) {
	fmt.Fprintf(res, "Hello \n")
}

// GET "/"
func DocsHandler(res http.ResponseWriter, req *http.Request) {
	fmt.Fprintf(res, "Api reference \n")
}

func StatsHandler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")

	stats := stats.New().Data()

	b, _ := json.Marshal(stats)

	w.Write(b)
}
