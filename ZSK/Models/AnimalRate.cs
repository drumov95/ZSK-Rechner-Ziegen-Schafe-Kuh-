using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZSK.Models
{
    public class AnimalRate
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Der Name ist erforderlich.")]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "Der Euro-Wert ist erforderlich.")]
        [Display(Name = "Euro Value")]
        public decimal EuroValue { get; set; }   
        public List<Conversion> Conversions { get; set; } = new();
    }
}
