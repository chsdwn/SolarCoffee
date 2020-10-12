using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SolarCoffee.Data.Migrations
{
    public partial class FixedTableNameTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAdresses_PrimaryAddressId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "CustomerAdresses");

            migrationBuilder.CreateTable(
                name: "CustomerAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AddressLine = table.Column<string>(maxLength: 100, nullable: true),
                    AddressLine2 = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    State = table.Column<string>(maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 5, nullable: true),
                    Country = table.Column<string>(maxLength: 32, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddresses", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAddresses_PrimaryAddressId",
                table: "Customers",
                column: "PrimaryAddressId",
                principalTable: "CustomerAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAddresses_PrimaryAddressId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "CustomerAddresses");

            migrationBuilder.CreateTable(
                name: "CustomerAdresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AddressLine = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AddressLine2 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    State = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAdresses", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAdresses_PrimaryAddressId",
                table: "Customers",
                column: "PrimaryAddressId",
                principalTable: "CustomerAdresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
