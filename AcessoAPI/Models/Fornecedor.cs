//  Erasmo Cardoso

using System.ComponentModel.DataAnnotations;

namespace AcessoAPI.Models
{
    public class Fornecedor
    {
        [Key]
        public int FornecedorID { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Contato { get; set; } = string.Empty;

        [Required]
        public string Telefone { get; set; } = string.Empty;
    }
}
