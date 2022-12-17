using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CutiOnlineWEB.Models
{
    public class User
    {
        [Key]
        [ForeignKey("Staff")]
        public int Id_Staff { get; set; }
        public string Password { get; set; }

        public Admin Admin { get; set; }
        [ForeignKey("Admin")]
        public int Id { get; set; }
        public string Name { get; set; }
        public Staff Staff { get; set; }






    }
}
