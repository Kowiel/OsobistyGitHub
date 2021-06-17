using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyDlaCiebie.Models
{
    public class PrzepisDbContext :DbContext
    {
        public PrzepisDbContext(DbContextOptions<PrzepisDbContext> options):base(options)
        {

        }

        public DbSet<Przepis> przepisy { get; set; }
    }
}
