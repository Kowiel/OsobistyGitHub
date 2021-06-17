using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PrzepisyDlaCiebie.Models;

namespace PrzepisyDlaCiebie.Pages.Przepisy
{
    public class IndexModel : PageModel
    {
        private readonly PrzepisDbContext _dbcontext;
        private readonly IWebHostEnvironment _webHostEnviorment;

        public IndexModel(PrzepisDbContext dbContext, IWebHostEnvironment webHostEnviorment)
        {
            _dbcontext = dbContext; //dependecny 
            _webHostEnviorment = webHostEnviorment;
        }

        [BindProperty]
        public Przepis Przepis { get; set; }
        public IEnumerable<Przepis> Przepisy { get; set; }
        public async Task OnGet()
        {
            Przepisy = await _dbcontext.przepisy.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var PrzepisDB = await _dbcontext.przepisy.FindAsync(id);
            if(PrzepisDB!=null)
            {
                if(PrzepisDB.Image!=null)
                {
                    string tempFilename = PrzepisDB.NazwaZdjecia;
                    string WWWRotPath = _webHostEnviorment.WebRootPath;
                    string pathToDel = Path.Combine(WWWRotPath + "/Images/", tempFilename);
                    System.IO.File.Delete(pathToDel);
                }
                _dbcontext.przepisy.Remove(PrzepisDB);
                await _dbcontext.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return NotFound();
        }
    }
}
