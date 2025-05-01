using BusinessLayer.Interfaces;
using ModelLayer;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepoService _bookRepoService;
        public BookService(IBookRepoService bookRepoService)
        {
            _bookRepoService = bookRepoService;
        }
        public Task<bool> AddBook(int userId, BookModel model)
        {
            return _bookRepoService.AddBook(userId, model);
        }

        public bool DeleteBook(int bookId)
        {
            return _bookRepoService.DeleteBook(bookId);
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepoService.GetAllBooks();
        }

        public Book GetBookById(int bookId)
        {
            return _bookRepoService.GetBookById(bookId);
        }

        public List<Book> GetBooksByPageNumber(int pageNumber)
        {
            return _bookRepoService.GetBooksByPageNumber(pageNumber);
        }

        public List<Book> SearchBooks(string searchTerm)
        {
            return _bookRepoService.SearchBooks(searchTerm);
        }

        public List<Book> SortBooks(bool asc)
        {
            return _bookRepoService.SortBooks(asc);
        }

        public Task<bool> UpdateBook(int bookId, BookModel model)
        {
            return _bookRepoService.UpdateBook(bookId, model);
        }
    }
}
