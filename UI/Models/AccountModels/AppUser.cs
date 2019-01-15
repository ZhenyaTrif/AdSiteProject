using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace UI.Models.AccountModels
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "Название роли")]
        public string RoleName { get; set; }
        //public string OldRole { get; set; }
        public AppUser() { }
    }
}