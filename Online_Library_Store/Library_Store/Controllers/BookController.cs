using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata.Ecma335;
using Library_Store.Data;
using Library_Store.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Library_Store.Controllers
{
    public class BookController : Controller
    {

        private readonly ApplicationDbContext _dbContext;

        public BookController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult HomePage()
        {
            return View();
        }
        public IActionResult Index()
        {
            var books = _dbContext.Books.ToList();
            if (books.Count !=0)
            {
                return View(books);
            }
            else
            {
                TempData["ErrorMessage"] = "There is no books avalible now";
                return View();
            }
        }

        public IActionResult ShowDetails(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var bookmodel = _dbContext.Books.FirstOrDefault(a => a.BookId == id);

                if (bookmodel == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(bookmodel);
                }

            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var bookmodel = _dbContext.Books.FirstOrDefault(a => a.BookId == id);
                if (bookmodel == null)
                {
                    return NotFound();
                }
                else
                {

                    return View(bookmodel);
                }
            }
        }
        [HttpPost]
        public IActionResult DeleteConfirmation(int id)
        {
            var deleted = _dbContext.Books.FirstOrDefault(a => a.BookId == id);
            if (deleted == null)
            {
                return NotFound();
            }
            _dbContext.Books.Remove(deleted);
            _dbContext.SaveChanges();
            TempData["DeleteMessage"] = "Deleted Successfully";
            return RedirectToAction("index", _dbContext.Books);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddBookDto addBookDto, IFormCollection form)
        {

            if (ModelState.IsValid)
            {
                var bookImage = form.Files["image"];
                if (bookImage != null && bookImage.Length > 0)
                {
                    var fileName = Path.GetFileName(bookImage.FileName);
                    var filePath = Path.Combine("wwwroot", "BooksImages", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        bookImage.CopyToAsync(stream);
                    }
                    var book = new Book()
                    {
                        BookName = addBookDto.BookName,
                        BookPrice = addBookDto.BookPrice,
                        BookQuantity = addBookDto.BookQuantity,
                        BookCategory = addBookDto.BookCategory,
                        BookImage = fileName
                    };
                    _dbContext.Books.Add(book);
                    _dbContext.SaveChanges();
                    TempData["AddSuccessMessage"] = "Added Successfully";
                    return View("index", _dbContext.Books);
                }
                else
                {
                    TempData["AddErrorMessage"] = "There is no image attached please add an image and try again";
                    return View(addBookDto);
                }
            }
            else
            {
                TempData["AddErrorMessage"] = "There is something wrong please try again later";
                return View(addBookDto);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_dbContext.Books.FirstOrDefault(a => a.BookId == id));
        }
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            var existing = _dbContext.Books.FirstOrDefault(a => a.BookId == book.BookId);

            existing.BookPrice = book.BookPrice;
            existing.BookQuantity = book.BookQuantity;
            _dbContext.SaveChanges();
            TempData["UpdateMessage"] = "Updated Successfully";
            return View("index", _dbContext.Books);
        }
    }



}



