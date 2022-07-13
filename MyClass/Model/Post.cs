using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Model
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public int? TopicId { get; set; }
        [Required]
        public String Title { get; set; }
        public String Slug { get; set; }
        [Required]
        public String Detail { get; set; }
        public String Img { get; set; }
        public String PostType { get; set; }
        [Required]
        public String MetaKey { get; set; }
        [Required]
        public String MetaDesc { get; set; }
        public int? CreateBy { get; set; }

        public DateTime? CreateAt { get; set; }
        public int? UpdateBy { get; set; }

        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }


    }
}
