using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sg_rentals.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Escolha a data inicial de locação.")]
        public DateTime DateStart { get; set; }

        [Required(ErrorMessage = "Escolha a data de devolução.")]
        public DateTime DateEnd { get; set; }

        [Required(ErrorMessage = "Escolha o cliente.")]
        public int CustomerId { get; set; }

        public int CarId { get; set; } = 0;
        public int HouseId { get; set; } = 0;

        public int Type {  get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [BindNever]
        public Customer? Customer { get; set; }
        
        [BindNever]
        public Car? Car { get; set; }
        
        [BindNever]
        public House? House { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Type == 0)
        //    {
        //        if(CarId == null)
        //        {
        //            yield return new ValidationResult("Escolha o veiculo", new[] { nameof(CarId) });
        //        }
        //    } else if (Type == 1)
        //    {
        //        if(HouseId == null)
        //        {
        //            yield return new ValidationResult("Escolha a casa", new[] { nameof(HouseId) });
        //        }
        //    }
        //}
    }
}
