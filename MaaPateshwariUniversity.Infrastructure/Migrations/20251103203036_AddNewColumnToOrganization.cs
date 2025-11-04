using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MaaPateshwariUniversity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnToOrganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AccreditationAgencies",
                columns: table => new
                {
                    AgencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AgencyCode = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AgencyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccreditationAgencies", x => x.AgencyId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountryName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    DistrictId = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StateId = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    DistrictName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.DistrictId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GovernmentCategories",
                columns: table => new
                {
                    GovernmentCategoryId = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GovernmentCategoryName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernmentCategories", x => x.GovernmentCategoryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InstitutionCategories",
                columns: table => new
                {
                    CategoryId = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitutionCategories", x => x.CategoryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LocationTypes",
                columns: table => new
                {
                    LocationTypeId = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    LocationTypeName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationTypes", x => x.LocationTypeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ManagementTypes",
                columns: table => new
                {
                    ManagementTypeId = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ManagementTypeName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagementTypes", x => x.ManagementTypeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MinorityTypes",
                columns: table => new
                {
                    MinorityTypeId = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MinorityTypeName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinorityTypes", x => x.MinorityTypeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrganizationAccreditations",
                columns: table => new
                {
                    OrganizationAccreditationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    AgencyId = table.Column<int>(type: "int", nullable: false),
                    GradeOrStatus = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValidFrom = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CertificateUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationAccreditations", x => x.OrganizationAccreditationId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrganizationTypeId = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    NameEn = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameHi = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameLocal = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentOrganizationId = table.Column<long>(type: "bigint", nullable: true),
                    InstituteName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InstituteCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAffiliated = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsAutonomous = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CategoryId = table.Column<ushort>(type: "smallint unsigned", nullable: true),
                    SubTypeId = table.Column<ushort>(type: "smallint unsigned", nullable: true),
                    Specialization = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ManagementTypeId = table.Column<ushort>(type: "smallint unsigned", nullable: true),
                    GovernmentCategoryId = table.Column<ushort>(type: "smallint unsigned", nullable: true),
                    IsMinorityInstitution = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MinorityTypeId = table.Column<ushort>(type: "smallint unsigned", nullable: true),
                    YearOfEstablishment = table.Column<ushort>(type: "smallint unsigned", nullable: true),
                    LocationTypeId = table.Column<byte>(type: "tinyint unsigned", nullable: true),
                    StateId = table.Column<ushort>(type: "smallint unsigned", nullable: true),
                    DistrictId = table.Column<uint>(type: "int unsigned", nullable: true),
                    City = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Street = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pincode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GeoLatitude = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    GeoLongitude = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    OfficialEmail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TelephoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fax = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExtensionNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Website = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TwitterLink = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FacebookLink = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LinkedinLink = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StatusId = table.Column<byte>(type: "tinyint unsigned", nullable: true),
                    IsVisible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    OuCategoryId = table.Column<byte>(type: "tinyint unsigned", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrganizationTypes",
                columns: table => new
                {
                    OrganizationTypeId = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    TypeName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationTypes", x => x.OrganizationTypeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OuCategories",
                columns: table => new
                {
                    OuCategoryId = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    OuCategoryName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OuCategories", x => x.OuCategoryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    RefreshTokenId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Token = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Revoked = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.RefreshTokenId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StatusMasters",
                columns: table => new
                {
                    StatusId = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    StatusName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusMasters", x => x.StatusId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubInstitutionTypes",
                columns: table => new
                {
                    SubTypeId = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubTypeName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubInstitutionTypes", x => x.SubTypeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<ulong>(type: "bigint unsigned", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StateName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CountryId = table.Column<ushort>(type: "smallint unsigned", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AccreditationAgencies",
                columns: new[] { "AgencyId", "AgencyCode", "AgencyName", "IsActive" },
                values: new object[,]
                {
                    { 1, "NAAC", "National Assessment and Accreditation Council", true },
                    { 2, "AICTE", "All India Council for Technical Education", true },
                    { 3, "NCTE", "National Council for Teacher Education", true },
                    { 4, "PCI", "Pharmacy Council of India", true },
                    { 5, "BCI", "Bar Council of India", true },
                    { 6, "UGC", "University Grants Commission", true }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryName", "IsActive" },
                values: new object[] { (ushort)1, "India", true });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "DistrictId", "DistrictName", "IsActive", "StateId" },
                values: new object[] { 1u, "Balrampur", true, (ushort)1 });

            migrationBuilder.InsertData(
                table: "LocationTypes",
                columns: new[] { "LocationTypeId", "IsActive", "LocationTypeName" },
                values: new object[,]
                {
                    { (byte)1, true, "Urban" },
                    { (byte)2, true, "Rural" }
                });

            migrationBuilder.InsertData(
                table: "OrganizationTypes",
                columns: new[] { "OrganizationTypeId", "IsActive", "TypeName" },
                values: new object[,]
                {
                    { (byte)1, true, "University" },
                    { (byte)2, true, "Affiliated College" },
                    { (byte)3, true, "Office" }
                });

            migrationBuilder.InsertData(
                table: "OuCategories",
                columns: new[] { "OuCategoryId", "IsActive", "OuCategoryName" },
                values: new object[,]
                {
                    { (byte)1, true, "PLAN" },
                    { (byte)2, true, "NON-PLAN" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { 1, "SuperAdmin" },
                    { 2, "UniversityAdmin" },
                    { 3, "CollegeAdmin" }
                });

            migrationBuilder.InsertData(
                table: "StatusMasters",
                columns: new[] { "StatusId", "IsActive", "StatusName" },
                values: new object[,]
                {
                    { (byte)1, true, "Published" },
                    { (byte)2, true, "Draft" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "CountryId", "IsActive", "StateName" },
                values: new object[] { (ushort)1, (ushort)1, true, "Uttar Pradesh" });

            migrationBuilder.CreateIndex(
                name: "IX_AccreditationAgencies_AgencyCode",
                table: "AccreditationAgencies",
                column: "AgencyCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Districts_StateId_DistrictName",
                table: "Districts",
                columns: new[] { "StateId", "DistrictName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccreditationAgencies");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "GovernmentCategories");

            migrationBuilder.DropTable(
                name: "InstitutionCategories");

            migrationBuilder.DropTable(
                name: "LocationTypes");

            migrationBuilder.DropTable(
                name: "ManagementTypes");

            migrationBuilder.DropTable(
                name: "MinorityTypes");

            migrationBuilder.DropTable(
                name: "OrganizationAccreditations");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "OrganizationTypes");

            migrationBuilder.DropTable(
                name: "OuCategories");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "StatusMasters");

            migrationBuilder.DropTable(
                name: "SubInstitutionTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
