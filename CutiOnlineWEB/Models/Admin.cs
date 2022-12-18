using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CutiOnlineWEB.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
