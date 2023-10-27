using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentitycrudMVC.Models
{
    public class PhotoModel
    {
      
            public int Id { get; set; }
        public string Filename { get; set; }

        public string FilePath { get; set; }
        [NotMapped]
        public IFormFile Fileobj { get; set; }  

    }
}
