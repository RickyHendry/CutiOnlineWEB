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
        public int StaffId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Staff Staff { get; set; }

        [ForeignKey("Admin")]
        public Admin Admin { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
       
    }
}
