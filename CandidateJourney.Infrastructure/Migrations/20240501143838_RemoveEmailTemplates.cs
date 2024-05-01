using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateJourney.Infrastructure.Migrations
{
    public partial class RemoveEmailTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactHistories_EmailTemplates_EmailTemplateId",
                table: "ContactHistories");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ContactHistories_EmailTemplateId",
                table: "ContactHistories");

            migrationBuilder.DropColumn(
                name: "EmailTemplateId",
                table: "ContactHistories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmailTemplateId",
                table: "ContactHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TemplateName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactHistories_EmailTemplateId",
                table: "ContactHistories",
                column: "EmailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_TemplateId",
                table: "EmailTemplates",
                column: "TemplateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_TemplateName",
                table: "EmailTemplates",
                column: "TemplateName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactHistories_EmailTemplates_EmailTemplateId",
                table: "ContactHistories",
                column: "EmailTemplateId",
                principalTable: "EmailTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
