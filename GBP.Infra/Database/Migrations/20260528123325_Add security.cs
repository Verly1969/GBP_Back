using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBP.Infra.Database.Migrations
{
    /// <inheritdoc />
    public partial class Addsecurity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SecurityLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    EndPoint = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateAttempt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UserAgent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsBanned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    StartBan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndBan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BanRaison = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityLog", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "0j+VVqhClNAGta6K5P4ZMQ==.+uG9kBwoXQibe2UdhYsqC8DDiVZpXWDrLPXzS83mLtg=");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityLog_IpAddress_DateAttempt_BanCheck",
                table: "SecurityLog",
                columns: new[] { "IpAddress", "DateAttempt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecurityLog");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "password-hash");
        }
    }
}
