namespace FinalProject.Service.Exceptions
{
    internal class PostNotFoundException : Exception
    {
        public PostNotFoundException() : base("POST not found in database")
        {
            
        }
    }
}
