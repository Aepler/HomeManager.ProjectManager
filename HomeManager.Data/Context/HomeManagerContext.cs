using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using HomeManager.Models;

namespace HomeManager.Data
{
    public class HomeManagerContext : DbContext
    {
        public HomeManagerContext(DbContextOptions<HomeManagerContext> options) : base(options)
        {
        }

        public DbSet<Salary> Salaries { get; set; }
        public DbSet<MonthlyExpens> MonthlyExpenses { get; set; }
        public DbSet<Expenditure> Expenditures { get; set; }
        public DbSet<Earning> Earnings { get; set; }
        public DbSet<Cash> Cashes { get; set; }
        public DbSet<Category_Entry> Category_Entries { get; set; }
        public DbSet<Status_Entry> Status_Entries { get; set; }
        public DbSet<MonthlyExpenses_Entry> MonthlyExpenses_Entries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Salary>().ToTable("Salary");
            modelBuilder.Entity<MonthlyExpens>().ToTable("MonthlyExpens");
            modelBuilder.Entity<Expenditure>().ToTable("Expenditure");
            modelBuilder.Entity<Earning>().ToTable("Earning");
            modelBuilder.Entity<Cash>().ToTable("Cash");
            modelBuilder.Entity<Category_Entry>().ToTable("Category_Entry");
            modelBuilder.Entity<Status_Entry>().ToTable("Status_Entry");
            modelBuilder.Entity<MonthlyExpenses_Entry>().ToTable("MonthlyExpenses_Entry");
        }
    }
}
