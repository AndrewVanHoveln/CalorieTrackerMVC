using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calorie_Tracking_App.Migrations
{
    /// <inheritdoc />
    public partial class AddDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalorieEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Carbs = table.Column<double>(type: "REAL", nullable: false),
                    Protien = table.Column<double>(type: "REAL", nullable: false),
                    Fats = table.Column<double>(type: "REAL", nullable: false),
                    Calories = table.Column<double>(type: "REAL", nullable: false),
                    LoggedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalorieEntries", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalorieEntries");
        }
    }
}
