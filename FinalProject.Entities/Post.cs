namespace FinalProject.Entities
{
    public class Post
    {
        //[Key]
        //[Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required]
        //[MaxLength(50)]
        public string Title { get; set; }

        // unda davamato context da kide raghacebi...
        //[Required]

        public string Description { get; set; }

        //[NotMapped] // wesit esaa :))
        //public int NumberOfComments { get; set; }

        //[Required]
        public DateTime CreationTime { get; set; }

        //[Required]
        //[ForeignKey("Author")]
        public string AuthorId {  get; set; } // guid mqonda aq
        
        //[Required]
        public User Author { get; set; }
        //[Required]
        public State State { get; set; }
        //[Required]
        public Status Status { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
