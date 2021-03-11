using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using Type = HomeManager.Models.Entities.Finance.Type;
using Microsoft.AspNetCore.Identity;
using HomeManager.Models.Enums;

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
                    FirstName = "Admin",
                    LastName = "Admin",
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
                FirstName = "Francesco",
                LastName = "Aepler",
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
                FirstName = "Ole",
                LastName = "Eggersmann",
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
                Id = Guid.Parse("58AE656B-0B17-4EED-AEEB-EEB660DB266B"),
                Name = "Paid",
                EndPoint = true
            },
            new Status
            {
                Id = Guid.Parse("641DDE1B-2959-47CA-BBC6-D0979FF2BC14"),
                Name = "Received",
                EndPoint = true
            },
            new Status
            {
                Id = Guid.Parse("C04F9F24-36C9-4569-86ED-3AA4E51278BE"),
                Name = "Pending"
            },
            new Status
            {
                Id = Guid.Parse("26425888-2203-422D-8CF1-44400B5F9459"),
                Name = "Fictitious"
            }
            );
        }

        public static void SeedType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Type>().HasData(
            new Type
            {
                Id = Guid.Parse("66B2ECFA-915D-4CA1-AF3F-E5C8602D1C23"),
                Name = "Salary",
                EndTaxType = PaymentTaxType.Net,
                TransactionType = PaymentTransactionType.Deposit,
                ExtraInput = new string[] { Convert.ToString((int)PaymentExtraInput.ExtraCost), Convert.ToString((int)PaymentExtraInput.DetailedTax) },
                fk_StatusId = Guid.Parse("641DDE1B-2959-47CA-BBC6-D0979FF2BC14")
            },
            new Type
            {
                Id = Guid.Parse("6B423C70-1EDD-4988-9CC4-BD4B77EEF7E0"),
                Name = "Monthly Expens",
                EndTaxType = PaymentTaxType.Gross,
                DefaultTaxRate = 19,
                TransactionType = PaymentTransactionType.Debit,
                ExtraInput = new string[] { Convert.ToString((int)PaymentExtraInput.Category)},
                Repeating = true,
                fk_StatusId = Guid.Parse("58AE656B-0B17-4EED-AEEB-EEB660DB266B")
            },
            new Type
            {
                Id = Guid.Parse("93399D01-9C77-483D-BD70-964898A7D875"),
                Name = "Expenditure",
                EndTaxType = PaymentTaxType.Gross,
                TransactionType = PaymentTransactionType.Debit,
                DefaultTaxRate = 19,
                ExtraInput = new string[] { Convert.ToString((int)PaymentExtraInput.ExtraCost), Convert.ToString((int)PaymentExtraInput.Category), Convert.ToString((int)PaymentExtraInput.Warranty) },
                fk_StatusId = Guid.Parse("58AE656B-0B17-4EED-AEEB-EEB660DB266B")
            },
            new Type
            {
                Id = Guid.Parse("2CDC950B-C30D-4CBE-A0CA-0965F86CB9AC"),
                Name = "Earnings",
                EndTaxType = PaymentTaxType.Net,
                TransactionType = PaymentTransactionType.Deposit,
                ExtraInput = new string[] { Convert.ToString((int)PaymentExtraInput.ExtraCost), Convert.ToString((int)PaymentExtraInput.Category) },
                fk_StatusId = Guid.Parse("641DDE1B-2959-47CA-BBC6-D0979FF2BC14")
            });
        }

        public static void SeedCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = Guid.Parse("65DF89EA-DF64-4074-A36C-6B15D1F16BAF"),
                Name = "Living"
            },
            new Category
            {
                Id = Guid.Parse("05E8B5CB-E640-4A20-8C4A-258626C90A84"),
                Name = "Groceries"
            },
            new Category
            {
                Id = Guid.Parse("E9187C61-0682-40D9-8A78-8098BFD5F401"),
                Name = "Leisure"
            },
            new Category
            {
                Id = Guid.Parse("6A7D9B47-7967-497F-8276-735D0A46CBCB"),
                Name = "Mobility"
            },
            new Category
            {
                Id = Guid.Parse("327B058F-4182-4CA1-9C1B-CFA46E3F59E0"),
                Name = "Insurance"
            },
            new Category
            {
                Id = Guid.Parse("DE9F70B1-E498-4E6F-ABA8-B290A7F91111"),
                Name = "Loans"
            },
            new Category
            {
                Id = Guid.Parse("1E02AECE-87A5-43A4-9407-232718500157"),
                Name = "Saving"
            }
            );
        }
    }
}
