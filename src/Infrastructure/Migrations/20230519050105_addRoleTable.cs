using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addRoleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Register",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RolesTableId",
                table: "Register",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Register_RolesTableId",
                table: "Register",
                column: "RolesTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Register_RolesTable_RolesTableId",
                table: "Register",
                column: "RolesTableId",
                principalTable: "RolesTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Register_RolesTable_RolesTableId",
                table: "Register");

            migrationBuilder.DropTable(
                name: "RolesTable");

            migrationBuilder.DropIndex(
                name: "IX_Register_RolesTableId",
                table: "Register");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Register");

            migrationBuilder.DropColumn(
                name: "RolesTableId",
                table: "Register");
        }
    }
}
