//  Erasmo Cardoso
// CODANDO AINDA

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

        [Required(ErrorMessage = "A data da nota fiscal é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; } 

        [Required(ErrorMessage = "O valor da nota fiscal é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; } 

        [StringLength(100, ErrorMessage = "A descrição não pode exceder 100 caracteres.")]
        public string Descricao { get; set; } = string.Empty; 

        // ID do cliente 
        [Required(ErrorMessage = "O ID do cliente é obrigatório.")]
        public int ClienteID { get; set; }

        public virtual Cliente? Cliente { get; set; }

        [StringLength(50, ErrorMessage = "O tipo da nota fiscal não pode exceder 50 caracteres.")]
        public string TipoNota { get; set; } = string.Empty; 

        [StringLength(20, ErrorMessage = "O status da nota fiscal não pode exceder 20 caracteres.")]
        public string Status { get; set; } = string.Empty; 

        public DateTime? DataVenda { get; set; }
    }
}
