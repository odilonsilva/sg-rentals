using System.ComponentModel.DataAnnotations;

namespace sg_rentals.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o Nome do cliente.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Digite o E-mail do cliente.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o endereço.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Digite o numero da residência.")]
        public string AddressNumber { get; set; }

        [Required(ErrorMessage = "Digite o telefone.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Digite o Bairro.")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "Digite a Cidade.")]
        public string City { get; set; }
    }
}
