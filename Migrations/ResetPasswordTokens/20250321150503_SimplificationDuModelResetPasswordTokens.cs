using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSENA.Migrations.ResetPasswordTokens
{
    /// <inheritdoc />
    public partial class SimplificationDuModelResetPasswordTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NewPassword",
                table: "ResetPasswordTokens",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ResetPasswordTokens",
                keyColumn: "NewPassword",
                keyValue: null,
                column: "NewPassword",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "NewPassword",
                table: "ResetPasswordTokens",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
