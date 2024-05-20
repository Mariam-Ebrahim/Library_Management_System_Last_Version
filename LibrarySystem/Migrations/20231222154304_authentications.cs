using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Migrations
{
    /// <inheritdoc />
    public partial class authentications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
   name: "StartMembership",
   table: "Member",
   nullable: false,
   defaultValueSql: "CONVERT(DATETIME, GETDATE())",
   oldClrType: typeof(DateTime),
   oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
              name: "EndMembership",
              table: "Member",
              nullable: false,
              defaultValueSql: "DATEADD(year, 1, GETDATE())",
              oldClrType: typeof(DateTime),
              oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
