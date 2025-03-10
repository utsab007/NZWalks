using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class seedingdataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("331238bb-bc64-447c-b29d-21b9998ec3cd"), "Hard" },
                    { new Guid("a2f36d39-fc69-4464-aed6-79a3c8f62eb9"), "Easy" },
                    { new Guid("f718d1e2-40c7-4b34-bfc5-488b694c4b19"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("f1b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"), "NOR", "Northland", "https://www.doc.govt.nz/globalassets/images/regions/northland/northland-landscape-2.jpg" },
                    { new Guid("f2b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"), "ACK", "Auckland", "https://www.doc.govt.nz/globalassets/images/regions/auckland/auckland-landscape-1.jpg" },
                    { new Guid("f3b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"), "WAI", "Waikato", "https://www.doc.govt.nz/globalassets/images/regions/waikato/waikato-landscape-1.jpg" },
                    { new Guid("f4b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"), "BOP", "Bay of Plenty", "https://www.doc.govt.nz/globalassets/images/regions/bay-of-plenty/bay-of-plenty-landscape-1.jpg" },
                    { new Guid("f5b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"), "TAR", "Taranaki", "https://www.doc.govt.nz/globalassets/images/regions/taranaki/taranaki-landscape-1.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("331238bb-bc64-447c-b29d-21b9998ec3cd"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a2f36d39-fc69-4464-aed6-79a3c8f62eb9"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f718d1e2-40c7-4b34-bfc5-488b694c4b19"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f1b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f2b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f3b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f4b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f5b3b3b4-1b3b-4b3b-8b3b-3b3b3b3b3b3b"));
        }
    }
}
