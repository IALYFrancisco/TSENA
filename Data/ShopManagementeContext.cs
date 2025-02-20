using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TSENA.Models;

    public class ShopManagementeContext : DbContext
    {
        public ShopManagementeContext (DbContextOptions<ShopManagementeContext> options)
            : base(options)
        {
        }

        public DbSet<TSENA.Models.Shop> Shop { get; set; } = default!;

    }
