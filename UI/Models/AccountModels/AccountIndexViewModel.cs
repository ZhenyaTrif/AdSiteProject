using System.ComponentModel.DataAnnotations;

namespace UI.Controllers
{
    public class AccountIndexViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}