using Contacto.Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contacto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"insert into Users values('{ApplicationUser.BuiltInUser.Id.ToString()}','{ApplicationUser.BuiltInUser.Username}','{ApplicationUser.BuiltInUser.PasswordHash}','{ApplicationUser.BuiltInUser.PasswordSalt}')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"delete from Users where Id = '{ApplicationUser.BuiltInUser.Id.ToString()}'");
        }
    }
}
