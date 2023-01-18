using Libirary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeActions;
using System.Diagnostics;

namespace Libirary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        LibDBContext LibDBContext;
        public HomeController(ILogger<HomeController> logger, LibDBContext _context)
        {
            _logger = logger;
            LibDBContext = _context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var books = LibDBContext.Books.ToList();
            return View(books);
        }
        public IActionResult AddNewbook()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewBook(Book newBook)
        {
            if (ModelState.IsValid)
            {
                newBook.borrowed = 0;
                newBook.availableCopiesToborrow = newBook.copies;
                LibDBContext.Books.Add(newBook);
                LibDBContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(newBook);
        }
        public IActionResult BorrowingSystem()
        {
            List<Book> books = LibDBContext.Books.ToList();
            ViewBag.Model = books;
            return View();
        }
        [HttpPost]
        public IActionResult BorrowingSystem(BorrowingOperation borrowing)
        {
            var book = LibDBContext.Books.Find(borrowing.BSN);
            if (book.availableCopiesToborrow != 0 && book.availableCopiesToborrow - borrowing.numberofCopiestoborrow >= 0)
            {
                book.borrowed += borrowing.numberofCopiestoborrow;
                book.availableCopiesToborrow -= borrowing.numberofCopiestoborrow;
                LibDBContext.SaveChanges();
                TempData["Warning"] = "Successfuly Saved " + borrowing.numberofCopiestoborrow.ToString() + " Copies of Book " +

                    book.bookTitle.ToString() + "Then Total borrowed Copies from this book is " + (book.borrowed).ToString();
            }
            else if(borrowing.numberofCopiestoborrow !=1)
            {
                TempData["Warning"] = "All Coppies are Borrowed Try to reduce Copies You want and try again";
            }
            else
            {
                TempData["Warning"] = "Sorry All Coppies of "+book.bookTitle + "are Borrowed... ";
            }
            List<Book> books = LibDBContext.Books.ToList();
            ViewBag.Model = books;
            return View();
        }
        public IActionResult BackingBooksupSystem()
        {
            List<Book> books = LibDBContext.Books.ToList();
            ViewBag.Model = books;
            return View();
        }
        [HttpPost]
        public IActionResult BackingBooksupSystem(BackingBooksUpOperation backingBooks)
        {
            var book = LibDBContext.Books.Find(backingBooks.BSN);
            if (book.borrowed - backingBooks.numberofCopiestoBackedUp >= 0)
            {
                book.borrowed -= backingBooks.numberofCopiestoBackedUp;
                book.availableCopiesToborrow += backingBooks.numberofCopiestoBackedUp;
                LibDBContext.SaveChanges();
                TempData["Warning"] = backingBooks.numberofCopiestoBackedUp + " Copies Of Book " + book.bookTitle + "have been Successfully Backed Up, Thank You..";
            }
          else  if (book.copies == book.availableCopiesToborrow)
            {
                TempData["Warning"] = "No Copies of " + book.bookTitle + "is needed To Be Backed Up";
            }
            else
            {
                if (backingBooks.numberofCopiestoBackedUp == 1)
                {
                    TempData["Warning"] = "You have Exceeded Copies number of Book  " + book.bookTitle + " that You already have ";
                }
                else
                {
                    TempData["Warning"] = "You have Exceeded Copies number of Book " + book.bookTitle + "that You already have Try To Reduce number of Copies You Want To Back it Up and Try again";

                }
            }
            List<Book> books = LibDBContext.Books.ToList();
            ViewBag.Model = books;
            return View();
        }

        public IActionResult DeleteBook(Guid id)
        {
            var book = LibDBContext.Books.SingleOrDefault(s => s.BSN == id);
            LibDBContext.Books.Remove(book);
            LibDBContext.SaveChanges();

            return RedirectToAction("index");
        }
        private Guid BID;
        public IActionResult EditBook(Guid id)
        {
            var book = LibDBContext.Books.Find(id);
            BID = book.BSN;
            return View(book);
        }
        public IActionResult EditBookSave(Book book)
        {
            if (ModelState.IsValid)
            {
                var _book = LibDBContext.Books.SingleOrDefault(s=>s.BSN== book.BSN);
                if(book.copies>_book.copies)
                {
                    _book.availableCopiesToborrow += book.copies - _book.copies;
                    _book.copies=book.copies;
                    _book.bookTitle = book.bookTitle;
                    _book.autorName=book.autorName;
                    LibDBContext.SaveChanges();
                }
            }
            else
            {
                RedirectToAction("EditBook");
            }
            return RedirectToAction("index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}