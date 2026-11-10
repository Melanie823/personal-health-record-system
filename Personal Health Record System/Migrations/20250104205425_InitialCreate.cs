using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Personal_Health_Record_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Health",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystolicPressure = table.Column<int>(type: "int", nullable: false),
                    DiastolicPressure = table.Column<int>(type: "int", nullable: false),
                    BloodOxygen = table.Column<int>(type: "int", nullable: false),
                    HeartRate = table.Column<int>(type: "int", nullable: false),
                    BodyTemperature = table.Column<float>(type: "real", nullable: false),
                    FastingBloodSugar = table.Column<float>(type: "real", nullable: false),
                    PostMealBloodSugar = table.Column<float>(type: "real", nullable: false),
                    SleepDuration = table.Column<float>(type: "real", nullable: false),
                    IsExercise = table.Column<bool>(type: "bit", nullable: false),
                    Breakfast = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lunch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dinner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Health", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Health");
        }
    }
}
