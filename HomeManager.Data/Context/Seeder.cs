using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models;
using Type = HomeManager.Models.Type;
using Microsoft.AspNetCore.Identity;

namespace HomeManager.Data.Context
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            SeedRole(modelBuilder);
            SeedUser(modelBuilder);
            SeedUserRole(modelBuilder);
            SeedStatus(modelBuilder);
            SeedType(modelBuilder);
            SeedCategory(modelBuilder);
        }

        public static void SeedRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.Parse("898DA44C-F5C4-45A4-7236-08D8C9FA7C8F"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "cc4e0669-e7ef-4edc-83d7-977ef284d407"
                },
                new Role
                {
                    Id = Guid.Parse("C50E0C00-BF4A-4E6A-8B06-08D8CA5D9E57"),
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "896e6ba7-5b4f-4231-bcc7-34aba5ca1e57"
                },
                new Role
                {
                    Id = Guid.Parse("626E5439-AC0E-423F-F10A-08D8CABAFA0B"),
                    Name = "Test",
                    NormalizedName = "TEST",
                    ConcurrencyStamp = "714a5239-a8a3-4b42-af07-033481bd81e0"
                }
                );
        }

        public static void SeedUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("7084A204-330E-4B8E-0788-08D8D0E3F5D6"),
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "Admin@Admin.Admin",
                    NormalizedEmail = "ADMIN@ADMIN.ADMIN",
                    Name = "Admin",
                    Lastname = "Admin",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEJLDpQEhYHywkPUimXOLlP6w24hXDuQdI2wtLcSKIB0K3BPmeFV+nzAaNgRRU2eozA==",
                    SecurityStamp = "RRF4WT3IKFEA5BLYKDY5UJNCIAGATOAP",
                    ConcurrencyStamp = "dc8cb136-9f75-46ee-8cae-72da0e35ff89",
                    Darkmode = true,
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                },
            new User
            {
                Id = Guid.Parse("1C30ADD5-C7A9-48E9-6BEB-08D8C9D5DC9C"),
                UserName = "Francesco",
                NormalizedUserName = "FRANCESCO",
                Email = "Francesco.Aepler@gmail.com",
                NormalizedEmail = "FRANCESCO.AEPLER@GMAIL.COM",
                Name = "Francesco",
                Lastname = "Aepler",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAENCkeX4zTaT+Tre5hnrmc1oMzq420b8/GcdRhtRgWXknIW9VmEdemaVj0SVLTxJERA==",
                SecurityStamp = "YK7VQDBLK2PUOJNEK7YKOW7NQDH7EDYO",
                ConcurrencyStamp = "22ac1596-f950-40cc-ad84-92df87f8d892",
                Darkmode = true,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            },

            new User
            {
                Id = Guid.Parse("C7E66C25-BB5D-41F2-C762-08D8CC11B158"),
                UserName = "Ole",
                NormalizedUserName = "OLE",
                Email = "ole@admin.gov",
                NormalizedEmail = "OLE@ADMIN.GOV",
                Name = "Ole",
                Lastname = "Eggersmann",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEHTM1p5KXcvGwKk4muG28dmLnhAgR3spQVXORsKEw+IN36bupGX27DhsTNrwIymmQg==",
                SecurityStamp = "VTPKISZMI2WKD6GNEHR223NRWDHGYRX6",
                ConcurrencyStamp = "05c6a9ba-80b0-4ee6-ab88-258cc2edbf09",
                Darkmode = true,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            });
        }

        public static void SeedUserRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    UserId = Guid.Parse("7084A204-330E-4B8E-0788-08D8D0E3F5D6"),
                    RoleId = Guid.Parse("898DA44C-F5C4-45A4-7236-08D8C9FA7C8F")
                },
            new IdentityUserRole<Guid>
            {
                UserId = Guid.Parse("1C30ADD5-C7A9-48E9-6BEB-08D8C9D5DC9C"),
                RoleId = Guid.Parse("898DA44C-F5C4-45A4-7236-08D8C9FA7C8F")
            },

            new IdentityUserRole<Guid>
            {
                UserId = Guid.Parse("C7E66C25-BB5D-41F2-C762-08D8CC11B158"),
                RoleId = Guid.Parse("898DA44C-F5C4-45A4-7236-08D8C9FA7C8F")
            });
        }

        public static void SeedStatus(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
            new Status
            {
                Id = 1,
                Name = "Paid",
                EndPoint = true
            },
            new Status
            {
                Id = 2,
                Name = "Received",
                EndPoint = true
            },
            new Status
            {
                Id = 3,
                Name = "Pending"
            },
            new Status
            {
                Id = 4,
                Name = "Fictitious"
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
                EndTaxType = "Net",
                ExtraInput = new string[] { "Extra_Amount", "TaxList" },
                fk_StatusId = 2
            },
            new Type
            {
                Id = 2,
                Name = "Monthly Expens",
                EndTaxType = "Gross",
                Debit = true,
                fk_StatusId = 1
            },
            new Type
            {
                Id = 3,
                Name = "Expenditure",
                EndTaxType = "Gross",
                Debit = true,
                ExtraInput = new string[] { "Extra_Amount" },
                fk_StatusId = 1
            },
            new Type
            {
                Id = 4,
                Name = "Earnings",
                EndTaxType = "Net",
                ExtraInput = new string[] { "Extra_Amount" },
                fk_StatusId = 2
            });
        }

        public static void SeedCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Living"
            },
            new Category
            {
                Id = 2,
                Name = "Groceries"
            },
            new Category
            {
                Id = 3,
                Name = "Leisure"
            },
            new Category
            {
                Id = 4,
                Name = "Mobility"
            },
            new Category
            {
                Id = 5,
                Name = "Insurance"
            },
            new Category
            {
                Id = 6,
                Name = "Loans"
            },
            new Category
            {
                Id = 7,
                Name = "Saving"
            }
            );
        }
    }
}
