using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CutiOnlineWEB.Models
{
    public class Crud
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public RCrud RCrud { get; set; }
        [ForeignKey("RCrud")]
        public int RcrudId { get; set; }


    }
}
