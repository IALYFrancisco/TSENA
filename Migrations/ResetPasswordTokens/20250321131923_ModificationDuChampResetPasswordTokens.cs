using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSENA.Migrations.ResetPasswordTokens
{
    /// <inheritdoc />
    public partial class ModificationDuChampResetPasswordTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "ResetPasswordTokens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NewPassword",
                table: "ResetPasswordTokens",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "ResetPasswordTokens");

            migrationBuilder.DropColumn(
                name: "NewPassword",
                table: "ResetPasswordTokens");
        }
    }
}
