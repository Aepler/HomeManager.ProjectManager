using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeManager.Data.Migrations
{
    public partial class HomeManagerApi : Migration
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
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Darkmode = table.Column<bool>(type: "bit", nullable: false),
                    ProfilePictureData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ProfilePictureDataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentWallet = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "FinanceWallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fk_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartBalance = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    BalanceUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceWallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceWallets_AspNetUsers_fk_UserId",
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndTaxType = table.Column<int>(type: "int", nullable: false),
                    DefaultTaxRate = table.Column<int>(type: "int", nullable: true),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    ExtraInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Repeating = table.Column<bool>(type: "bit", nullable: false),
                    fk_StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "FinanceRepeatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RepeatInterval = table.Column<int>(type: "int", nullable: false),
                    RepeatStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepeatEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fk_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fk_WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    GrossAmount = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    TaxDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRate = table.Column<int>(type: "int", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    ExtraCostDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraCostAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailedTaxDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailedTaxRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailedTaxAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    InvoiceDataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarrantyExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fk_TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fk_CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceRepeatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceRepeatings_AspNetUsers_fk_UserId",
                        column: x => x.fk_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceRepeatings_FinanceCategories_fk_CategoryId",
                        column: x => x.fk_CategoryId,
                        principalTable: "FinanceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceRepeatings_FinanceTypes_fk_TypeId",
                        column: x => x.fk_TypeId,
                        principalTable: "FinanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceRepeatings_FinanceWallets_fk_WalletId",
                        column: x => x.fk_WalletId,
                        principalTable: "FinanceWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinanceTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fk_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    GrossAmount = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    TaxDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRate = table.Column<int>(type: "int", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    ExtraCostDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraCostAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailedTaxDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailedTaxRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailedTaxAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    InvoiceDataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarrantyExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fk_TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fk_CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceTemplates_AspNetUsers_fk_UserId",
                        column: x => x.fk_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceTemplates_FinanceCategories_fk_CategoryId",
                        column: x => x.fk_CategoryId,
                        principalTable: "FinanceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinanceTemplates_FinanceTypes_fk_TypeId",
                        column: x => x.fk_TypeId,
                        principalTable: "FinanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancePayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fk_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fk_WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    GrossAmount = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    TaxDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxRate = table.Column<int>(type: "int", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    ExtraCostDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraCostAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailedTaxDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailedTaxRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailedTaxAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    InvoiceDataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarrantyExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fk_RepeatingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    fk_TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fk_CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    fk_StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                        name: "FK_FinancePayments_FinanceRepeatings_fk_RepeatingId",
                        column: x => x.fk_RepeatingId,
                        principalTable: "FinanceRepeatings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancePayments_FinanceStatuses_fk_StatusId",
                        column: x => x.fk_StatusId,
                        principalTable: "FinanceStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancePayments_FinanceTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "FinanceTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancePayments_FinanceTypes_fk_TypeId",
                        column: x => x.fk_TypeId,
                        principalTable: "FinanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancePayments_FinanceWallets_fk_WalletId",
                        column: x => x.fk_WalletId,
                        principalTable: "FinanceWallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("898da44c-f5c4-45a4-7236-08d8c9fa7c8f"), "cc4e0669-e7ef-4edc-83d7-977ef284d407", "Admin", "ADMIN" },
                    { new Guid("c50e0c00-bf4a-4e6a-8b06-08d8ca5d9e57"), "896e6ba7-5b4f-4231-bcc7-34aba5ca1e57", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CurrentWallet", "Darkmode", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureData", "ProfilePictureDataType", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("7084a204-330e-4b8e-0788-08d8d0e3f5d6"), 0, "dc8cb136-9f75-46ee-8cae-72da0e35ff89", null, true, "Admin@Admin.Admin", true, "Admin", "Admin", true, null, "ADMIN@ADMIN.ADMIN", "ADMIN", "AQAAAAEAACcQAAAAEJLDpQEhYHywkPUimXOLlP6w24hXDuQdI2wtLcSKIB0K3BPmeFV+nzAaNgRRU2eozA==", null, false, null, null, "RRF4WT3IKFEA5BLYKDY5UJNCIAGATOAP", false, "Admin" },
                    { new Guid("1c30add5-c7a9-48e9-6beb-08d8c9d5dc9c"), 0, "22ac1596-f950-40cc-ad84-92df87f8d892", null, true, "Francesco.Aepler@gmail.com", true, "Francesco", "Aepler", true, null, "FRANCESCO.AEPLER@GMAIL.COM", "FRANCESCO", "AQAAAAEAACcQAAAAENCkeX4zTaT+Tre5hnrmc1oMzq420b8/GcdRhtRgWXknIW9VmEdemaVj0SVLTxJERA==", null, false, null, null, "YK7VQDBLK2PUOJNEK7YKOW7NQDH7EDYO", false, "Francesco" },
                    { new Guid("c7e66c25-bb5d-41f2-c762-08d8cc11b158"), 0, "05c6a9ba-80b0-4ee6-ab88-258cc2edbf09", null, true, "ole@admin.gov", true, "Ole", "Eggersmann", true, null, "OLE@ADMIN.GOV", "OLE", "AQAAAAEAACcQAAAAEHTM1p5KXcvGwKk4muG28dmLnhAgR3spQVXORsKEw+IN36bupGX27DhsTNrwIymmQg==", null, false, null, null, "VTPKISZMI2WKD6GNEHR223NRWDHGYRX6", false, "Ole" }
                });

            migrationBuilder.InsertData(
                table: "FinanceCategories",
                columns: new[] { "Id", "Deleted", "DeletedOn", "Name", "fk_UserId" },
                values: new object[,]
                {
                    { new Guid("65df89ea-df64-4074-a36c-6b15d1f16baf"), false, null, "Living", null },
                    { new Guid("05e8b5cb-e640-4a20-8c4a-258626c90a84"), false, null, "Groceries", null },
                    { new Guid("e9187c61-0682-40d9-8a78-8098bfd5f401"), false, null, "Leisure", null },
                    { new Guid("6a7d9b47-7967-497f-8276-735d0a46cbcb"), false, null, "Mobility", null },
                    { new Guid("327b058f-4182-4ca1-9c1b-cfa46e3f59e0"), false, null, "Insurance", null },
                    { new Guid("de9f70b1-e498-4e6f-aba8-b290a7f91111"), false, null, "Loans", null },
                    { new Guid("1e02aece-87a5-43a4-9407-232718500157"), false, null, "Saving", null }
                });

            migrationBuilder.InsertData(
                table: "FinanceStatuses",
                columns: new[] { "Id", "Deleted", "DeletedOn", "EndPoint", "Name", "fk_UserId" },
                values: new object[,]
                {
                    { new Guid("58ae656b-0b17-4eed-aeeb-eeb660db266b"), false, null, true, "Paid", null },
                    { new Guid("641dde1b-2959-47ca-bbc6-d0979ff2bc14"), false, null, true, "Received", null },
                    { new Guid("c04f9f24-36c9-4569-86ed-3aa4e51278be"), false, null, false, "Pending", null },
                    { new Guid("26425888-2203-422d-8cf1-44400b5f9459"), false, null, false, "Fictitious", null }
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
                columns: new[] { "Id", "DefaultTaxRate", "Deleted", "DeletedOn", "EndTaxType", "ExtraInput", "Name", "Repeating", "TransactionType", "fk_StatusId", "fk_UserId" },
                values: new object[,]
                {
                    { new Guid("6b423c70-1edd-4988-9cc4-bd4b77eef7e0"), 19, false, null, 2, "3", "Monthly Expens", true, 1, new Guid("58ae656b-0b17-4eed-aeeb-eeb660db266b"), null },
                    { new Guid("93399d01-9c77-483d-bd70-964898a7d875"), 19, false, null, 2, "1,3,4", "Expenditure", false, 1, new Guid("58ae656b-0b17-4eed-aeeb-eeb660db266b"), null },
                    { new Guid("66b2ecfa-915d-4ca1-af3f-e5c8602d1c23"), null, false, null, 1, "1,2", "Salary", false, 2, new Guid("641dde1b-2959-47ca-bbc6-d0979ff2bc14"), null },
                    { new Guid("2cdc950b-c30d-4cbe-a0ca-0965f86cb9ac"), null, false, null, 1, "1,3", "Earnings", false, 2, new Guid("641dde1b-2959-47ca-bbc6-d0979ff2bc14"), null }
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
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceCategories_fk_UserId",
                table: "FinanceCategories",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancePayments_fk_CategoryId",
                table: "FinancePayments",
                column: "fk_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancePayments_fk_RepeatingId",
                table: "FinancePayments",
                column: "fk_RepeatingId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancePayments_fk_StatusId",
                table: "FinancePayments",
                column: "fk_StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancePayments_fk_TypeId",
                table: "FinancePayments",
                column: "fk_TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancePayments_fk_UserId",
                table: "FinancePayments",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancePayments_fk_WalletId",
                table: "FinancePayments",
                column: "fk_WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancePayments_TemplateId",
                table: "FinancePayments",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceRepeatings_fk_CategoryId",
                table: "FinanceRepeatings",
                column: "fk_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceRepeatings_fk_TypeId",
                table: "FinanceRepeatings",
                column: "fk_TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceRepeatings_fk_UserId",
                table: "FinanceRepeatings",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceRepeatings_fk_WalletId",
                table: "FinanceRepeatings",
                column: "fk_WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceStatuses_fk_UserId",
                table: "FinanceStatuses",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceTemplates_fk_CategoryId",
                table: "FinanceTemplates",
                column: "fk_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceTemplates_fk_TypeId",
                table: "FinanceTemplates",
                column: "fk_TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceTemplates_fk_UserId",
                table: "FinanceTemplates",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceTypes_fk_StatusId",
                table: "FinanceTypes",
                column: "fk_StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceTypes_fk_UserId",
                table: "FinanceTypes",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceWallets_fk_UserId",
                table: "FinanceWallets",
                column: "fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });
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
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "FinancePayments");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CookingIngredients");

            migrationBuilder.DropTable(
                name: "CookingRecipes");

            migrationBuilder.DropTable(
                name: "CookingTags");

            migrationBuilder.DropTable(
                name: "FinanceRepeatings");

            migrationBuilder.DropTable(
                name: "FinanceTemplates");

            migrationBuilder.DropTable(
                name: "FinanceWallets");

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
