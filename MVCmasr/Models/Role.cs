using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCmasr.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Artist> Artists { get; set; } = new HashSet<Artist>();

    }
}