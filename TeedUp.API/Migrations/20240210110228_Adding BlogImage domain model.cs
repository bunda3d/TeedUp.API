using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeedUp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddingBlogImagedomainmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    FileExtension = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    Title = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(2000)", maxLength: 2000, nullable: true),
                    Latitude = table.Column<decimal>(type: "DECIMAL(12,9)", nullable: true),
                    Longitude = table.Column<decimal>(type: "DECIMAL(12,9)", nullable: true),
                    Url = table.Column<string>(type: "VARCHAR(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogImages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogImages");
        }
    }
}
