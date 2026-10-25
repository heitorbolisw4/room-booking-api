using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stage1.Migrations
{
    /// <inheritdoc />
    public partial class AddExpiresTimes_Reserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ExpiraEm",
                table: "Reservas",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiraEm",
                table: "Reservas");
        }
    }
}
