using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDManager.Data.Models
{
    public class Payment
    {
        [Display(Name = "Id")] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] [Required] [Key] public int Id { get; set; }
        [ForeignKey("Kid")] [Required] public int KidId { get; set; }
        [Display(Name = "Dziecko")] public Kid Kid { get; set; }
        [Display(Name = "Kwota")] [Required] [DataType(DataType.Currency)] public decimal Value { get; set; }
        [Display(Name = "Dni pracujące w miesiącu")] [Required] public byte WorkingDays { get; set; }
        [Display(Name = "Miesiąc")][Required] public byte MonthNo { get; set; }
    }
}
