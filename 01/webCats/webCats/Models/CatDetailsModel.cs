
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webCats.Models
{
    public class CatDetailsModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "invalid length")]
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Id == 5 && Name == "Pesho")
            {
                yield return new ValidationResult("Sry, id cannot be 5 if your name is Pesho");
                //ili errors.Add(new ValidationResult("niakva gresssshka"));
            }
            //return errors;
        }
    }
}
