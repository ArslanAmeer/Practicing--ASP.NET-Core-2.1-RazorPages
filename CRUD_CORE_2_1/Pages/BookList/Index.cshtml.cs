using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_CORE_2_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRUD_CORE_2_1.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        public string Message { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }

        public async Task OnGet()
        {
            using (_db)
            {
                Books = await _db.Books.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            using (_db)
            {
                var book = await _db.Books.FindAsync(id);
                _db.Books.Remove(book);
                await _db.SaveChangesAsync();
            }

            Message = $"Book with Id: {id} Deleted Successfully";
            return RedirectToPage("Index");
        }
    }
}