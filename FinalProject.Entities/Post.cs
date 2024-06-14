namespace FinalProject.Entities
{
    public class Post
    {
        
        public int Id { get; set; }

        
        public string Title { get; set; }

       

        public string Description { get; set; }

        

        public DateTime CreationTime { get; set; }

        
        public string AuthorId {  get; set; } 
        
        
        public User Author { get; set; }
        
        public State State { get; set; }
        public Status Status { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
