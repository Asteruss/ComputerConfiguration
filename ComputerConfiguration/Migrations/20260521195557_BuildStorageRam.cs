using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerConfiguration.Migrations
{
    /// <inheritdoc />
    public partial class BuildStorageRam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComputerBuildRam");

            migrationBuilder.DropTable(
                name: "ComputerBuildStorage");

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

            migrationBuilder.CreateTable(
                name: "BuildRams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComputerBuildId = table.Column<int>(type: "int", nullable: true),
                    RamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildRams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildRams_ComputerBuilds_ComputerBuildId",
                        column: x => x.ComputerBuildId,
                        principalTable: "ComputerBuilds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BuildRams_Rams_RamId",
                        column: x => x.RamId,
                        principalTable: "Rams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BuildStorages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComputerBuildId = table.Column<int>(type: "int", nullable: true),
                    StorageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildStorages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildStorages_ComputerBuilds_ComputerBuildId",
                        column: x => x.ComputerBuildId,
                        principalTable: "ComputerBuilds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BuildStorages_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComputerBuilds_RamId",
                table: "ComputerBuilds",
                column: "RamId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerBuilds_StorageId",
                table: "ComputerBuilds",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildRams_ComputerBuildId",
                table: "BuildRams",
                column: "ComputerBuildId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildRams_RamId",
                table: "BuildRams",
                column: "RamId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildStorages_ComputerBuildId",
                table: "BuildStorages",
                column: "ComputerBuildId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildStorages_StorageId",
                table: "BuildStorages",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComputerBuilds_Rams_RamId",
                table: "ComputerBuilds");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerBuilds_Storages_StorageId",
                table: "ComputerBuilds");

            migrationBuilder.DropTable(
                name: "BuildRams");

            migrationBuilder.DropTable(
                name: "BuildStorages");

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

            migrationBuilder.CreateTable(
                name: "ComputerBuildRam",
                columns: table => new
                {
                    ComputerBuildsId = table.Column<int>(type: "int", nullable: false),
                    RamsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerBuildRam", x => new { x.ComputerBuildsId, x.RamsId });
                    table.ForeignKey(
                        name: "FK_ComputerBuildRam_ComputerBuilds_ComputerBuildsId",
                        column: x => x.ComputerBuildsId,
                        principalTable: "ComputerBuilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputerBuildRam_Rams_RamsId",
                        column: x => x.RamsId,
                        principalTable: "Rams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComputerBuildStorage",
                columns: table => new
                {
                    ComputerBuildsId = table.Column<int>(type: "int", nullable: false),
                    StoragesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerBuildStorage", x => new { x.ComputerBuildsId, x.StoragesId });
                    table.ForeignKey(
                        name: "FK_ComputerBuildStorage_ComputerBuilds_ComputerBuildsId",
                        column: x => x.ComputerBuildsId,
                        principalTable: "ComputerBuilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComputerBuildStorage_Storages_StoragesId",
                        column: x => x.StoragesId,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComputerBuildRam_RamsId",
                table: "ComputerBuildRam",
                column: "RamsId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerBuildStorage_StoragesId",
                table: "ComputerBuildStorage",
                column: "StoragesId");
        }
    }
}
