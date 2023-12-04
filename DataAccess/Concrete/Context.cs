using Core.Concrete.Entities;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class Context: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=mertozgen.com.tr\\MSSQLSERVER2019;User Id=mertozge_gutenberguser;" +
                "Password=ah11!v11V;TrustServerCertificate=Yes;Initial Catalog=mertozge_gutenberg;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<LibraryItem> LibraryItems { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
