using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }

        public AppUsers AppUsers { get; set; }
        public int AppUsersId { get; set; }
    }
}