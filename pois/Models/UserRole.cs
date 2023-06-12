using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public UserRole()
        {
            Users = new List<User>();
        }
    }
}
