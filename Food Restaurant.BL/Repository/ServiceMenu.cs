using Food_Restaurant.BL.IRepositories;
using Food_Restaurant.BL.Repositories;
using Food_Restaurant.BL.ViewModels;
using Food_Restaurant.DAL;
using Food_Restaurant.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Restaurant.BL.Repository
{
    public class ServiceMenu : IServiceMenu
    {
        private readonly EntityContext context;
        private readonly IGenericRepository<Menus> genericRepository;
        private readonly IHostingEnvironment _hosting;
        public ServiceMenu(IGenericRepository<Menus> genericRepository, EntityContext context, IHostingEnvironment hosting)
        {
            this.genericRepository = genericRepository;
            this.context = context;
            this._hosting=hosting;
        }
        public async Task<Menus?> add(MenuViewModel menuViewModel)
        {
            if (menuViewModel != null)
            {
                string filename = string.Empty;
                if(menuViewModel.File!=null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, ("uploaded_img"));
                    filename=menuViewModel.File.FileName;
                    string fullpath=Path.Combine(uploads, filename);
                    menuViewModel.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                }
                Menus menu = new Menus()
                {
                    Name = menuViewModel.Name,
                    Price = menuViewModel.Price,
                    imgpath=filename,
                    CategoryId=menuViewModel.CategoryId
                };
                var obj = await genericRepository.add(menu);
                return obj;
            }
            return null;
        }
        
        public async Task<int> Delete(int id)
        {
            if (id > 0)
            {
                int result = await genericRepository.delete(id);
                return result;
            }
            return 0;
        }

        public async Task<List<Menus>?> GetalldeletedMenus()
        {
            List<Menus> list = await genericRepository.GetallSoftDeleted();
            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
           
        }

        public async Task<List<Menus>?> GetallMenus()
        {
            List<Menus> list = await context.Menus.Where(e=>e.IsDeleted==false).Include(e => e.Categories).ToListAsync();
            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        public async Task<MenuUpdateViewModel?> GetByid(int id)
        {
            if (id > 0)
            {
                var result = await genericRepository.GetById(id);
                if (result != null)
                {
                    MenuUpdateViewModel menuUpdateViewModel = new MenuUpdateViewModel()
                    {
                        Name = result.Name,
                        Id = result.Id,
                        Price = result.Price,
                        imgpath=result.imgpath,
                        CategoryId=result.CategoryId
                    };
                    return menuUpdateViewModel;
                }

            }
            return null;
        }

        public async Task<List<Menus>?> GetMenuByCategory(int id)
        {
            if(id!=0)
            {
                return await context.Menus.Where(s=>s.IsDeleted==false).Where(s => s.CategoryId == id).ToListAsync();
            }
            return null;
        }

        public async Task<int> RestoreMenu(int id)
        {
            if (id > 0)
            {
                int result = await genericRepository.Restordeleted(id);
                return result;
            }
            return 0;
        }

        public async Task Update(MenuUpdateViewModel menuUpdateViewModel)
        {
            if (menuUpdateViewModel != null)
            {
                string filename = string.Empty;
                Menus? oldobj = await context.Menus.FindAsync(menuUpdateViewModel.Id);
                if (menuUpdateViewModel.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, ("uploaded_img"));
                    filename = menuUpdateViewModel.File.FileName;
                    string fullpath = Path.Combine(uploads, filename);
                    if (oldobj?.imgpath != null)
                    {
                        string oldfilename = oldobj.imgpath;
                        string oldfullpath=Path.Combine(uploads, oldfilename);
                        System.IO.File.Delete(oldfullpath);
                    }
                    menuUpdateViewModel.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                }
                else
                {
                    filename = oldobj.imgpath;
                }
                if(context.Menus.Where(s=>s.Id !=oldobj.Id).Where(s=>s.Name==menuUpdateViewModel.Name).Count()>0)
                {
                    throw new ArgumentNullException();
                }
                oldobj.Name = menuUpdateViewModel.Name;
                oldobj.Price = menuUpdateViewModel.Price;
                oldobj.imgpath = filename;
                oldobj.CategoryId = menuUpdateViewModel.CategoryId;
              await   genericRepository.update(oldobj);
            }
        }
    }
}
