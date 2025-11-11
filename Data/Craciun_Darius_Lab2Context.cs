using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Craciun_Darius_Lab2.Models;

namespace Craciun_Darius_Lab2.Data
{
    public class Craciun_Darius_Lab2Context : DbContext
    {
        public Craciun_Darius_Lab2Context (DbContextOptions<Craciun_Darius_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Craciun_Darius_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Craciun_Darius_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Craciun_Darius_Lab2.Models.Author> Author { get; set; } = default!;
    }
}
