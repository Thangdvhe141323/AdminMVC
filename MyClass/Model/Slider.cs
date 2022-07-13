using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Model
{
    [Table("Sliders")]
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        public String Link { get; set; }
        [Required]
        public String Img { get; set; }
        [Required]
        public String Position { get; set; }
        public int? CreateBy { get; set; }

        public DateTime? CreateAt { get; set; }
        public int? UpdateBy { get; set; }

        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }
    }
}
