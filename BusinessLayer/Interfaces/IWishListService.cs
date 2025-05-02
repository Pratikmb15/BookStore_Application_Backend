using ModelLayer;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IWishListService
    {
        public Task<bool> AddToWishList(int userId, WishListItemModel item);
        public Task<bool> RemoveFromWishList(int userId, int wishListId);
        public List<WishListItem> GetAllWishListItems(int userId);
        public WishListItem GetWishListItemById(int userId, int wishListId);
    }
}
