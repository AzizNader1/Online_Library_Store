using Library_Store.Data;
using Library_Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace Library_Store.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext Context)
        {
            _context = Context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
            var UserEmail = form["UserEmail"].ToString();
            var UserPassword = form["UserPassword"].ToString();
            var existing = _context.Users.FirstOrDefault(a => a.UserEmail == UserEmail);
            var hashedPassword = HashPassword(UserPassword);
            if (existing != null && hashedPassword == existing.UserPassword)
            {
                ViewBag.usersession = existing.UserPassword.ToString();
                return RedirectToAction("Index", "Book", _context.Books);
            }
            else
            {
                ModelState.AddModelError("", "Invaild username or password");
                return View();
            }

        }
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(AddUserDto addUserDto, IFormCollection form)
        {
            if (!ModelState.IsValid)
            {
                return View(addUserDto);
            }
            if (_context.Users.Any(a => a.UserEmail == addUserDto.UserEmail))
            {
                TempData["EmailErrorMessage"] = "User Email already exists";
                return View(addUserDto);
            }
            var userImage = form.Files["image"];
            if (userImage != null && userImage.Length > 0)
            {
                var fileName = Path.GetFileName(userImage.FileName);
                var filePath = Path.Combine("wwwroot", "UsersImages", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    userImage.CopyToAsync(stream);
                }
                var hashedPassword = HashPassword(addUserDto.UserPassword);
                var user = new User()
                {
                    UserEmail = addUserDto.UserEmail,
                    UserName = addUserDto.UserName,
                    UserImage = fileName,
                    UserPassword = hashedPassword
                };
                TempData["UserEmail"] = addUserDto.UserEmail;
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                return View(addUserDto);
            }
        }


        public static string HashPassword(string password)
        {
            byte[] passBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = new MD5CryptoServiceProvider().ComputeHash(passBytes);

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                stringBuilder.Append(hashBytes[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }
}
