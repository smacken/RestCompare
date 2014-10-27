using System.ComponentModel.DataAnnotations;
using RestCompare.Data.Validation;
using FluentValidation.Attributes;

namespace RestCompare.Data.Models
{
    [Validator(typeof(ProductValidator))]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
