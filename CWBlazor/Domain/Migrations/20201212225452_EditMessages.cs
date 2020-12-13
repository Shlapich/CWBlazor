using Microsoft.EntityFrameworkCore.Migrations;

namespace CWBlazor.Domain.Migrations
{
    public partial class EditMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Messages",
                newName: "CWUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                newName: "IX_Messages_CWUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_CWUserId",
                table: "Messages",
                column: "CWUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_CWUserId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "CWUserId",
                table: "Messages",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_CWUserId",
                table: "Messages",
                newName: "IX_Messages_ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
