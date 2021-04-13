using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Brand : IDBEntity
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(40)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
