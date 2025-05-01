using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System.Security.Claims;

namespace BookStore_App_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class bookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public bookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> AddBook(BookModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Validation Failed",
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var result = await _bookService.AddBook(userId,model);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "Book registration failed" });
                }
                return Ok(new { success = true, message = "Book Registered Successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = "Book registration failed" });
            }
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _bookService.GetAllBooks();
                if (books == null || !books.Any())
                {
                    return NotFound(new { success = false, message = "No books found" });
                }
                return Ok(new { success = true, message = "Books Found Successfully", data = books });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = "Error retrieving books" });
            }
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("{bookId}")]
        public IActionResult GetBookById(int bookId)
        {
            try
            {
                var book = _bookService.GetBookById(bookId);
                if (book == null)
                {
                    return NotFound(new { success = false, message = "Book not found" });
                }
                return Ok(new { success = true, message = "Book Found Successfully", data = book });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = "Error retrieving book" });
            }
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("search")]
        public IActionResult SearchBooks(string searchText)
        {
            try
            {
                var books = _bookService.SearchBooks(searchText);
                if (books == null || !books.Any())
                {
                    return NotFound(new { success = false, message = "No books found" });
                }
                return Ok(new { success = true, message = "Books Found Successfully", data = books });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = "Error retrieving books" });
            }
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("sort")]
        public IActionResult SortBooks(bool sortBy)
        {
            try
            {
                var books = _bookService.SortBooks(sortBy);
                if (books == null || !books.Any())
                {
                    return NotFound(new { success = false, message = "No books found" });
                }
                return Ok(new { success = true, message = "Books Found Successfully", data = books });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = "Error retrieving books" });
            }
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("pagination")]
        public IActionResult PaginateBooks(int pageNumber)
        {
            try
            {
                var books = _bookService.GetBooksByPageNumber(pageNumber);
                if (books == null || !books.Any())
                {
                    return NotFound(new { success = false, message = "No books found" });
                }
                return Ok(new { success = true, message = "Books Found Successfully", data = books });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = "Error retrieving books" });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook(int bookId, BookModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Validation Failed",
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }
                var result = await _bookService.UpdateBook(bookId, model);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "Book update failed" });
                }
                return Ok(new { success = true, message = "Book Updated Successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = "Book update failed" });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{bookId}")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                var result = _bookService.DeleteBook(bookId);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "Book deletion failed" });
                }
                return Ok(new { success = true, message = "Book Deleted Successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false, message = "Book deletion failed" });
            }
        }

    }
}
