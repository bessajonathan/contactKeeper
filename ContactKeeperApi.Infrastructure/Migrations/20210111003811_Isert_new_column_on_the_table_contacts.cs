using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactKeeperApi.Infra.Migrations
{
    public partial class Isert_new_column_on_the_table_contacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Contacts",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Contacts");
        }
    }
}
