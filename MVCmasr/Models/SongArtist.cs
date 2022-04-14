using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCmasr.Models
{
    public class SongArtist
    {
        [ForeignKey("Song")]
        public int SongId { get; set; }
        public virtual Song Song { get; set; }

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }


    }
}
