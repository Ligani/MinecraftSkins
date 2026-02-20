using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MinecraftSkins.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Skins",
                columns: new[] { "Id", "BasePriceUsd", "CreatedAtUtc", "IsAvailable", "IsDeleted", "Name", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440000"), 10.0m, new DateTime(2026, 2, 20, 10, 7, 4, 996, DateTimeKind.Utc).AddTicks(2530), true, false, "Steve Classic", null },
                    { new Guid("67f12345-a1b2-c3d4-e5f6-7890abcdef12"), 15.5m, new DateTime(2026, 2, 20, 10, 7, 4, 996, DateTimeKind.Utc).AddTicks(2533), true, false, "Creeper Hoodie", null },
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), 15.5m, new DateTime(2026, 2, 20, 10, 7, 4, 996, DateTimeKind.Utc).AddTicks(2535), true, false, "Redstone Automaton", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440000"));

            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("67f12345-a1b2-c3d4-e5f6-7890abcdef12"));

            migrationBuilder.DeleteData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"));
        }
    }
}
