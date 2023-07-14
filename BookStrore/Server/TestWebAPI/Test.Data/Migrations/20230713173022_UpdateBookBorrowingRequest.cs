using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Data.Migrations
{
    public partial class UpdateBookBorrowingRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserApproveId",
                table: "BookBorrowingRequests");

            migrationBuilder.AddColumn<string>(
                name: "UserApprovedName",
                table: "BookBorrowingRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserApprovedName",
                table: "BookBorrowingRequests");

            migrationBuilder.AddColumn<Guid>(
                name: "UserApproveId",
                table: "BookBorrowingRequests",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
