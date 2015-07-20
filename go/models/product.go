package models

import (
	"github.com/mholt/binding"
)

type Product struct {
	Id          number `json:"id"`
	Name        string `json:"name"`
	Description string `json:"description"`
}

// mapping
func (product *Product) FieldMap(req *http.Request) binding.FieldMap {
	return binding.FieldMap{
		&product.Id:          "id",
		&product.Name:        "name",
		&product.Description: "description",
	}
}

// validation
func (product *Product) Validate(req *http.Request, errs binding.Errors) binding.Errors {
	if product.Name == "" {
		errs = append(errs, binding.Error{
			FieldNames:     []string{"name"},
			Classification: "ComplaintError",
			Message:        "A product name is required.",
		})
	}

	if product.Password == "" {
		errs = append(errs, binding.Error{
			FieldNames:     []string{"description"},
			Classification: "ComplaintError",
			Message:        "A product description is required.",
		})
	}
	return errs
}
