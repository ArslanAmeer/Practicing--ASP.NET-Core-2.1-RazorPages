using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_CORE_2_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUD_CORE_2_1.Pages.BookList
{
    public class EditModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet(int id)
        {
            using (_db)
            {
                Book = _db.Books.Find(id);
            }
        }

        public async Task<IActionResult> OnPost()
        {
            using (_db)
            {
                var oldBook = _db.Books.Find(Book.Id);
                oldBook.Name = Book.Name;
                oldBook.ISBN = Book.ISBN;
                oldBook.Author = Book.Author;

                await _db.SaveChangesAsync();
                Message = $"Book: {Book.Name} Update Successfully";
            }

            return RedirectToPage("Index");
        }
    }
}