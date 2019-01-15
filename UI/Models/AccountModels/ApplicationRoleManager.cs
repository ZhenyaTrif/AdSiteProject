using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace UI.Models.AccountModels
{
    class ApplicationRoleManager : RoleManager<AppRole>
    {
        public ApplicationRoleManager(RoleStore<AppRole> store)
                    : base(store)
        { }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options,
                                                IOwinContext context)
        {
            return new ApplicationRoleManager(new
                    RoleStore<AppRole>(context.Get<ApplicationContext>()));
        }
    }
}