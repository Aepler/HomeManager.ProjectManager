using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeManager.Data.Migrations
{
    public partial class HomeManagerInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndPoint = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_AspNetUsers_fk_UserId",
                        column: x => x.fk_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndTaxType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Debit = table.Column<bool>(type: "bit", nullable: false),
                    ExtraInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fk_StatusId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Types_Statuses_fk_StatusId",
                        column: x => x.fk_StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payment_Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Invoice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fk_TypeId = table.Column<int>(type: "int", nullable: false),
                    fk_CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Templates_AspNetUsers_fk_UserId",
                        column: x => x.fk_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_Templates_Categories_fk_CategoryId",
                        column: x => x.fk_CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_Templates_Types_fk_TypeId",
                        column: x => x.fk_TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_Extra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_Tax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount_Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount_Gross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount_Net = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount_Extra = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount_TaxList = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Invoice = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fk_TypeId = table.Column<int>(type: "int", nullable: false),
                    fk_CategoryId = table.Column<int>(type: "int", nullable: false),
                    fk_StatusId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_fk_UserId",
                        column: x => x.fk_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Categories_fk_CategoryId",
                        column: x => x.fk_CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Statuses_fk_StatusId",
                        column: x => x.fk_StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Types_fk_TypeId",
                        column: x => x.fk_TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("898da44c-f5c4-45a4-7236-08d8c9fa7c8f"), "cc4e0669-e7ef-4edc-83d7-977ef284d407", "Admin", "ADMIN" },
                    { new Guid("c50e0c00-bf4a-4e6a-8b06-08d8ca5d9e57"), "896e6ba7-5b4f-4231-bcc7-34aba5ca1e57", "User", "USER" },
                    { new Guid("626e5439-ac0e-423f-f10a-08d8cabafa0b"), "714a5239-a8a3-4b42-af07-033481bd81e0", "Test", "TEST" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("1c30add5-c7a9-48e9-6beb-08d8c9d5dc9c"), 0, "22ac1596-f950-40cc-ad84-92df87f8d892", "Francesco.Aepler@gmail.com", true, true, null, "FRANCESCO.AEPLER@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAENCkeX4zTaT+Tre5hnrmc1oMzq420b8/GcdRhtRgWXknIW9VmEdemaVj0SVLTxJERA==", null, false, "YK7VQDBLK2PUOJNEK7YKOW7NQDH7EDYO", false, "Admin" },
                    { new Guid("c7e66c25-bb5d-41f2-c762-08d8cc11b158"), 0, "05c6a9ba-80b0-4ee6-ab88-258cc2edbf09", "ole@admin.gov", true, true, null, "OLE@ADMIN.GOV", "OLE", "AQAAAAEAACcQAAAAEHTM1p5KXcvGwKk4muG28dmLnhAgR3spQVXORsKEw+IN36bupGX27DhsTNrwIymmQg==", null, false, "VTPKISZMI2WKD6GNEHR223NRWDHGYRX6", false, "Ole" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Deleted", "DeletedOn", "Name", "fk_UserId" },
                values: new object[,]
                {
                    { 1, false, null, "Living", null },
                    { 2, false, null, "Groceries", null },
                    { 3, false, null, "Leisure", null },
                    { 4, false, null, "Mobility", null },
                    { 5, false, null, "Insurance", null },
                    { 6, false, null, "Loans", null },
                    { 7, false, null, "Saving", null }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Deleted", "DeletedOn", "EndPoint", "Name" },
                values: new object[,]
                {
                    { 1, false, null, true, "Paid" },
                    { 2, false, null, true, "Received" },
                    { 3, false, null, false, "Pending" },
                    { 4, false, null, false, "Fictitious" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("898da44c-f5c4-45a4-7236-08d8c9fa7c8f"), new Guid("1c30add5-c7a9-48e9-6beb-08d8c9d5dc9c") },
                    { new Guid("898da44c-f5c4-45a4-7236-08d8c9fa7c8f"), new Guid("c7e66c25-bb5d-41f2-c762-08d8cc11b158") }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "Debit", "Deleted", "DeletedOn", "EndTaxType", "ExtraInput", "Name", "fk_StatusId" },
                values: new object[,]
                {
                    { 2, true, false, null, "Gross", null, "Monthly Expens", 1 },
                    { 3, true, false, null, "Gross", "Extra_Amount", "Expenditure", 1 },
                    { 1, false, false, null, "Net", "Extra_Amount,TaxList", "Salary", 2 },
                    { 4, false, false, null, "Net", "Extra_Amount", "Earnings", 2 },
                    { 5, false, false, null, "None", null, "Start Balance", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_fk_UserId",
                table: "Categories",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Templates_fk_CategoryId",
                table: "Payment_Templates",
                column: "fk_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Templates_fk_TypeId",
                table: "Payment_Templates",
                column: "fk_TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Templates_fk_UserId",
                table: "Payment_Templates",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_fk_CategoryId",
                table: "Payments",
                column: "fk_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_fk_StatusId",
                table: "Payments",
                column: "fk_StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_fk_TypeId",
                table: "Payments",
                column: "fk_TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_fk_UserId",
                table: "Payments",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Types_fk_StatusId",
                table: "Types",
                column: "fk_StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Payment_Templates");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}
