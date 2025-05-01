using ModelLayer;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBookService
    {
        Task<bool> AddBook(int userId,BookModel model);
        Task<bool> UpdateBook(int bookId, BookModel model);
        bool DeleteBook(int bookId);
        public Book GetBookById(int bookId);
        public List<Book> GetAllBooks();
        public List<Book> SearchBooks(string searchTerm);
        public List<Book> SortBooks(bool asc);
        public List<Book> GetBooksByPageNumber(int pageNumber);
    }
}
