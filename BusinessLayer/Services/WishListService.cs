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
    public class WishListService : IWishListService
    {
        private readonly IWishListRepoService _repoService;
        public WishListService(IWishListRepoService repoService)
        {
            _repoService = repoService;
        }
        public async Task<bool> AddToWishList(int userId, WishListItemModel item)
        {
            return await _repoService.AddToWishList(userId, item);
        }

        public List<WishListItem> GetAllWishListItems(int userId)
        {
            return _repoService.GetAllWishListItems(userId);
        }

        public WishListItem GetWishListItemById(int userId, int wishListId)
        {
            return _repoService.GetWishListItemById(userId, wishListId);
        }

        public Task<bool> RemoveFromWishList(int userId, int wishListId)
        {
           return _repoService.RemoveFromWishList(userId, wishListId);
        }
    }
}
