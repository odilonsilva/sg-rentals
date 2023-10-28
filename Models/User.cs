using System.ComponentModel.DataAnnotations;

namespace sg_rentals.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o Nome do usuário.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Digite o E-mail do usuário.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite uma Senha para o usuário.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Digite o Login do usuário.")]
        public string Login {  get; set; }
        public bool isAdmin { get; set; } = false;
    }
}
