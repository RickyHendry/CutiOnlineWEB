using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CutiOnlineWEB.Models
{
    public class Staff
    {
        [Key]
        public int Id_Staff { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
