using System.ComponentModel.DataAnnotations;

namespace IdentitycrudMVC.Models
{
    public class Product
    {
       
            [Key]
            public int PId { get; set; }

            [Required]
            public string ProductName { get; set; }

            public int CId { get; set; }

      

    }
}
