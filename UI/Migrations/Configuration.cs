namespace UI.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using UI.Models.AccountModels;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "UI.Models.AccountModels.ApplicationContext";
        }

        protected override void Seed(UI.Models.AccountModels.ApplicationContext context)
        {
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
