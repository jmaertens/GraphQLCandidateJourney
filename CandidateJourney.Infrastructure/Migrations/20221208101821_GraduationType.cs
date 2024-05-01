using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateJourney.Infrastructure.Migrations
{
    public partial class GraduationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TemplateName",
                table: "EmailTemplates",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateId",
                table: "EmailTemplates",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ExtraInfo",
                table: "Candidate",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GraduationType",
                table: "Candidate",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmailTemplates_TemplateId",
                table: "EmailTemplates");

            migrationBuilder.DropIndex(
                name: "IX_EmailTemplates_TemplateName",
                table: "EmailTemplates");


            migrationBuilder.DropColumn(
                name: "ExtraInfo",
                table: "Candidate");

            migrationBuilder.DropColumn(
                name: "GraduationType",
                table: "Candidate");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateName",
                table: "EmailTemplates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateId",
                table: "EmailTemplates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
