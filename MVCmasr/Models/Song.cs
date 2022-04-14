using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCmasr.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }
        [ForeignKey("Album")]
        public int AlbumId { get; set; }

        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public bool IsFeatured { get; set; }

        public virtual Album Album { get; set; }
        public virtual Genre Genre { get; set; }

        public virtual ICollection<SongArtist> SongArtists { get; set;} = new HashSet<SongArtist>();    

    }
}