using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int CatId { get; set; }
        public int SuplierId { get; set; }
        [Required]
        public String Name { get; set; }
        public String Slug { get; set; }
        public String Detail { get; set; }
        public int Number { get; set; }
        public int PriceBuy { get; set; }
        public int PriceSale { get; set; }
        public String Img { get; set; }
        [Required]
        public string MetaDesc { get; set; }

        [Required]
        public string MetaKey { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateAt { get; set; }
        public int? UpdateBy { get; set; }

        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }



    }
}
