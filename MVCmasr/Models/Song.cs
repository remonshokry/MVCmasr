using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

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
        public string Audio { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile AudioFile { get; set; }

        public Album Album { get; set; }
        public Genre Genre { get; set; }

        public virtual ICollection<Artist> Artists { get; set; } = new HashSet<Artist>();


    }
}