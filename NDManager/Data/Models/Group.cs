using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDManager.Data.Models
{
    public class Group
    {
        [Display(Name = "Id")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] [Required] [Key] public int Id { get; set; }
        [Display(Name = "Nazwa grupy")] [Required] public string Name { get; set; }
        [ForeignKey("Teacher")] public int TeacherId { get; set; }
        [Display(Name = "Nauczyciel")] public Teacher Teacher { get; set; }
        [Display(Name = "Aktywna")] public bool IsActive { get; set; }

        public virtual IEnumerable<Kid> Kids { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
