using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Diplom.Models;

namespace Diplom.Models
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<ApplicationBuh> ApplicationBuh { get; set; }

        public DbSet<ApplicationUr> ApplicationUr { get; set; }

        public DbSet<OrderBuh> OrderBuh { get; set; }
        public DbSet<UserRole> Roles { get; set; }
   

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //добавляем тестовые данные в бд
            

            // добавляем роли
            string adminRoleName = "admin";
            string userRoleName = "user";
            string adminEmail = "admin@admin.com";
            string adminPassword = "123456";
            UserRole adminRole = new UserRole { Id = 1, Name = adminRoleName };
            UserRole userRole = new UserRole { Id = 2, Name = userRoleName };
            UserRole bannedRole = new UserRole { Id = 3, Name = "banned" };
            //User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };
            User adminUser = new User { ID = 1, NAME = "admin", EMAIL = adminEmail, PASSWORD = adminPassword, RoleId = adminRole.Id };
            User IVAN = new User { ID = 2, NAME = "IVAN", SURNAME="PUPKIN", PHONE="12321232123", EMAIL = "IVANEmail", PASSWORD = adminPassword, RoleId = userRole.Id };
            modelBuilder.Entity<UserRole>().HasData(new UserRole[] { adminRole, userRole, bannedRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser, IVAN });
            base.OnModelCreating(modelBuilder);
        }

        //public DbSet<Diplom.Models.ApplicationBuh> ApplicationBuhs { get; set; }
    }
}
