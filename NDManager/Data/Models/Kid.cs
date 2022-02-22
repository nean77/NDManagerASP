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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] [Required] [Key] public int Id { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public Group Group { get; set; }
        [Required] [DataType(DataType.Currency)] public decimal MealDailyRate { get; set; }
        [Required] [DataType(DataType.Currency)] public decimal AttendanceDailyRate { get; set; }
    }
}
