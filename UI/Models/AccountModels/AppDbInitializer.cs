using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace UI.Models.AccountModels
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<AppUser>(context));

            var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(context));

            var role1 = new AppRole { Name = "administrator" };
            var role2 = new AppRole { Name = "moderator" };
            var role3 = new AppRole { Name = "user" };

            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            base.Seed(context);
        }
    }
}