using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Model
{
    [Table("Menu")]
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        [Required]
        public String Link { get; set; }
        public int ParentId { get; set; }
        public int Orders { get; set; }
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
