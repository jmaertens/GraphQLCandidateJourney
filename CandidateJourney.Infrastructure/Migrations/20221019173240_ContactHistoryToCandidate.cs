using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateJourney.Infrastructure.Migrations
{
    public partial class ContactHistoryToCandidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactHistories_Candidate_CandidateId",
                table: "ContactHistories");

            migrationBuilder.AlterColumn<Guid>(
                name: "CandidateId",
                table: "ContactHistories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactHistories_Candidate_CandidateId",
                table: "ContactHistories",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactHistories_Candidate_CandidateId",
                table: "ContactHistories");

            migrationBuilder.AlterColumn<Guid>(
                name: "CandidateId",
                table: "ContactHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactHistories_Candidate_CandidateId",
                table: "ContactHistories",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
