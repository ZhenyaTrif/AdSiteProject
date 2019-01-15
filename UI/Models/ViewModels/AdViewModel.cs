using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Models.ViewModels
{
    public class AdViewModel
    {
        [Required(ErrorMessage = "Это поле должно быть заполненным")]
        [Display(Name = "Название")]
        [DataType(DataType.Text)]
        public string AdName { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненным")]
        [Display(Name = "Описание")]
        [DataType(DataType.Text)]
        public string Info { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата создания")]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненным")]
        [Display(Name = "Картинка")]
        [DataType(DataType.Text)]
        public string PicPath { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненным")]
        [Display(Name = "Название категории")]
        public string CategoryName { get; set; }

        [Display(Name = "Имя автора")]
        [DataType(DataType.Text)]
        public string AuthorName { get; set; }

        [Display(Name = "Email автора")]
        [DataType(DataType.Text)]
        public string AuthorEmail { get; set; }

        [Display(Name = "Телефон автора")]
        [DataType(DataType.Text)]
        public string AuthorPhone { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненным")]
        [Display(Name = "Местонахождение товара или услуги")]
        [DataType(DataType.Text)]
        public string ProductPlacement { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненным")]
        [Display(Name = "Тип")]
        [DataType(DataType.Text)]
        public string ProductType { get; set; }

        [Required(ErrorMessage = "Это поле должно быть заполненным")]
        [Display(Name = "Состояние товара или услуги")]
        [DataType(DataType.Text)]
        public string ProductState { get; set; }

        [Required]
        [Display(Name = "Объявитель")]
        [DataType(DataType.Text)]
        public string UserId { get; set; }
    }
}