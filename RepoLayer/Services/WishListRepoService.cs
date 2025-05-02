using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Services
{
    public class WishListRepoService: IWishListRepoService
    {
        private readonly AppDbContext _context;
        private readonly IBookRepoService _bookRepoService;
        public WishListRepoService(AppDbContext context, IBookRepoService bookRepoService)
        {
            _context = context;
            _bookRepoService = bookRepoService;
        }
        public async Task<bool> AddToWishList(int userId, WishListItemModel item)
        {
            var isExist = IsBookInWishList(userId, item.bookId);
            if (isExist)
            {
                throw new ArgumentException("Book already exists in wishlist");
            }
            var book = _bookRepoService.GetBookById(item.bookId);
            if (book == null)
            {
                throw new ArgumentException($"No book found with id = {item.bookId}");
            }
            var wishItem = new WishListItem() { bookId = item.bookId, userId = userId };
            _context.WhishList.Add(wishItem);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveFromWishList(int userId, int wishListId)
        {
            if (wishListId <= 0)
            {
                throw new ArgumentException("Invalid WishList ID");
            }
            var wishListItem = _context.WhishList.FirstOrDefault(x => x.whishListId == wishListId && x.userId == userId);
            if (wishListItem == null)
            {
                throw new Exception("Item not found in wishlist");
            }
            _context.WhishList.Remove(wishListItem);
            await _context.SaveChangesAsync();
            return true;
        }
        public List<WishListItem> GetAllWishListItems(int userId)
        {
            var wishListItems = _context.WhishList.Include(i => i.Book).Where(x => x.userId == userId).ToList();
            if (wishListItems == null || wishListItems.Count == 0)
            {
                throw new Exception("No items found in wishlist");
            }
            return wishListItems;
        }
        public WishListItem GetWishListItemById(int userId, int wishListId)
        {
            var wishListItem = _context.WhishList.Include(i => i.Book).FirstOrDefault(x => x.whishListId == wishListId && x.userId == userId);
            if (wishListItem == null)
            {
                throw new Exception("No items found in wishlist");
            }
            return wishListItem;
        }
        public bool IsBookInWishList(int userId, int bookId)
        {
            var wishListItem = _context.WhishList.FirstOrDefault(x => x.userId == userId && x.bookId == bookId);
            if (wishListItem != null)
            {
                return true;
            }
            return false;

        }
    }
}
