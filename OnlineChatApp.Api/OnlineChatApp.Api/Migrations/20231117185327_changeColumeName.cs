using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineChatApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class changeColumeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoreSalt",
                table: "TblUsers",
                newName: "StoredSalt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoredSalt",
                table: "TblUsers",
                newName: "StoreSalt");
        }
    }
}
