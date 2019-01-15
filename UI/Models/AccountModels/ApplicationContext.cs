using Microsoft.AspNet.Identity.EntityFramework;

namespace UI.Models.AccountModels
{
    public class ApplicationContext : IdentityDbContext<AppUser>
    {
        public ApplicationContext() : base("DefaultConnection", throwIfV1Schema: false) { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}