using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerConfiguration.Migrations
{
    /// <inheritdoc />
    public partial class OrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoolerSockets_Coolers_CoolerId",
                table: "CoolerSockets");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CoolerId",
                table: "CoolerSockets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CoolerSockets_Coolers_CoolerId",
                table: "CoolerSockets",
                column: "CoolerId",
                principalTable: "Coolers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoolerSockets_Coolers_CoolerId",
                table: "CoolerSockets");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "CoolerId",
                table: "CoolerSockets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoolerSockets_Coolers_CoolerId",
                table: "CoolerSockets",
                column: "CoolerId",
                principalTable: "Coolers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
