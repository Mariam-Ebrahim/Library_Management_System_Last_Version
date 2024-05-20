using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Migrations
{
    /// <inheritdoc />
    public partial class copy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopyBookLending_BookCopy_BookCopyISBN",
                table: "BookCopyBookLending");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCopyBookLending",
                table: "BookCopyBookLending");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCopy",
                table: "BookCopy");

            migrationBuilder.DropColumn(
                name: "BookCopyISBN",
                table: "BookCopyBookLending");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "BookCopy");

            migrationBuilder.AddColumn<string>(
                name: "BookCopycopyId",
                table: "BookCopyBookLending",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "copyId",
                table: "BookCopy",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCopyBookLending",
                table: "BookCopyBookLending",
                columns: new[] { "BookCopycopyId", "BookLendingaLendingId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCopy",
                table: "BookCopy",
                column: "copyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopyBookLending_BookCopy_BookCopycopyId",
                table: "BookCopyBookLending",
                column: "BookCopycopyId",
                principalTable: "BookCopy",
                principalColumn: "copyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopyBookLending_BookCopy_BookCopycopyId",
                table: "BookCopyBookLending");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCopyBookLending",
                table: "BookCopyBookLending");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCopy",
                table: "BookCopy");

            migrationBuilder.DropColumn(
                name: "BookCopycopyId",
                table: "BookCopyBookLending");

            migrationBuilder.DropColumn(
                name: "copyId",
                table: "BookCopy");

            migrationBuilder.AddColumn<int>(
                name: "BookCopyISBN",
                table: "BookCopyBookLending",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ISBN",
                table: "BookCopy",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCopyBookLending",
                table: "BookCopyBookLending",
                columns: new[] { "BookCopyISBN", "BookLendingaLendingId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCopy",
                table: "BookCopy",
                column: "ISBN");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopyBookLending_BookCopy_BookCopyISBN",
                table: "BookCopyBookLending",
                column: "BookCopyISBN",
                principalTable: "BookCopy",
                principalColumn: "ISBN",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
