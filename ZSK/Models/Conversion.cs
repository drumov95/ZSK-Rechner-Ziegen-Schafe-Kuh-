using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZSK.Models
{
    public enum ConversionDirection
    {
        [Display(Name = "Tiere → Euro")]
        AnimalsToEuro = 1,

        [Display(Name = "Euro → Tiere")]
        EuroToAnimals = 2
    }

    public class Conversion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tier-ID")]
        public int AnimalRateId { get; set; }

        [Display(Name = "Tier")]
        public AnimalRate AnimalRate { get; set; } = null!;

        [Required]
        [Display(Name = "Richtung")]
        public ConversionDirection Direction { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Betrag in Euro")]
        public decimal EuroAmount { get; set; }

        [Required]
        [Display(Name = "Menge")]
        public int Quantity { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Euro-Wert pro Einheit bei Erstellung")]
        public decimal UnitEuroAtCreation { get; set; }

        [Required]
        [Display(Name = "Erstellt am")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}