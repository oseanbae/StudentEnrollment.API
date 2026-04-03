using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentEnrollment.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Student",
                type: "TEXT",
                maxLength: 15,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Student");
        }
    }
}
