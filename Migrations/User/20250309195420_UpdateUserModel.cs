using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSENA.Migrations.User
{
    /// <inheritdoc />
    public partial class UpdateUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "signInDate",
                table: "User",
                newName: "SignInDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SignInDate",
                table: "User",
                newName: "signInDate");
        }
    }
}
