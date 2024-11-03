using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaisSocialMediaTask.Core.Entities
{
	public class AppUser: IdentityUser
	{
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public virtual ICollection<Post>? Posts { get; set; }
    }
}
