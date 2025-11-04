using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaaPateshwariUniversity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnsToOrganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ushort>(
                name: "CountryId",
                table: "Organizations",
                type: "smallint unsigned",
                nullable: false,
                defaultValue: (ushort)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Organizations");
        }
    }
}
