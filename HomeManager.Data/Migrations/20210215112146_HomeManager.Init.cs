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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Darkmode = table.Column<bool>(type: "bit", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartBalance = table.Column<double>(type: "float", nullable: false),
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
                name: "CookingIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ammount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fk_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IngredientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CookingIngredients_AspNetUsers_fk_UserId",
                        column: x => x.fk_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CookingIngredients_CookingIngredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "CookingIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CookingRecipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CookingTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    PreapearingTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    Stars = table.Column<byte>(type: "tinyint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Public = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fk_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingRecipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CookingRecipes_AspNetUsers_fk_UserId",
                        column: x => x.fk_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CookingTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fk_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CookingTags_AspNetUsers_fk_UserId",
                        column: x => x.fk_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinanceCategories",
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
                    table.PrimaryKey("PK_FinanceCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceCategories_AspNetUsers_fk_UserId",
                        column: x => x.fk_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinanceStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndPoint = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fk_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceStatuses_AspNetUsers_fk_UserId",
                        column: x => x.fk_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CookingFavorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    fk_RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingFavorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CookingFavorites_AspNetUsers_fk_UserId",
                        column: x => x.fk_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CookingFavorites_CookingRecipes_fk_RecipeId",
                        column: x => x.fk_RecipeId,
                        principalTable: "CookingRecipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CookingIngredientRecipes",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "int", nullable: false),
                    RecipesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingIngredientRecipes", x => new { x.IngredientsId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_CookingIngredientRecipes_CookingIngredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "CookingIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CookingIngredientRecipes_CookingRecipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "CookingRecipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CookingIngredientTags",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingIngredientTags", x => new { x.IngredientsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_CookingIngredientTags_CookingIngredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "CookingIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CookingIngredientTags_CookingTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "CookingTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CookingRecipeTags",
                columns: table => new
                {
                    RecipesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingRecipeTags", x => new { x.RecipesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_CookingRecipeTags_CookingRecipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "CookingRecipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CookingRecipeTags_CookingTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "CookingTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinanceTypes",
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
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fk_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceTypes_AspNetUsers_fk_UserId",
                        column: x => x.fk_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceTypes_FinanceStatuses_fk_StatusId",
                        column: x => x.fk_StatusId,
                        principalTable: "FinanceStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancePaymentTemplates",
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
                    fk_CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancePaymentTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancePaymentTemplates_AspNetUsers_fk_UserId",
                        column: x => x.fk_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancePaymentTemplates_FinanceCategories_fk_CategoryId",
                        column: x => x.fk_CategoryId,
                        principalTable: "FinanceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancePaymentTemplates_FinanceTypes_fk_TypeId",
                        column: x => x.fk_TypeId,
                        principalTable: "FinanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancePayments",
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
                    TaxList = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount_Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount_Gross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount_Net = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount_Extra = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Amount_TaxList = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Invoice = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fk_TemplateId = table.Column<int>(type: "int", nullable: true),
                    fk_TypeId = table.Column<int>(type: "int", nullable: false),
                    fk_CategoryId = table.Column<int>(type: "int", nullable: true),
                    fk_StatusId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancePayments_AspNetUsers_fk_UserId",
                        column: x => x.fk_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancePayments_FinanceCategories_fk_CategoryId",
                        column: x => x.fk_CategoryId,
                        principalTable: "FinanceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancePayments_FinancePaymentTemplates_fk_TemplateId",
                        column: x => x.fk_TemplateId,
                        principalTable: "FinancePaymentTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancePayments_FinanceStatuses_fk_StatusId",
                        column: x => x.fk_StatusId,
                        principalTable: "FinanceStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancePayments_FinanceTypes_fk_TypeId",
                        column: x => x.fk_TypeId,
                        principalTable: "FinanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("626e5439-ac0e-423f-f10a-08d8cabafa0b"), "714a5239-a8a3-4b42-af07-033481bd81e0", "Test", "TEST" },
                    { new Guid("c50e0c00-bf4a-4e6a-8b06-08d8ca5d9e57"), "896e6ba7-5b4f-4231-bcc7-34aba5ca1e57", "User", "USER" },
                    { new Guid("898da44c-f5c4-45a4-7236-08d8c9fa7c8f"), "cc4e0669-e7ef-4edc-83d7-977ef284d407", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Darkmode", "DataType", "Email", "EmailConfirmed", "Lastname", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Picture", "SecurityStamp", "StartBalance", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("c7e66c25-bb5d-41f2-c762-08d8cc11b158"), 0, "05c6a9ba-80b0-4ee6-ab88-258cc2edbf09", true, null, "ole@admin.gov", true, "Eggersmann", true, null, "Ole", "OLE@ADMIN.GOV", "OLE", "AQAAAAEAACcQAAAAEHTM1p5KXcvGwKk4muG28dmLnhAgR3spQVXORsKEw+IN36bupGX27DhsTNrwIymmQg==", null, false, null, "VTPKISZMI2WKD6GNEHR223NRWDHGYRX6", 0.0, false, "Ole" },
                    { new Guid("7084a204-330e-4b8e-0788-08d8d0e3f5d6"), 0, "dc8cb136-9f75-46ee-8cae-72da0e35ff89", true, null, "Admin@Admin.Admin", true, "Admin", true, null, "Admin", "ADMIN@ADMIN.ADMIN", "ADMIN", "AQAAAAEAACcQAAAAEJLDpQEhYHywkPUimXOLlP6w24hXDuQdI2wtLcSKIB0K3BPmeFV+nzAaNgRRU2eozA==", null, false, null, "RRF4WT3IKFEA5BLYKDY5UJNCIAGATOAP", 0.0, false, "Admin" },
                    { new Guid("1c30add5-c7a9-48e9-6beb-08d8c9d5dc9c"), 0, "22ac1596-f950-40cc-ad84-92df87f8d892", true, null, "Francesco.Aepler@gmail.com", true, "Aepler", true, null, "Francesco", "FRANCESCO.AEPLER@GMAIL.COM", "FRANCESCO", "AQAAAAEAACcQAAAAENCkeX4zTaT+Tre5hnrmc1oMzq420b8/GcdRhtRgWXknIW9VmEdemaVj0SVLTxJERA==", null, false, null, "YK7VQDBLK2PUOJNEK7YKOW7NQDH7EDYO", 0.0, false, "Francesco" }
                });

            migrationBuilder.InsertData(
                table: "FinanceCategories",
                columns: new[] { "Id", "Deleted", "DeletedOn", "Name", "fk_UserId" },
                values: new object[,]
                {
                    { 1, false, null, "Living", null },
                    { 6, false, null, "Loans", null },
                    { 5, false, null, "Insurance", null },
                    { 4, false, null, "Mobility", null },
                    { 3, false, null, "Leisure", null },
                    { 2, false, null, "Groceries", null },
                    { 7, false, null, "Saving", null }
                });

            migrationBuilder.InsertData(
                table: "FinanceStatuses",
                columns: new[] { "Id", "Deleted", "DeletedOn", "EndPoint", "Name", "fk_UserId" },
                values: new object[,]
                {
                    { 1, false, null, true, "Paid", null },
                    { 3, false, null, false, "Pending", null },
                    { 4, false, null, false, "Fictitious", null },
                    { 2, false, null, true, "Received", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("898da44c-f5c4-45a4-7236-08d8c9fa7c8f"), new Guid("7084a204-330e-4b8e-0788-08d8d0e3f5d6") },
                    { new Guid("898da44c-f5c4-45a4-7236-08d8c9fa7c8f"), new Guid("1c30add5-c7a9-48e9-6beb-08d8c9d5dc9c") },
                    { new Guid("898da44c-f5c4-45a4-7236-08d8c9fa7c8f"), new Guid("c7e66c25-bb5d-41f2-c762-08d8cc11b158") }
                });

            migrationBuilder.InsertData(
                table: "FinanceTypes",
                columns: new[] { "Id", "Debit", "Deleted", "DeletedOn", "EndTaxType", "ExtraInput", "Name", "fk_StatusId", "fk_UserId" },
                values: new object[,]
                {
                    { 2, true, false, null, "Gross", null, "Monthly Expens", 1, null },
                    { 3, true, false, null, "Gross", "Extra_Amount", "Expenditure", 1, null },
                    { 1, false, false, null, "Net", "Extra_Amount,TaxList", "Salary", 2, null },
                    { 4, false, false, null, "Net", "Extra_Amount", "Earnings", 2, null }
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
                name: "IX_CookingFavorites_fk_RecipeId",
                table: "CookingFavorites",
                column: "fk_RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_CookingFavorites_fk_UserId",
                table: "CookingFavorites",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CookingIngredientRecipes_RecipesId",
                table: "CookingIngredientRecipes",
                column: "RecipesId");

            migrationBuilder.CreateIndex(
                name: "IX_CookingIngredients_fk_UserId",
                table: "CookingIngredients",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CookingIngredients_IngredientId",
                table: "CookingIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_CookingIngredientTags_TagsId",
                table: "CookingIngredientTags",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_CookingRecipes_fk_UserId",
                table: "CookingRecipes",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CookingRecipeTags_TagsId",
                table: "CookingRecipeTags",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_CookingTags_fk_UserId",
                table: "CookingTags",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceCategories_fk_UserId",
                table: "FinanceCategories",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancePayments_fk_CategoryId",
                table: "FinancePayments",
                column: "fk_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancePayments_fk_StatusId",
                table: "FinancePayments",
                column: "fk_StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancePayments_fk_TemplateId",
                table: "FinancePayments",
                column: "fk_TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancePayments_fk_TypeId",
                table: "FinancePayments",
                column: "fk_TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancePayments_fk_UserId",
                table: "FinancePayments",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancePaymentTemplates_fk_CategoryId",
                table: "FinancePaymentTemplates",
                column: "fk_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancePaymentTemplates_fk_TypeId",
                table: "FinancePaymentTemplates",
                column: "fk_TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancePaymentTemplates_fk_UserId",
                table: "FinancePaymentTemplates",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceStatuses_fk_UserId",
                table: "FinanceStatuses",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceTypes_fk_StatusId",
                table: "FinanceTypes",
                column: "fk_StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceTypes_fk_UserId",
                table: "FinanceTypes",
                column: "fk_UserId");
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
                name: "CookingFavorites");

            migrationBuilder.DropTable(
                name: "CookingIngredientRecipes");

            migrationBuilder.DropTable(
                name: "CookingIngredientTags");

            migrationBuilder.DropTable(
                name: "CookingRecipeTags");

            migrationBuilder.DropTable(
                name: "FinancePayments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CookingIngredients");

            migrationBuilder.DropTable(
                name: "CookingRecipes");

            migrationBuilder.DropTable(
                name: "CookingTags");

            migrationBuilder.DropTable(
                name: "FinancePaymentTemplates");

            migrationBuilder.DropTable(
                name: "FinanceCategories");

            migrationBuilder.DropTable(
                name: "FinanceTypes");

            migrationBuilder.DropTable(
                name: "FinanceStatuses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
