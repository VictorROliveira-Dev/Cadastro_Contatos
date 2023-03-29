using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppWebMVC.Models;

namespace AppWebMVC.Data
{
    public class AppWebMVCContext : DbContext
    {
        public AppWebMVCContext (DbContextOptions<AppWebMVCContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; } 
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SalesRecord> SalesRecords { get; set; }
    }
}
