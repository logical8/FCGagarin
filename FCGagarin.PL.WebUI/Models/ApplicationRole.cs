using Microsoft.AspNet.Identity.EntityFramework;

namespace FCGagarin.PL.WebUI.Models
{
    public sealed class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            
        }

        public ApplicationRole(string name, string description): base(name)
        {
            Description = description;
        }
        public string Description { get; set; }
    }
}