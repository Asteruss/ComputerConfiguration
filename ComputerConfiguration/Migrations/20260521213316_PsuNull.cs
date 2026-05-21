using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerConfiguration.Migrations
{
    /// <inheritdoc />
    public partial class PsuNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComputerBuilds_Rams_RamId",
                table: "ComputerBuilds");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerBuilds_Storages_StorageId",
                table: "ComputerBuilds");

            migrationBuilder.DropIndex(
                name: "IX_ComputerBuilds_RamId",
                table: "ComputerBuilds");

            migrationBuilder.DropIndex(
                name: "IX_ComputerBuilds_StorageId",
                table: "ComputerBuilds");

            migrationBuilder.DropColumn(
                name: "RamId",
                table: "ComputerBuilds");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "ComputerBuilds");

            migrationBuilder.AlterColumn<int>(
                name: "Wattage",
                table: "Psus",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SataConnectors",
                table: "Psus",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PcieConnectors",
                table: "Psus",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Modular",
                table: "Psus",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "EfficiencyRating",
                table: "Psus",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Wattage",
                table: "Psus",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SataConnectors",
                table: "Psus",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PcieConnectors",
                table: "Psus",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Modular",
                table: "Psus",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EfficiencyRating",
                table: "Psus",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RamId",
                table: "ComputerBuilds",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StorageId",
                table: "ComputerBuilds",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComputerBuilds_RamId",
                table: "ComputerBuilds",
                column: "RamId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerBuilds_StorageId",
                table: "ComputerBuilds",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerBuilds_Rams_RamId",
                table: "ComputerBuilds",
                column: "RamId",
                principalTable: "Rams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerBuilds_Storages_StorageId",
                table: "ComputerBuilds",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id");
        }
    }
}
