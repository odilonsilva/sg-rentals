using System.ComponentModel.DataAnnotations;

namespace sg_rentals.Models
{
    public class House
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite uma descrição da casa.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Digite o valor de locação.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Digite a quantidade de quartos.")]
        public int Rooms { get; set; } = 1;

        [Required(ErrorMessage = "Digite o endereço.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Digite o numero da residência.")]
        public string AddressNumber { get; set; }

        [Required(ErrorMessage = "Digite o Bairro.")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "Digite a Cidade.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Digite a CEP.")]
        public string ZipCode { get; set; }
    }
}
