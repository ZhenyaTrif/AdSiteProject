using System.ComponentModel.DataAnnotations;

namespace UI.Models.AccountModels
{
    public class EditRoleModel
    {
        public string Id { get; set; }

        [Display(Name = "Название роли")]
        public string Name { get; set; }
    }
}