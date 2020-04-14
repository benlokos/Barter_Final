using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Project.Models;


namespace Project.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
          : base(options)
        {

        }

        public DbSet<ItemModel> Items { set; get; }
        public DbSet<TraderModel> Traders { set; get; }
        public DbSet<AdressModel> Locations { set; get; }
        public DbSet<ReviewModel> Reviews { set; get; }
        public DbSet<TransactionModel> Transactions { set; get; }
        public DbSet<TagModel> Tags { set; get; }

    }
}
