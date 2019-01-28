using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_CORE_2_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUD_CORE_2_1.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        public string Message { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (_db)
            {
                _db.Books.Add(Book);
                await _db.SaveChangesAsync();
            }

            Message = $"Book: {Book.Name} Created Successfully";
            return RedirectToPage("Index");
        }
    }
}