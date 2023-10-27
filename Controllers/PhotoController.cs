using IdentitycrudMVC.Data;
using IdentitycrudMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentitycrudMVC.Controllers
{
    public class PhotoController : Controller
    {




        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _iweb;

        public PhotoController(ApplicationDbContext context,IWebHostEnvironment iweb)
        {
            _context = context;
            _iweb = iweb;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Photos != null ?
                      View(await _context.Photos.ToListAsync()) :
                      Problem("Entity set 'ApplicationDbContext.categories'  is null.");
            return View();
        }
        
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Createpost(IFormFile Fileobj,PhotoModel ph)
        {
           


            var imgtxt=Path.GetExtension(Fileobj.FileName);
            if(imgtxt ==".jpg"|| imgtxt==".gif")
            {
                var uploading = Path.Combine(_iweb.WebRootPath, "Images", Fileobj.FileName);

                var stream = new FileStream(uploading, FileMode.Create);
                await Fileobj.CopyToAsync(stream);
                stream.Close();

                ph.Filename = Fileobj.FileName;
                ph.FilePath = uploading;
                //await _context.Saveimg.AddAsync(ph);
                _context.Photos.Add(ph);
                await _context.SaveChangesAsync();


            
            }
            return RedirectToAction("Index");
        }

    }
}
