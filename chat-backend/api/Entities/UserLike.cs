namespace api.Entities
{
    public class UserLike
    {
        public AppUsers LikedUser { get; set; }
        public int LikedUserId { get; set; }

        public AppUsers SourceUser { get; set; }
        public int SourceUserId { get; set; }
    }
}