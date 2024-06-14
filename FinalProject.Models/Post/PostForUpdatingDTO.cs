using FinalProject.Entities;

namespace FinalProject.Models.Post
{
    public class PostForUpdatingDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }    
    
    public class PostForChangingStatusDTO
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
    public class PostForChangingStateDTO
    {
        public int Id { get; set; }
        public State State { get; set; }
    }
}
