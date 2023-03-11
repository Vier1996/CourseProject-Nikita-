using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.Migrations
{
    /// <inheritdoc />
    public partial class dddTTTDz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_FormedEducations_FormedEducationReferenceId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Qualifications_QualificationReferenceId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Specialities_SpecialityReferenceId",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "SpecialityReferenceId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QualificationReferenceId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FormedEducationReferenceId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_FormedEducations_FormedEducationReferenceId",
                table: "Groups",
                column: "FormedEducationReferenceId",
                principalTable: "FormedEducations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Qualifications_QualificationReferenceId",
                table: "Groups",
                column: "QualificationReferenceId",
                principalTable: "Qualifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Specialities_SpecialityReferenceId",
                table: "Groups",
                column: "SpecialityReferenceId",
                principalTable: "Specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_FormedEducations_FormedEducationReferenceId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Qualifications_QualificationReferenceId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Specialities_SpecialityReferenceId",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "SpecialityReferenceId",
                table: "Groups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "QualificationReferenceId",
                table: "Groups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FormedEducationReferenceId",
                table: "Groups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_FormedEducations_FormedEducationReferenceId",
                table: "Groups",
                column: "FormedEducationReferenceId",
                principalTable: "FormedEducations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Qualifications_QualificationReferenceId",
                table: "Groups",
                column: "QualificationReferenceId",
                principalTable: "Qualifications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Specialities_SpecialityReferenceId",
                table: "Groups",
                column: "SpecialityReferenceId",
                principalTable: "Specialities",
                principalColumn: "Id");
        }
    }
}
