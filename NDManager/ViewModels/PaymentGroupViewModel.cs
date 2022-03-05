using System.ComponentModel.DataAnnotations;

namespace NDManager.ViewModels
{
    public class PaymentGroupViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa grupy")] public string Name { get; set; }
        [Display(Name = "Dni pracujące w miesiącu")] public byte WorkingDays { get; set; }
        [Display(Name = "Miesiąc")] public byte Month { get; set; }
    }
}
