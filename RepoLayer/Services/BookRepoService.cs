using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Services
{
    public class BookRepoService : IBookRepoService
    {
        private readonly AppDbContext _context;
        public BookRepoService(AppDbContext context)
        {
            _context = context;
        }


        public Task<bool> AddBook(int userId, BookModel model)
        {
            var book = new Book()
            {
                description = model.description,
                discountPrice = model.discountPrice,
                bookImage = model.bookImage,
                userId = userId,
                bookName = model.bookName,
                author = model.author,
                quantity = model.quantity,
                price = model.price,
                createdAtDate = model.createdAtDate,
                updatedAtDate = model.updatedAtDate
            };
            _context.Books.Add(book);
            _context.SaveChangesAsync();
            return Task.FromResult(true);
        }

        public Task<bool> UpdateBook(int bookId, BookModel model)
        {
            var book = _context.Books.FirstOrDefault(b => b.bookId == bookId);
            if (book == null)
            {
                throw new ArgumentException("Book not found");
            }
            book.description = model.description;
            book.discountPrice = model.discountPrice;
            book.bookImage = model.bookImage;
            book.bookName = model.bookName;
            book.author = model.author;
            book.quantity = model.quantity;
            book.price = model.price;
            book.createdAtDate = model.createdAtDate;
            book.updatedAtDate = model.updatedAtDate;
            _context.Books.Update(book);
            _context.SaveChangesAsync();
            return Task.FromResult(true);
        }

        public Book GetBookById(int bookId)
        {
            if (bookId == null)
            {
                throw new ValidationException("Book ID must be greater than zero.");
            }
            var book = _context.Books.FirstOrDefault(b => b.bookId == bookId);
            if (book == null)
            {
                throw new ArgumentException("Book not found");
            }
            return book;
        }

        public List<Book> GetAllBooks()
        {
            var books = _context.Books.ToList();
            return books;
        }

        public bool DeleteBook(int bookId)
        {
            if (bookId == null)
            {
                throw new ValidationException("Book ID must be greater than zero.");
            }
            var book = _context.Books.FirstOrDefault(b => b.bookId == bookId);
            if (book == null)
            {
                throw new ArgumentException("Book not found");
            }
            _context.Books.Remove(book);
            _context.SaveChangesAsync();
            return true;
        }


    }
}
