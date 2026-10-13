using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuznNat_Kt_23.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cd_discipline",
                columns: table => new
                {
                    discipline_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор записи дисциплины")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Название дисциплины"),
                    f_is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Флаг мягкого удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_discipline_discipline_id", x => x.discipline_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_speciality",
                columns: table => new
                {
                    speciality_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор записи специальности")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_title = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Название специальности"),
                    c_code = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Код специальности")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_speciality_speciality_id", x => x.speciality_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_group",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор записи группы")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_group_name = table.Column<string>(type: "nvarchar(max)", maxLength: 100, nullable: false, comment: "Название группы"),
                    c_group_course = table.Column<int>(type: "int", nullable: false),
                    f_specality_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор специальности"),
                    f_is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Флаг мягкого удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_group_group_id", x => x.group_id);
                    table.ForeignKey(
                        name: "fk_f_speciality_id",
                        column: x => x.f_specality_id,
                        principalTable: "cd_speciality",
                        principalColumn: "speciality_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cd_student",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор записи студента")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_student_firstname = table.Column<string>(type: "nvarchar(max)", maxLength: 100, nullable: false, comment: "Имя студента"),
                    c_student_lastname = table.Column<string>(type: "nvarchar(max)", maxLength: 100, nullable: false, comment: "Фамилия студента"),
                    f_group_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор группы"),
                    f_is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Флаг мягкого удаления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_student_student_id", x => x.student_id);
                    table.ForeignKey(
                        name: "fk_f_group_id",
                        column: x => x.f_group_id,
                        principalTable: "cd_group",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cd_grade",
                columns: table => new
                {
                    grade_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор записи оценки")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_value = table.Column<int>(type: "int", nullable: false, comment: "оценка"),
                    f_student_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор студента"),
                    f_discipline_id = table.Column<int>(type: "int", nullable: false, comment: "Идентификатор дисциплины")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_grade_grade_id", x => x.grade_id);
                    table.ForeignKey(
                        name: "fk_f_discipline_id",
                        column: x => x.f_discipline_id,
                        principalTable: "cd_discipline",
                        principalColumn: "discipline_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_f_student_id",
                        column: x => x.f_student_id,
                        principalTable: "cd_student",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "idx_cd_grade_fk_f_discipline_id",
                table: "cd_grade",
                column: "f_discipline_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_grade_fk_f_student_id",
                table: "cd_grade",
                column: "f_student_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_group_fk_f_specality_id",
                table: "cd_group",
                column: "f_specality_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_student_fk_f_group_id",
                table: "cd_student",
                column: "f_group_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_grade");

            migrationBuilder.DropTable(
                name: "cd_discipline");

            migrationBuilder.DropTable(
                name: "cd_student");

            migrationBuilder.DropTable(
                name: "cd_group");

            migrationBuilder.DropTable(
                name: "cd_speciality");
        }
    }
}
