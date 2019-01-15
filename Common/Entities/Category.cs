using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Это поле должно быть заполненным")]
        [Display(Name ="Название категории")]
        [DataType(DataType.Text)]
        public string CategoryName { get; set; }
    }
}
