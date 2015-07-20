package controllers

import (
	"github.com/mholt/binding"
	"github.com/unrolled/render"
	"net/http"
)

type LoginRequest struct {
	Username string `json:"username"`
	Password string `json:"password"`
}

func (login *LoginRequest) FieldMap(req *http.Request) binding.FieldMap {
	return binding.FieldMap{
		&login.Username: "username",
		&login.Password: "password",
	}
}

func (login *LoginRequest) Validate(req *http.Request, errs binding.Errors) binding.Errors {
	if login.Username == "" {
		errs = append(errs, binding.Error{
			FieldNames:     []string{"username"},
			Classification: "ComplaintError",
			Message:        "A username is required.",
		})
	}

	if login.Password == "" {
		errs = append(errs, binding.Error{
			FieldNames:     []string{"password"},
			Classification: "ComplaintError",
			Message:        "A password is required.",
		})
	}
	return errs
}

func LoginHandler(res http.ResponseWriter, req *http.Request) {
	r := render.New()
	login := new(LoginRequest)
	if binding.Bind(req, login).Handle(res) {
		return
	}
	r.JSON(res, http.StatusOK, login)
}

func LogoutHandler(res http.ResponseWriter, req *http.Request) {

}

func SignupHandler(res http.ResponseWriter, req *http.Request) {

}
