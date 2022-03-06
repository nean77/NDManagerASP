using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDManager.Data.Models
{
    public class Kid
    {
        [Display(Name = "Id")] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] [Required] [Key] public int Id { get; set; }
        [Display(Name = "Imię")] [Required] public string FirstName { get; set; }
        [Display(Name = "Nazwisko")] [Required] public string LastName { get; set; }
        [ForeignKey("Group")] public int GroupId { get; set; }
        [Display(Name = "Grupa")] [Required] public Group Group { get; set; }
        [Display(Name = "Stawka żywieniowa - dzienna")] [Required] [DataType(DataType.Currency)] public decimal MealDailyRate { get; set; }
        [Display(Name = "Stawka za godziny - dzienna")] [Required] [DataType(DataType.Currency)] public decimal AttendanceDailyRate { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
