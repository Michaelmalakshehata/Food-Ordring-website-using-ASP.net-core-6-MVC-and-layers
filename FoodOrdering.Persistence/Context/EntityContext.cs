using FoodOredering.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrdering.Persistence.Context
{
    public class EntityContext : IdentityDbContext<ApplicationUser>
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {

        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<FinishedOrders> FinishedOrders { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Menus>().HasOne(p => p.Categories).WithMany(p => p.Menus).HasForeignKey(p => p.CategoryId).HasConstraintName("CategoriesMenus");
            builder.Entity<Orders>().HasOne(p => p.User).WithMany(p => p.orders).HasForeignKey(p => p.UserId).HasConstraintName("UserCarts");
            builder.Entity<Cart>().HasOne(p => p.User).WithMany(p => p.Carts).HasForeignKey(p => p.UserId).HasConstraintName("UserOrders");


            base.OnModelCreating(builder);
        }

    }
}
