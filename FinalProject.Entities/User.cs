﻿using Microsoft.AspNetCore.Identity;

namespace FinalProject.Entities
{
    
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBlocked {  get; set; }  

    }
}
