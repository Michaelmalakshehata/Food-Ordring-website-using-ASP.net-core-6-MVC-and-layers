using FoodOrdering.Application.Features.CartFeatures.CartCreate;
using FoodOrdering.Application.Features.OrderFeatures.OrderSearch;
using FoodOrdering.Application.Repositories;
using FoodOrdering.Persistence.Context;
using FoodOredering.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Persistence.Repositories
{
    public class ServiceShowMenu : IServiceShowMenu
    {
        private readonly EntityContext context;
        private readonly IGenericRepository<Menus> genericRepository;
        public ServiceShowMenu(IGenericRepository<Menus> genericRepository, EntityContext context)
        {
            this.genericRepository = genericRepository;
            this.context = context;
        }
        public async Task<List<CartViewModel>?> GetlastMenus()
        {
            try
            {
                List<Menus> list = await context.Menus.Where(e => e.IsDeleted == false).Include(e => e.Categories).Take(9).ToListAsync();
                if (list is null)
                {
                    return new List<CartViewModel>();
                }
                List<CartViewModel> cart = new List<CartViewModel>();

                foreach (var item in list)
                {
                    cart.Add(new CartViewModel
                    {
                        imgpath = item.imgpath,
                        Price = item.Price,
                        Ordername = item.Name,
                        MenuId = item.Id,
                        CategoryId = item.CategoryId,
                        CategoryName = item.Categories.Name
                    });
                }
                return cart;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<Menus>?> Takefristthreemenu()
        {
            try
            {
                return await context.Menus.Where(e => e.IsDeleted == false).Take(3).ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<CartViewModel?> GetByid(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new CartViewModel();
                }
                var result = await genericRepository.GetById(id);
                if (result is null)
                {
                    return new CartViewModel();
                }
                CartViewModel menuUpdateViewModel = new CartViewModel()
                {
                    Ordername = result.Name,
                    MenuId = result.Id,
                    Price = result.Price,
                    imgpath = result.imgpath,
                    CategoryId = result.CategoryId
                };
                return menuUpdateViewModel;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CartViewModel>?> GetMenuByCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new List<CartViewModel>();
                }
                List<Menus> menus = await context.Menus.Where(e => e.IsDeleted == false).Where(s => s.CategoryId == id).ToListAsync();
                if (menus is null)
                {
                    return new List<CartViewModel>();
                }
                List<CartViewModel> cart = new List<CartViewModel>();
                foreach (var item in menus)
                {
                    cart.Add(new CartViewModel
                    {
                        imgpath = item.imgpath,
                        Price = item.Price,
                        Ordername = item.Name,
                        MenuId = item.Id,
                        CategoryId = item.CategoryId
                    });
                }
                return cart;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CartViewModel>?> SearchMenu(SearchViewModel searchViewModel)
        {
            try
            {
                if (searchViewModel.name is null)
                {
                    return new List<CartViewModel>();
                }
                List<Menus> list = await context.Menus.Where(o => o.IsDeleted == false).Where(o => o.Name.Contains(searchViewModel.name)).Include(e => e.Categories).ToListAsync();
                if (list is null)
                {
                    return new List<CartViewModel>();
                }
                List<CartViewModel> cart = new List<CartViewModel>();
                foreach (var item in list)
                {
                    cart.Add(new CartViewModel
                    {
                        imgpath = item.imgpath,
                        Price = item.Price,
                        Ordername = item.Name,
                        MenuId = item.Id,
                        CategoryId = item.CategoryId,
                        CategoryName = item.Categories.Name
                    });
                }
                return cart;
            }
            catch
            {
                throw;
            }
        }
    }
}
