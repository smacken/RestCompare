package models

import (
	"database/sql"
	"github.com/mholt/binding"
	"net/http"
)

type Product struct {
	Model
	Name        string        `json:"name" sql:"not null"`
	Description string        `json:"description"`
	Category    Category      `json:"category"`
	CategoryId  sql.NullInt64 `json:"categoryId" sql:"index:idx_products_bycategory"`
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

	if product.Description == "" {
		errs = append(errs, binding.Error{
			FieldNames:     []string{"description"},
			Classification: "ComplaintError",
			Message:        "A product description is required.",
		})
	}
	return errs
}
