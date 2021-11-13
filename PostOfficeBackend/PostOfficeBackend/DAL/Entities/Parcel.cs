using System.ComponentModel.DataAnnotations.Schema;

namespace PostOfficeBackend.DAL.Entities
{
    public class Parcel
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(6,3)")]
        public decimal Weight { get; set; }
        public string Phone { get; set; }
        public string Info { get; set; }

        [ForeignKey("Post")]
        public int? PostId { get; set; }
        public Post Post { get; set; }
    }
}
