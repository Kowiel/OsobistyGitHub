using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrzepisyDlaCiebie.Models;
using Microsoft.AspNetCore.Http;


namespace PrzepisyDlaCiebie.Pages.Przepisy
{
    public class EditModel : PageModel
    {
        private readonly PrzepisDbContext _dbcontext;
        private readonly IWebHostEnvironment _webHostEnviorment;

        public EditModel(PrzepisDbContext dbContext, IWebHostEnvironment webHostEnviorment)
        {
            _dbcontext = dbContext; //dependecny 
            _webHostEnviorment = webHostEnviorment;
        }
        [BindProperty]
        public Przepis Przepis { get; set; }
        public async Task OnGet(int id)
        {
            Przepis = await _dbcontext.przepisy.FindAsync(id);
            
        }
       
        public async Task<IActionResult> OnPost([Bind("Id,NazwaPrzepisu,TrescPrzepisu,Image")] Przepis przepis)
        {
            var PrzepisDb = await _dbcontext.przepisy.FindAsync(Przepis.Id);
            if (ModelState.IsValid)
            {
                if(przepis.Image!=null)
                {
                    string tempFilename = PrzepisDb.NazwaZdjecia;
                    string WWWRotPath = _webHostEnviorment.WebRootPath;
                    string FileName = Path.GetFileNameWithoutExtension(przepis.Image.FileName);
                    string Extension = Path.GetExtension(przepis.Image.FileName);
                    przepis.NazwaZdjecia = FileName = FileName + DateTime.Now.ToString("yymmssfff") + Extension;
                    string path = Path.Combine(WWWRotPath + "/Images/", FileName);
                    string pathToDel = Path.Combine(WWWRotPath + "/Images/", tempFilename);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                       
                        await Przepis.Image.CopyToAsync(fileStream);
                        System.IO.File.Delete(pathToDel);
                    }
                    PrzepisDb.NazwaZdjecia = przepis.NazwaZdjecia;
                }
                PrzepisDb.NazwaPrzepisu = przepis.NazwaPrzepisu;
                PrzepisDb.TrescPrzepisu = przepis.TrescPrzepisu;
             
                await _dbcontext.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
