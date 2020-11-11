using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestGraphQLWithHotChocolate.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opportunities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opportunities_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Opportunities_SystemUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("64553146-c836-49eb-ad87-a8c639a4f054"), "Microsoft" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("00553146-c836-49eb-ad87-a8c639a4f054"), "Google" });

            migrationBuilder.InsertData(
                table: "SystemUsers",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { new Guid("f0c74a06-1849-4ad7-a28f-3981697c3e71"), "daniel@diaz.com", "Daniel Diaz" });

            migrationBuilder.InsertData(
                table: "Opportunities",
                columns: new[] { "Id", "AccountId", "OwnerId", "Subject" },
                values: new object[] { new Guid("bab9706e-deb7-4b55-a145-79245fad3608"), new Guid("64553146-c836-49eb-ad87-a8c639a4f054"), new Guid("f0c74a06-1849-4ad7-a28f-3981697c3e71"), "Oportunidad 1" });

            migrationBuilder.InsertData(
                table: "Opportunities",
                columns: new[] { "Id", "AccountId", "OwnerId", "Subject" },
                values: new object[] { new Guid("47fb11ed-f63f-48a9-83d8-89cea36e4de3"), new Guid("64553146-c836-49eb-ad87-a8c639a4f054"), new Guid("f0c74a06-1849-4ad7-a28f-3981697c3e71"), "Oportunidad 2" });

            migrationBuilder.InsertData(
                table: "Opportunities",
                columns: new[] { "Id", "AccountId", "OwnerId", "Subject" },
                values: new object[] { new Guid("00fb11ed-f63f-48a9-83d8-89cea36e4de3"), new Guid("00553146-c836-49eb-ad87-a8c639a4f054"), new Guid("f0c74a06-1849-4ad7-a28f-3981697c3e71"), "Oportunidad 3" });

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_AccountId",
                table: "Opportunities",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_OwnerId",
                table: "Opportunities",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opportunities");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "SystemUsers");
        }
    }
}
