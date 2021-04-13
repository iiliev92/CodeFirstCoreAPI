using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Product : IDBEntity
    {
        [Key]
        [MaxLength(40)]
        public string Barcode { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(80)]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Brand is required!")]
        public virtual Brand Brand { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
