using FinalProject.Entities;
using FinalProject.Models.Comment;

namespace FinalProject.Models.Post
{
    public class PostForGettingDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int NumberOfComments { get; set; }

        public DateTime CreationTime { get; set; }

        public string AuthorId { get; set; }

        public State State { get; set; } 

        public Status Status { get; set; } 
    }   
    
    public class PostForGettingDetailedDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public List<CommentForGettingDTO> Comments { get; set; }

        public DateTime CreationTime { get; set; } 
        public string AuthorId { get; set; }

        public State State { get; set; }

        public Status Status { get; set; }
    }
}
