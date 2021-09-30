using Microsoft.EntityFrameworkCore.Migrations;

namespace UserIdentityProject.Migrations
{
    public partial class OptionalPasswordforFolders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OptionalPassword",
                schema: "Identity",
                table: "Folders",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionalPassword",
                schema: "Identity",
                table: "Folders");
        }
    }
}
