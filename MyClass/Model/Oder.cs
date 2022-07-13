using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClass.Model
{
    [Table("Orders")]
    public class Oder
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; }

        public String DeliveryName { get; set; }
        public String DeliveryEmail { get; set; }
        public String DeliveryPhone { get; set; }
        public String DeliveryAdress { get; set; }

    }
}
