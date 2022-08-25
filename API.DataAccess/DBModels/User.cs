using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DataAccess.DBModels
{
    public class User : IdentityUser
    {
        public User()
        {
            
        }
        public Guid Id { get; set; }
        [Range(2, 40)]
        public string FirstName { get; set; }
        [Range(2, 40)]
        public string Lastname { get; set; }
        
    }
}
