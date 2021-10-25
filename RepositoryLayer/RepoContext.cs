using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class RepoContext : DbContext
    {
       
       
        public DbSet<CounterDetailsEntity> CounterDetails { get; set; }
        public virtual DbSet<VendorDetailsEntity>  VendorDetails { get; set; }
        public DbSet<UsersEntity> Users { get; set; }
        public DbSet<MenuEntity> Menu { get; set; }
        
        


        public DbSet<BookingEntity> Booking { get; set; }
    }
}
