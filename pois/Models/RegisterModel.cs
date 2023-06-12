using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Diplom.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Не указано имя")]
        [DataType(DataType.Text)]
        public string NAME { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [DataType(DataType.Text)]
        public string SURNAME { get; set; }

        [Required(ErrorMessage = "Не указано отчество")]
        [DataType(DataType.Text)]
        public string MIDDLENAME { get; set; }

        [Required(ErrorMessage = "Не указана дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DATEOFBIRTH { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PHONE { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Номер паспорта должен содержать 10 цифр")]
        [Required(ErrorMessage = "Не указан номер паспорта")]
        [DataType(DataType.Text)]
        public string PASSPORT { get; set; }


    }
}
