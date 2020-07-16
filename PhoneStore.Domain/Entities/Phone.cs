using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PhoneStore.Domain.Entities
{
    public class Phone
    {
        [HiddenInput(DisplayValue = false)]
        public int PhoneId { get; set; }
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название телефона")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание для телефона")]
        public string Description { get; set; }
        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Пожалуйста, укажите категорию для телефона")]
        public string Category { get; set; }
        [Display(Name = "Цена (грн)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
        public decimal Price { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
