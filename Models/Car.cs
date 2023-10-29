using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace sg_rentals.Models
{
    public class Car
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Digite a placa do veiculo.")]
        public string LicensePlate { get; set; }
        
        [Required(ErrorMessage = "Escolha a cor do veiculo.")]
        public string Color { get; set; }
        
        [Required(ErrorMessage = "Selecione o status do veiculo.")]
        public int Status { get; set; } = 0;
        
        [Required(ErrorMessage = "Escolha o fabricante do veiculo.")]
        public int BrandId { get; set; }
        
        [Required(ErrorMessage = "Escolha o modelo do veiculo.")]
        public int CarBrandModelId { get; set; }
        
        [Required(ErrorMessage = "Escolha o modelo do veiculo.")]
        public decimal Price { get; set; }

        [BindNever]
        public CarBrandModel? CarModel { get; set; }
        [BindNever]
        public Brand? Brand { get; set; } 

    }
}
