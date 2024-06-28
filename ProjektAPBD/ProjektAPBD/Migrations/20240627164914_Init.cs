using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektAPBD.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyClients",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    KRSNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyClients", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountId);
                });

            migrationBuilder.CreateTable(
                name: "IndividualClients",
                columns: table => new
                {
                    IndividualPersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualClients", x => x.IndividualPersonId);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareCategories",
                columns: table => new
                {
                    SoftwareCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareCategories", x => x.SoftwareCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Softwares",
                columns: table => new
                {
                    SoftwareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoftwareCategoryId = table.Column<int>(type: "int", nullable: false),
                    IsSubscription = table.Column<bool>(type: "bit", nullable: false),
                    IsBoughtInOneTransaction = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softwares", x => x.SoftwareId);
                    table.ForeignKey(
                        name: "FK_Softwares_SoftwareCategories_SoftwareCategoryId",
                        column: x => x.SoftwareCategoryId,
                        principalTable: "SoftwareCategories",
                        principalColumn: "SoftwareCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agreements",
                columns: table => new
                {
                    AgreementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Signed = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoftwareVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftwareId = table.Column<int>(type: "int", nullable: false),
                    Payment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyClientId = table.Column<int>(type: "int", nullable: true),
                    IndividualClientId = table.Column<int>(type: "int", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizationYears = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreements", x => x.AgreementId);
                    table.ForeignKey(
                        name: "FK_Agreements_CompanyClients_CompanyClientId",
                        column: x => x.CompanyClientId,
                        principalTable: "CompanyClients",
                        principalColumn: "CompanyId");
                    table.ForeignKey(
                        name: "FK_Agreements_IndividualClients_IndividualClientId",
                        column: x => x.IndividualClientId,
                        principalTable: "IndividualClients",
                        principalColumn: "IndividualPersonId");
                    table.ForeignKey(
                        name: "FK_Agreements_Softwares_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Softwares",
                        principalColumn: "SoftwareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Software_Discount",
                columns: table => new
                {
                    SoftwareDiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountId = table.Column<int>(type: "int", nullable: false),
                    SoftwareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software_Discount", x => x.SoftwareDiscountId);
                    table.ForeignKey(
                        name: "FK_Software_Discount_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Software_Discount_Softwares_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Softwares",
                        principalColumn: "SoftwareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CompanyClients",
                columns: new[] { "CompanyId", "Address", "CompanyName", "Email", "KRSNumber", "PhoneNumber" },
                values: new object[] { 1, "AdresCKlienta1", "KowalskispZoo", "malpa@wp.pl", "12345678", 123456789 });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "DiscountId", "DateFrom", "DateTo", "Name", "Value" },
                values: new object[] { 1, new DateTime(2024, 6, 26, 14, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 27, 14, 30, 0, 0, DateTimeKind.Unspecified), "Whitethursday", 5 });

            migrationBuilder.InsertData(
                table: "IndividualClients",
                columns: new[] { "IndividualPersonId", "Address", "Email", "FirstName", "IsDeleted", "Pesel", "PhoneNumber", "SecondName" },
                values: new object[] { 1, "AdresIKlienta1", "malpainna@wp.pl", "Jan", false, "03333333333", 111456789, "Kowalski" });

            migrationBuilder.InsertData(
                table: "SoftwareCategories",
                columns: new[] { "SoftwareCategoryId", "Type" },
                values: new object[] { 1, "finanse" });

            migrationBuilder.InsertData(
                table: "Softwares",
                columns: new[] { "SoftwareId", "Description", "IsBoughtInOneTransaction", "IsSubscription", "Name", "Price", "SoftwareCategoryId", "Version" },
                values: new object[] { 1, "description", true, false, "opraogramowanieChad", 1000m, 1, "3.14" });

            migrationBuilder.InsertData(
                table: "Software_Discount",
                columns: new[] { "SoftwareDiscountId", "DiscountId", "SoftwareId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_CompanyClientId",
                table: "Agreements",
                column: "CompanyClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_IndividualClientId",
                table: "Agreements",
                column: "IndividualClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_SoftwareId",
                table: "Agreements",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Software_Discount_DiscountId",
                table: "Software_Discount",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Software_Discount_SoftwareId",
                table: "Software_Discount",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_SoftwareCategoryId",
                table: "Softwares",
                column: "SoftwareCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agreements");

            migrationBuilder.DropTable(
                name: "Software_Discount");

            migrationBuilder.DropTable(
                name: "CompanyClients");

            migrationBuilder.DropTable(
                name: "IndividualClients");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Softwares");

            migrationBuilder.DropTable(
                name: "SoftwareCategories");
        }
    }
}
