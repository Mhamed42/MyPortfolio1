using Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Owner>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<ProtfolioItem>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            ////odelBuilder.Entity<Adress>().Property(x=> x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Owner>().HasData(new Owner()
            {

                Id = Guid.NewGuid(),
                Avatar = "avatar.jpg",
                FullName = "Mohamed Khaild",
                Profile = "Microsft MVP "


            });
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<ProtfolioItem> ProtfolioItems { get; set; }
    }
}
