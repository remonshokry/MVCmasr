using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCmasr.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        
        public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();
        public virtual ICollection<Album> Albums { get; set; } = new HashSet<Album>();
        public virtual ICollection<Song> Songs { get; set; } = new HashSet<Song>();

        //public virtual ICollection<SongArtist> SongArtist { get; set; } = new HashSet<SongArtist>();

    }
}