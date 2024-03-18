using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "secr_id",
            //    schema: "so",
            //    table: "service_premi_credit",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "soft_delete",
                schema: "hr",
                table: "employees",
                type: "nvarchar(max)",
                nullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "eawg_id",
            //    schema: "hr",
            //    table: "employee_are_workgroup",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "soft_delete",
                schema: "hr",
                table: "employee_are_workgroup",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "soft_delete",
                schema: "hr",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "soft_delete",
                schema: "hr",
                table: "employee_are_workgroup");

            migrationBuilder.AlterColumn<int>(
                name: "secr_id",
                schema: "so",
                table: "service_premi_credit",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "eawg_id",
                schema: "hr",
                table: "employee_are_workgroup",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
