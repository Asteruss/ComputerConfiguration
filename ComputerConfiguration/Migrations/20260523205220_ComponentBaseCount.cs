using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerConfiguration.Migrations
{
    /// <inheritdoc />
    public partial class ComponentBaseCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Psus");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Gpus");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Coolers");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Cases");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Storages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Rams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Psus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Motherboards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Gpus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Cpus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Coolers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Rams");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Psus");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Motherboards");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Gpus");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Cpus");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Coolers");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Cases");

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Storages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Rams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Psus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Motherboards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Gpus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Cpus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Coolers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Cases",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
