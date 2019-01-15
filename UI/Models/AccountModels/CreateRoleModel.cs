using System.ComponentModel.DataAnnotations;

namespace UI.Models.AccountModels
{
    public class CreateRoleModel
    {
        [Display(Name = "Название роли")]
        public string Name { get; set; }
    }
}