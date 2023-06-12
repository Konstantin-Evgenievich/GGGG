using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diplom.Models
{
    public class OrderBuh
        {
        
            public int ID { get; set; }//ID заказа
            [DisplayName("Цена торта")]
            
            public int PRICE { get; set; }//цена торта
            [DisplayName("Торт")]
            public int CakeID { get; set; }//ID торта
            public ApplicationBuh Cake { get; set; }
            [DisplayName("Пользователь")]
            public int? UserID { get; set; }//ID пользователя (nullable) 
            public User User { get; set; }
            [DisplayName("Адрес получения")]
            public string RECEIVING_ADDRESS { get; set; }//Адрес получения
            [DisplayName("Статус заказа")]
            public string status { get; set; }//тип дизайна
            public string test { get; set; }
    

    }
}
