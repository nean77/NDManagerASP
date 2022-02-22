using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDManager.Data.Models
{
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] [Required] [Key] public int Id { get; set; }
        [Required] public string Name { get; set; }
        public Teacher Teacher { get; set; }
        public bool IsActive { get; set; }

        public virtual IEnumerable<Kid> Kids { get; set; }
    }
}
