//  Erasmo Cardoso

using System.ComponentModel.DataAnnotations;

namespace AcessoAPI.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoID { get; set; } // ProdutoID vai sql gera tambem

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do produto não pode exceder 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço do produto é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O ID do fornecedor é obrigatório.")]
        public int FornecedorID { get; set; } 
    }
}
