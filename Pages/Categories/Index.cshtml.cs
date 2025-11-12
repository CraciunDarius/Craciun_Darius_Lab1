using Craciun_Darius_Lab2.Data;
using Craciun_Darius_Lab2.Models;
using Craciun_Darius_Lab2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Craciun_Darius_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Craciun_Darius_Lab2.Data.Craciun_Darius_Lab2Context _context;

        public IndexModel(Craciun_Darius_Lab2.Data.Craciun_Darius_Lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }
        public int BookID { get; set; }
        public async Task OnGetAsync(int? id, int? bookID)
        {
            CategoryData = new CategoryIndexData();

            CategoryData.Categories = await _context.Category
                 .Include(i => i.BookCategories)
                 .ThenInclude(bc => bc.Book)
                 .ThenInclude(b => b.Author)
                 .OrderBy(i => i.CategoryName)
                 .ToListAsync();

            if (id != null)
            {
                CategoryID = id.Value;
                var category = CategoryData.Categories
                    .Where(c => c.ID == id.Value)
                    .Single();

                CategoryData.Books = category.BookCategories
                    .Select(bc => bc.Book)
                    .OrderBy(b => b.Title);
            }
        }
    }
}
