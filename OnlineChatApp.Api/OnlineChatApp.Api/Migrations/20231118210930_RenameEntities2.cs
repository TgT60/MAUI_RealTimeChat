using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineChatApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class RenameEntities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FrinedId",
                table: "TblUserFriends",
                newName: "FriendId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FriendId",
                table: "TblUserFriends",
                newName: "FrinedId");
        }
    }
}
