using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diplom.Models
{
    public class User
    {
        public int ID { get; set; }

        [DisplayName("Имя")]
        [DataType(DataType.Text)]
        public string NAME { get; set; }

        [DisplayName("Отчество")]
        [DataType(DataType.Text)]
        public string MIDDLENAME { get; set; }

        [DisplayName("Фамилия")]
        [DataType(DataType.Text)]
        public string SURNAME { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Дата рождения")]
        public DateTime DATEOFBIRTH { get; set; }

        [DisplayName("Телефон")]
        [DataType(DataType.PhoneNumber)]
        public string PHONE { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string EMAIL { get; set; }

        [DisplayName("Номер паспорта")]
        [DataType(DataType.Text)]
        public string PASSPORT { get; set; }

        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string PASSWORD { get; set; }
        public int? RoleId { get; set; }
        public UserRole Role { get; set; }

        [DisplayName("Дата регистрации")]
        public DateTime CREATE_DATE { get; set; }

        [DisplayName("Дата последнего изменения")]
        public DateTime UPDATE_DATE { get; set; }
    }
}
