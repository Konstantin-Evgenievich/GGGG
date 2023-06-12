using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diplom.Models
{
    public class ApplicationUr
    {
        public int ID { get; set; }//ID торта

        [DisplayName("Название организации")]

        public string PHYSICAL_LAW { get; set; }//название торта

        [DisplayName("ФИО")]
        public string NAME_ORG { get; set; }//тип начинки

        [DisplayName("Форма собственности")]
        public string ACTIVITY { get; set; }//тип дизайна
        [DisplayName("Система налогообложения")]

        public string QUESTION { get; set; }//тип украшений
        [DisplayName("Сфера вашей деятельности")]
   
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")] public DateTime TIME { get; set; }//время изготовления

    }
}