package models

import (
	"github.com/mholt/binding"
	"net/http"
)

type Category struct {
	Model
	Name             string `json:"name" sql:"type:varchar(150);not null"`
	Description      string `json:"description" sql:"type:varchar(250);"`
	ParentCategoryId int    `json:"id" `
	Products         []Product
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
