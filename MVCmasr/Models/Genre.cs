using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCmasr.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        public virtual ICollection<Album> Albums { get; set; } = new HashSet<Album>();

        //public virtual ICollection<AlbumGenre> AlbumGenre { get; set; } = new HashSet<AlbumGenre>();
    }
}