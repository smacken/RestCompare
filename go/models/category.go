package models

import (
	"github.com/mholt/binding"
	"net/http"
	"time"
)

type Category struct {
	Id               int    `json:"id" sql:"not null"`
	Name             string `json:"name" sql:"not null"`
	Description      string `json:"description"`
	ParentCategoryId int    `json:"id" `
	CreatedAt        time.Time
	UpdatedAt        time.Time
}

// mapping
func (category *Category) FieldMap(req *http.Request) binding.FieldMap {
	return binding.FieldMap{
		&category.Id:               "id",
		&category.Name:             "name",
		&category.Description:      "description",
		&category.ParentCategoryId: "parentCategoryId",
	}
}

// validation
func (category *Category) Validate(req *http.Request, errs binding.Errors) binding.Errors {
	if category.Name == "" {
		errs = append(errs, binding.Error{
			FieldNames:     []string{"name"},
			Classification: "ComplaintError",
			Message:        "A category name is required.",
		})
	}

	if category.Description == "" {
		errs = append(errs, binding.Error{
			FieldNames:     []string{"description"},
			Classification: "ComplaintError",
			Message:        "A category description is required.",
		})
	}
	return errs
}
