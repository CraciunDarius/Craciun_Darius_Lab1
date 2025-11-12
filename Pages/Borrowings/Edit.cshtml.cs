using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Craciun_Darius_Lab2.Data;
using Craciun_Darius_Lab2.Models;

namespace Craciun_Darius_Lab2.Pages.Borrowings
{
    public class EditModel : PageModel
    {
        private readonly Craciun_Darius_Lab2.Data.Craciun_Darius_Lab2Context _context;

        public EditModel(Craciun_Darius_Lab2.Data.Craciun_Darius_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing =  await _context.Borrowing.FirstOrDefaultAsync(m => m.ID == id);
            if (borrowing == null)
            {
                return NotFound();
            }
            Borrowing = borrowing;

            //Populam listele derulante cu date relevante
            var books = await _context.Book
                .Include(b => b.Author)
                .ToListAsync();
            var members = await _context.Member.ToListAsync();

            ViewData["BookID"] = new SelectList(_context.Book, "ID", "Title");
           ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["BookID"] = new SelectList(_context.Book, "ID", "Title");
                ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
                return Page();
            }

            _context.Attach(Borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(Borrowing.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.ID == id);
        }
    }
}
