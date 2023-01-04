namespace web.Entities 
{
    public class Comment 
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}