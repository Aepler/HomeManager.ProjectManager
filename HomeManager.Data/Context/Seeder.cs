using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using Type = HomeManager.Models.Type;

namespace HomeManager.Data.Context
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            SeedStatus(modelBuilder);
            SeedType(modelBuilder);
            SeedCategory(modelBuilder);
        }

        public static void SeedStatus(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
            new Status
            {
                Id = 1,
                Name = "Paid",
                Deleted = false
            },
            new Status
            {
                Id = 2,
                Name = "Received",
                Deleted = false
            },
            new Status
            {
                Id = 3,
                Name = "Pending",
                Deleted = false
            },
            new Status
            {
                Id = 4,
                Name = "Fictitious",
                Deleted = false
            }
            );
        }

        public static void SeedType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Type>().HasData(
            new Type
            {
                Id = 1,
                Name = "Salary",
                TaxType = "Net",
                Deleted = false
            },
            new Type
            {
                Id = 2,
                Name = "Monthly Expens",
                TaxType = "Gross",
                Deleted = false
            },
            new Type
            {
                Id = 3,
                Name = "Expenditure",
                TaxType = "Gross",
                Deleted = false
            },
            new Type
            {
                Id = 4,
                Name = "Earnings",
                TaxType = "Gross",
                Deleted = false
            },
            new Type
            {
                Id = 5,
                Name = "Start Balance",
                TaxType = "None",
                Deleted = false
            }
            );
        }

        public static void SeedCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Living",
                Deleted = false
            },
            new Category
            {
                Id = 2,
                Name = "Groceries",
                Deleted = false
            },
            new Category
            {
                Id = 3,
                Name = "Leisure",
                Deleted = false
            },
            new Category
            {
                Id = 4,
                Name = "Mobility",
                Deleted = false
            },
            new Category
            {
                Id = 5,
                Name = "Insurance",
                Deleted = false
            },
            new Category
            {
                Id = 6,
                Name = "Loans",
                Deleted = false
            },
            new Category
            {
                Id = 7,
                Name = "Saving",
                Deleted = false
            }
            );
        }
    }
}
