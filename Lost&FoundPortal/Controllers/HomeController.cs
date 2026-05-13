using Lost_FoundPortal.Data;
using Lost_FoundPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lost_FoundPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        //constructor
        //context is used to communicate with DB
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        //checks if session exists
        private bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail"));
        }

        // Loads Homepage
        public IActionResult Index()
        {
            return View();
        }

        // Get Login Page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        //Post Login Form
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var student = _context.Users
                .FirstOrDefault(s => s.Email == email && s.Password == password);

            if (student == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }
            
            //Store Session

            HttpContext.Session.SetString("UserEmail", student.Email!);
            HttpContext.Session.SetString("UserName", student.FullName!);
            HttpContext.Session.SetInt32("UserId", student.UserId);

            return RedirectToAction("Index");
        }

      //Clear Session to Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        // Load Register Page
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //post registration form

        [HttpPost]
        public IActionResult Register(Student model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool exists = _context.Users.Any(s => s.Email == model.Email);
            if (exists)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return View(model);
            }
            model.CreatedAt = DateTime.Now;
            _context.Users.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        // Fetch lost items from DB and show on view
        public IActionResult Lost()
        {
            var items = _context.LostItems.ToList();
            var users = _context.Users.ToList();
            ViewBag.IsLoggedIn = IsLoggedIn();
            ViewBag.Users = users;
            return View("lost_items", items);
        }

        // Fetch found items from DB and show on view
        public IActionResult Found()
        {
            var items = _context.FoundItems.ToList();
            var users = _context.Users.ToList();
            ViewBag.IsLoggedIn = IsLoggedIn();
            ViewBag.Users = users;
            return View("found_items", items);
        }

        // ---------------- SEARCH PAGE ----------------
        [HttpGet]
        public IActionResult Search()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login");

            return View();
        }

        //
        [HttpPost]
        public IActionResult Search(string query)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login");

            //EF framework queries
            var results = _context.LostItems
                .Where(x => x.ItemName!.Contains(query) || x.LocationLost!.Contains(query))
                .ToList();

            var users = _context.Users.ToList();

            ViewBag.SearchResults = results;
            ViewBag.Users = users;
            ViewBag.Query = query;

            return View();
        }
        // ---------------- POST LOST ITEM ----------------
        [HttpPost]
        public IActionResult Lost_Items(LostItem item, IFormFile ImageFile)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(u => u.Email == HttpContext.Session.GetString("UserEmail"));
            item.UserId = user!.UserId;
            item.Status = "active";
            item.CreatedAt = DateTime.Now;

            if (ImageFile != null)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                    ImageFile.CopyTo(stream);
                item.ImagePath = "/images/" + fileName;
            }

            _context.LostItems.Add(item);
            _context.SaveChanges();
            return RedirectToAction("Lost");
        }

        //
        [HttpPost]
        public IActionResult Found_Items(FoundItem item, IFormFile ImageFile)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(u => u.Email == HttpContext.Session.GetString("UserEmail"));
            item.UserId = user!.UserId;
            item.Status = "active";
            item.CreatedAt = DateTime.Now;

            if (ImageFile != null)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                    ImageFile.CopyTo(stream);
                item.ImagePath = "/images/" + fileName;
            }

            _context.FoundItems.Add(item);
            _context.SaveChanges();
            return RedirectToAction("Found");
        }

        // ---------------- RESOLVE ----------------
        [HttpPost]
        public IActionResult ResolveLost(int id)
        {
            if (!IsLoggedIn()) 
                return RedirectToAction("Login");

            var currentUserId = HttpContext.Session.GetInt32("UserId");
            var item = _context.LostItems.FirstOrDefault(x => x.LostId == id && x.UserId == currentUserId);

            if (item != null)
            {
                item.Status = "resolved";
                _context.SaveChanges();
            }

            return RedirectToAction("Lost");
        }

        [HttpPost]
        public IActionResult ResolveFound(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login");

            var currentUserId = HttpContext.Session.GetInt32("UserId");
            var item = _context.FoundItems.FirstOrDefault(x => x.FoundId == id && x.UserId == currentUserId);

            if (item != null)
            {
                item.Status = "resolved";
                _context.SaveChanges();
            }

            return RedirectToAction("Found");
        }

        // ---------------- ERROR ----------------
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}