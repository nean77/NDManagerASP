using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDManager.Data.Models
{
    public class Teacher
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] [Required] [Key] public int Id { get; set; }
        [Display(Name = "Imię")] [Required] public string FirstName { get; set; }
        [Display(Name = "Nazwisko")] [Required] public string LastName { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
