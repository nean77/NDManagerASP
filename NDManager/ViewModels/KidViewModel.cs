using NDManager.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDManager.ViewModels
{
    public class KidViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Imię")] public string FirstName { get; set; }
        [Display(Name = "Nazwisko")] public string LastName { get; set; }
        [ForeignKey("Group")] public int GroupId { get; set; }
        [Display(Name = "Grupa")]  public Group Group { get; set; }
        [Display(Name = "Stawka żywieniowa - dzienna")] [DataType(DataType.Currency)] public decimal MealDailyRate { get; set; }
        [Display(Name = "Stawka za godziny - dzienna")]  [DataType(DataType.Currency)] public decimal AttendanceDailyRate { get; set; }

        public IEnumerable<Group> GroupList { get; set; }
    }
}
