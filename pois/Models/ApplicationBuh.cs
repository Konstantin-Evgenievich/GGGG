using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diplom.Models
{
    public class ApplicationBuh
    {

        public int ID { get; set; }//ID торта

        [DisplayName("Название организации")]
        public string NAME { get; set; }//название торта

        [DisplayName("ФИО")]
        public string FILLING_TYPE { get; set; }//тип начинки

        [DisplayName("Форма собственности")]
        public string TYPE_OF_DESIGN { get; set; }//тип дизайна
        [DisplayName("Система налогообложения")]
        public string TYPE_OF_DECORATION { get; set; }//тип украшений
        [DisplayName("Сфера вашей деятельности")]
        public string PLACE_OF_MANUFACTURE { get; set; }//место производства
        [DisplayName("Время заполнения заявки")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")] public DateTime CREATE_DATE { get; set; }//время изготовления

    }
}

