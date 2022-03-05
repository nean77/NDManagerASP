using System.ComponentModel.DataAnnotations;

namespace NDManager.ViewModels
{
    public class PaymentViewModel
    {
        [Display(Name = "Id")] public int Id { get; set; }
        [Display(Name = "Id dziecka")] public int KidId { get; set; }
        [Display(Name = "Imię")] public string FirstName { get; set; }
        [Display(Name = "Nazwisko")] public string LastName { get; set; }
        [Display(Name = "Kwota")]  public decimal Value { get; set; }
        [Display(Name = "Odliczenie")] public decimal Deduction { get; set; }
        [Display(Name = "Miesiąc")]  public byte MonthNo { get; set; }
        [Display(Name = "Dni pracujące w miesiącu")] public byte WorkingDays { get; set; }
    }
}
