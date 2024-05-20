using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Migrations
{
    /// <inheritdoc />
    public partial class copyy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookCopyBookLending");

            migrationBuilder.AddColumn<int>(
                name: "BookLendingLendingId",
                table: "BookCopy",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookCopy_BookLendingLendingId",
                table: "BookCopy",
                column: "BookLendingLendingId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopy_BookLending_BookLendingLendingId",
                table: "BookCopy",
                column: "BookLendingLendingId",
                principalTable: "BookLending",
                principalColumn: "LendingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopy_BookLending_BookLendingLendingId",
                table: "BookCopy");

            migrationBuilder.DropIndex(
                name: "IX_BookCopy_BookLendingLendingId",
                table: "BookCopy");

            migrationBuilder.DropColumn(
                name: "BookLendingLendingId",
                table: "BookCopy");

            migrationBuilder.CreateTable(
                name: "BookCopyBookLending",
                columns: table => new
                {
                    BookCopycopyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookLendingaLendingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCopyBookLending", x => new { x.BookCopycopyId, x.BookLendingaLendingId });
                    table.ForeignKey(
                        name: "FK_BookCopyBookLending_BookCopy_BookCopycopyId",
                        column: x => x.BookCopycopyId,
                        principalTable: "BookCopy",
                        principalColumn: "copyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCopyBookLending_BookLending_BookLendingaLendingId",
                        column: x => x.BookLendingaLendingId,
                        principalTable: "BookLending",
                        principalColumn: "LendingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookCopyBookLending_BookLendingaLendingId",
                table: "BookCopyBookLending",
                column: "BookLendingaLendingId");
        }
    }
}
