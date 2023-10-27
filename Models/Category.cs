using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentitycrudMVC.Models
{
    public class Category
    {
        [Key]
        public int CId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [NotMapped]
        public String Photo { get; set; }


    }
   
}
