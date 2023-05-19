using Microsoft.EntityFrameworkCore;
using PruebaAranda.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaAranda.Infrastructure.Persistence
{
    public class ProductoDbContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public ProductoDbContext(DbContextOptions<ProductoDbContext> options) : base(options)
        {
        }

    }
}
