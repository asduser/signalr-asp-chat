using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SignalR_app.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        public string DisplayName { get; set; }
        public DateTime RegisteredFrom { get; set; }
        public string Gender { get; set; }
    }
}