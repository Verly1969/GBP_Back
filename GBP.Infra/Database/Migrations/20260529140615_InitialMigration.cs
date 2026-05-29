using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBP.Infra.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SecretKeyHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastConnected = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastConnected", "LastName", "PasswordHash", "Role", "SecretKeyHash", "Status", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@example.com", "User", null, "Admin", "v1.j8BV0YL+rRLe4si63uUFUg==.8hNiGyzHkCh33TrPllVuFHZOsFTYdm5xfr80PVWiCa4=", "Admin", null, "Active", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_SecurityLog_IpAddress_DateAttempt_BanCheck",
                table: "SecurityLog",
                columns: new[] { "IpAddress", "DateAttempt" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SecurityLog");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
