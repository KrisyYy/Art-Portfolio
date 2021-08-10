using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtPortfolio.Data.Migrations
{
    public partial class FixDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prop_Commissions_CommissionId",
                table: "Prop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prop",
                table: "Prop");

            migrationBuilder.RenameTable(
                name: "Prop",
                newName: "Props");

            migrationBuilder.RenameIndex(
                name: "IX_Prop_CommissionId",
                table: "Props",
                newName: "IX_Props_CommissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Props",
                table: "Props",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Props_Commissions_CommissionId",
                table: "Props",
                column: "CommissionId",
                principalTable: "Commissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Props_Commissions_CommissionId",
                table: "Props");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Props",
                table: "Props");

            migrationBuilder.RenameTable(
                name: "Props",
                newName: "Prop");

            migrationBuilder.RenameIndex(
                name: "IX_Props_CommissionId",
                table: "Prop",
                newName: "IX_Prop_CommissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prop",
                table: "Prop",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prop_Commissions_CommissionId",
                table: "Prop",
                column: "CommissionId",
                principalTable: "Commissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
