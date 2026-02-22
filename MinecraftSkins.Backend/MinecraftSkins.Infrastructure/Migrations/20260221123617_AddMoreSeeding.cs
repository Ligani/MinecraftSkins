using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MinecraftSkins.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Skins",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440000"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4913));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("67f12345-a1b2-c3d4-e5f6-7890abcdef12"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4915));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4917));

            migrationBuilder.InsertData(
                table: "Skins",
                columns: new[] { "Id", "BasePriceUsd", "CreatedAtUtc", "IsAvailable", "IsDeleted", "Name", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("01234567-89ab-cdef-0123-456789abcdef"), 22.0m, new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4964), true, false, "Potion Bottle (Night Vision)", null },
                    { new Guid("7890abcd-ef12-3456-7890-abcdef123456"), 8.5m, new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4961), true, false, "Nether Portal Poster", null },
                    { new Guid("890abcde-f123-4567-890a-bcdef1234567"), 5.0m, new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4962), true, false, "Wolf Tamed Keychain", null },
                    { new Guid("90abcdef-1234-5678-90ab-cdef12345678"), 16.0m, new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4963), true, false, "Skeleton Archer Figurine", null },
                    { new Guid("b2c3d4e5-f678-90ab-cdef-1234567890ab"), 25.0m, new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4918), true, false, "Diamond Sword Replica", null },
                    { new Guid("c3d4e5f6-7890-abcd-ef12-34567890abcd"), 12.99m, new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4956), true, false, "Enderman Plush", null },
                    { new Guid("d4e5f678-90ab-cdef-1234-567890abcdef"), 29.5m, new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4957), true, false, "Ghast Mood Lamp", null },
                    { new Guid("e5f67890-abcd-ef12-3456-7890abcdef12"), 14.0m, new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4958), true, false, "Piggy Bank (Pig)", null },
                    { new Guid("f67890ab-cdef-1234-5678-90abcdef1234"), 18.75m, new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4959), true, false, "Iron Golem Action Figure", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("01234567-89ab-cdef-0123-456789abcdef"));

            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("7890abcd-ef12-3456-7890-abcdef123456"));

            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("890abcde-f123-4567-890a-bcdef1234567"));

            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("90abcdef-1234-5678-90ab-cdef12345678"));

            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f678-90ab-cdef-1234567890ab"));

            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-7890-abcd-ef12-34567890abcd"));

            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f678-90ab-cdef-1234-567890abcdef"));

            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("e5f67890-abcd-ef12-3456-7890abcdef12"));

            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("f67890ab-cdef-1234-5678-90abcdef1234"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Skins",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440000"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 20, 10, 7, 4, 996, DateTimeKind.Utc).AddTicks(2530));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("67f12345-a1b2-c3d4-e5f6-7890abcdef12"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 20, 10, 7, 4, 996, DateTimeKind.Utc).AddTicks(2533));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 20, 10, 7, 4, 996, DateTimeKind.Utc).AddTicks(2535));
        }
    }
}
