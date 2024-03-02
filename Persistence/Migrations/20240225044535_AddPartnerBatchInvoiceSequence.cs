using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPartnerBatchInvoiceSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "PartnerBatchInvoiceSequence",
                schema: "partners",
                startValue: 1,
                incrementBy: 1
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "PartnerBatchInvoiceSequence",
                schema: "partners"
            );
        }
    }
}
