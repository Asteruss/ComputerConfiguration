using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerConfiguration.Migrations
{
    /// <inheritdoc />
    public partial class CompBuildOptionsManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalServiceOptions_ComputerBuilds_ComputerBuildId",
                table: "AdditionalServiceOptions");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalServiceOptions_ComputerBuildId",
                table: "AdditionalServiceOptions");

            migrationBuilder.DropColumn(
                name: "ComputerBuildId",
                table: "AdditionalServiceOptions");

            migrationBuilder.CreateTable(
                name: "AdditionalServiceOptionComputerBuild",
                columns: table => new
                {
                    AdditionalServicesId = table.Column<int>(type: "int", nullable: false),
                    ComputerBuildsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalServiceOptionComputerBuild", x => new { x.AdditionalServicesId, x.ComputerBuildsId });
                    table.ForeignKey(
                        name: "FK_AdditionalServiceOptionComputerBuild_AdditionalServiceOptions_AdditionalServicesId",
                        column: x => x.AdditionalServicesId,
                        principalTable: "AdditionalServiceOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdditionalServiceOptionComputerBuild_ComputerBuilds_ComputerBuildsId",
                        column: x => x.ComputerBuildsId,
                        principalTable: "ComputerBuilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceOptionComputerBuild_ComputerBuildsId",
                table: "AdditionalServiceOptionComputerBuild",
                column: "ComputerBuildsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalServiceOptionComputerBuild");

            migrationBuilder.AddColumn<int>(
                name: "ComputerBuildId",
                table: "AdditionalServiceOptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalServiceOptions_ComputerBuildId",
                table: "AdditionalServiceOptions",
                column: "ComputerBuildId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalServiceOptions_ComputerBuilds_ComputerBuildId",
                table: "AdditionalServiceOptions",
                column: "ComputerBuildId",
                principalTable: "ComputerBuilds",
                principalColumn: "Id");
        }
    }
}
