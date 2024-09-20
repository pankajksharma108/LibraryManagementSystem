using LibraryManagementSystem.Common;
using LibraryManagementSystem.Entity;
using LibraryManagementSystem.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IConfiguration _config;

        public BookController(IBookService bookService, IConfiguration config)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _config = config;
        }

        public async Task<IActionResult> GetBookDetails()
        {
            IEnumerable<BookEntity> response = await _bookService.GetBookDetails();
            //if (TempData["ResponseId"] != null)
            //{
            //    ViewBag.ResponseId = TempData["ResponseId"];

            //    if (TempData["Message"] != null)
            //    {
            //        ViewBag.Message = TempData["Message"];
            //    }

            //    if (TempData["WarningMessage"] != null)
            //    {
            //        ViewBag.WarningMessage = TempData["WarningMessage"];
            //    }
            //}
            return View(response);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBooks()
        {
            return View();
        }

        public async Task<IActionResult> DeleteBookById(int BookId)
        {
            try
            {
                var response = await _bookService.DeleteBookById(BookId);
                TempData["ResponseId"] = response.ResponseId;
                TempData["WarningMessage"] = response.Message;
                return RedirectToAction("GetBookDetails", "Book");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<IActionResult> InsertBookDetails(BookRequest bookRequest)
        {
            try
            {
                var response = await _bookService.InsertBookDetails(bookRequest);
                TempData["ResponseId"] = response.ResponseId;
                TempData["Message"] = response.Message;
                return RedirectToAction("GetBookDetails", "Book");
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public async Task<IActionResult> GetBookById(int BookId)
        {
            try
            {
                if (BookId != null)
                {
                    BookResponseEntity response = await _bookService.GetBookById(BookId);
                    return View(response);
                }
                else
                {
                    return RedirectToAction("GetBookDetails", "Book");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> UpdateBookDetails(BookRequest bookRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _bookService.UpdateBookDetails(bookRequest);
                    TempData["ResponseId"] = response.ResponseId;
                    TempData["Message"] = response.Message;
                    return RedirectToAction("GetBookDetails", "Book");
                }
                return View(bookRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
