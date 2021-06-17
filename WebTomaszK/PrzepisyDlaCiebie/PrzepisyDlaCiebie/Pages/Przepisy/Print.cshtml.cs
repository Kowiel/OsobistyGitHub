using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrzepisyDlaCiebie.Models;

namespace PrzepisyDlaCiebie.Pages.Przepisy
{
    public class PrintModel : PageModel
    {
        private readonly PrzepisDbContext _dbcontext;

        public PrintModel(PrzepisDbContext dbContext)
        {
            _dbcontext = dbContext; //dependecny 
        }
        [BindProperty]
        public Przepis Przepis { get; set; }
        public async Task OnGet(int id)
        {
            Przepis = await _dbcontext.przepisy.FindAsync(id);
        }
    }
}
