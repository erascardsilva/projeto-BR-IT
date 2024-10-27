//  Erasmo Cardoso

using System.ComponentModel.DataAnnotations;

namespace AcessoAPI.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais que 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido.")]
        [StringLength(150, ErrorMessage = "O e-mail não pode ter mais que 150 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [StringLength(20, ErrorMessage = "O telefone não pode ter mais que 20 caracteres.")]
        public string Telefone { get; set; } = string.Empty;
    }
}
