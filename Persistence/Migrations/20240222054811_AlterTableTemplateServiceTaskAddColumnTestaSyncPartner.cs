using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableTemplateServiceTaskAddColumnTestaSyncPartner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<char>(
                name: "testa_sync_partner", // Name of the new column
                table: "template_service_task", // Name of the existing table
                schema: "mtr", // Schema of the existing table (if applicable)
                type: "char(1)", // SQL data type of the new column
                nullable: false, // Whether the column allows NULL values
                defaultValue: 'N' // Default value for the new column
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}