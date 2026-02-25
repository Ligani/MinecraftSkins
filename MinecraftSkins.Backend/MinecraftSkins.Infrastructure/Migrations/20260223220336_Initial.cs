using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinecraftSkins.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("01234567-89ab-cdef-0123-456789abcdef"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 23, 22, 3, 35, 595, DateTimeKind.Utc).AddTicks(2406));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("550e8400-e29b-41d4-a716-446655440000"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 23, 22, 3, 35, 595, DateTimeKind.Utc).AddTicks(2386));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("67f12345-a1b2-c3d4-e5f6-7890abcdef12"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 23, 22, 3, 35, 595, DateTimeKind.Utc).AddTicks(2393));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("7890abcd-ef12-3456-7890-abcdef123456"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 23, 22, 3, 35, 595, DateTimeKind.Utc).AddTicks(2402));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("890abcde-f123-4567-890a-bcdef1234567"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 23, 22, 3, 35, 595, DateTimeKind.Utc).AddTicks(2403));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("90abcdef-1234-5678-90ab-cdef12345678"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 23, 22, 3, 35, 595, DateTimeKind.Utc).AddTicks(2404));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 23, 22, 3, 35, 595, DateTimeKind.Utc).AddTicks(2395));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f678-90ab-cdef-1234567890ab"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 23, 22, 3, 35, 595, DateTimeKind.Utc).AddTicks(2396));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-7890-abcd-ef12-34567890abcd"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 23, 22, 3, 35, 595, DateTimeKind.Utc).AddTicks(2397));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f678-90ab-cdef-1234-567890abcdef"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 23, 22, 3, 35, 595, DateTimeKind.Utc).AddTicks(2398));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("e5f67890-abcd-ef12-3456-7890abcdef12"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 23, 22, 3, 35, 595, DateTimeKind.Utc).AddTicks(2400));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("f67890ab-cdef-1234-5678-90abcdef1234"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 23, 22, 3, 35, 595, DateTimeKind.Utc).AddTicks(2401));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("01234567-89ab-cdef-0123-456789abcdef"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4964));

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
                keyValue: new Guid("7890abcd-ef12-3456-7890-abcdef123456"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4961));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("890abcde-f123-4567-890a-bcdef1234567"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4962));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("90abcdef-1234-5678-90ab-cdef12345678"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4963));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4917));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-f678-90ab-cdef-1234567890ab"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4918));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("c3d4e5f6-7890-abcd-ef12-34567890abcd"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4956));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f678-90ab-cdef-1234-567890abcdef"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4957));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("e5f67890-abcd-ef12-3456-7890abcdef12"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4958));

            migrationBuilder.UpdateData(
                table: "Skins",
                keyColumn: "Id",
                keyValue: new Guid("f67890ab-cdef-1234-5678-90abcdef1234"),
                column: "CreatedAtUtc",
                value: new DateTime(2026, 2, 21, 12, 36, 17, 313, DateTimeKind.Utc).AddTicks(4959));
        }
    }
}
