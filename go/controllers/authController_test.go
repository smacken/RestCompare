package controllers

import (
	"github.com/unrolled/render"
	"net/http"
	"net/http/httptest"
	"testing"
)

type LoginRequest struct {
	Username string `json:"username"`
	Password string `json:"password"`
}

func TestingLoginHandler(t *testing.T) {
	r := render.New()

	handler := http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		r.JSON(w, 200, Login{"login", "pass"})
	})

	res := httptest.NewRecorder()
	req, _ := http.NewRequest("GET", "/auth/login", nil)
	handler.ServeHTTP(res, req)

	expect(t, res.Code, 200)
	expect(t, res.Header().Get(ContentType), ContentJSON+"; charset=UTF-8")
}
