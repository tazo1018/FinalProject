using Microsoft.AspNetCore.Identity;

namespace FinalProject.Entities
{
    // dbContext ro meqneba unda gamoviyeno IdentityDbContext
    // identityUser aris clasi racaa  ssawiro
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBlocked {  get; set; }  

    }
}
