// Erasmo Cardoso
using System;
using System.ComponentModel.DataAnnotations;

namespace AcessoAPI.Models
{
    public class NotaFiscal
    {
        
        [Key]
        public int NotaFiscalID { get; set; }

        [Required(ErrorMessage = "O número da nota fiscal é obrigatório.")]
        [StringLength(50, ErrorMessage = "O número da nota fiscal não pode exceder 50 caracteres.")]
        public string Numero { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de emissão é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataEmissao { get; set; }

        [Required(ErrorMessage = "O valor da nota fiscal é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; }

        [StringLength(100, ErrorMessage = "A descrição não pode exceder 100 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ID do cliente é obrigatório.")]
        public int ClienteID { get; set; }

    }
}
