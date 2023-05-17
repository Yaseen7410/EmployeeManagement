using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Register_RolesTable_RolesTableId",
                table: "Register");

            migrationBuilder.DropTable(
                name: "RolesTable");

            migrationBuilder.RenameColumn(
                name: "RolesTableId",
                table: "Register",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_Register_RolesTableId",
                table: "Register",
                newName: "IX_Register_RolesId");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Register_Roles_RolesId",
                table: "Register",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Register_Roles_RolesId",
                table: "Register");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "Register",
                newName: "RolesTableId");

            migrationBuilder.RenameIndex(
                name: "IX_Register_RolesId",
                table: "Register",
                newName: "IX_Register_RolesTableId");

            migrationBuilder.CreateTable(
                name: "RolesTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesTable", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Register_RolesTable_RolesTableId",
                table: "Register",
                column: "RolesTableId",
                principalTable: "RolesTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
