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
    public class CreateModel : PageModel
    {

        private readonly PrzepisDbContext _dbcontext;
        private readonly IWebHostEnvironment _webHostEnviorment;

        public CreateModel(PrzepisDbContext dbContext, IWebHostEnvironment webHostEnviorment)
        {
            _dbcontext = dbContext; //dependecny 
            _webHostEnviorment = webHostEnviorment;
        }
        [BindProperty]
        public Przepis Przepis { get; set; }

        public void OnGet()
        {
        }

     
        public async Task<IActionResult> OnPost([Bind("Id,NazwaPrzepisu,TrescPrzepisu,Image")]Przepis przepis)
        {
            if(ModelState.IsValid)
            {
                if(przepis.Image!=null)
                {
                    string WWWRotPath = _webHostEnviorment.WebRootPath;
                    string FileName = Path.GetFileNameWithoutExtension(przepis.Image.FileName);
                    string Extension = Path.GetExtension(przepis.Image.FileName);
                    przepis.NazwaZdjecia = FileName = FileName + DateTime.Now.ToString("yymmssfff") + Extension;
                    string path = Path.Combine(WWWRotPath + "/Images/", FileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await Przepis.Image.CopyToAsync(fileStream);
                    }
                }
                await _dbcontext.AddAsync(przepis);
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
