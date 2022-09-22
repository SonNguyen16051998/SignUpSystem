using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignupSystem.Migrations
{
    public partial class signupsystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Code_Course = table.Column<string>(type: "varchar(100)", nullable: false),
                    NameCoure = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Code_Course);
                });

            migrationBuilder.CreateTable(
                name: "HolidaySchedules",
                columns: table => new
                {
                    Id_Holiday = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HolidayName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidaySchedules", x => x.Id_Holiday);
                });

            migrationBuilder.CreateTable(
                name: "Khoa",
                columns: table => new
                {
                    Id_Khoa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_Khoa = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Khoa", x => x.Id_Khoa);
                });

            migrationBuilder.CreateTable(
                name: "PointTypes",
                columns: table => new
                {
                    Id_PointType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    HeSo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointTypes", x => x.Id_PointType);
                });

            migrationBuilder.CreateTable(
                name: "Quyens",
                columns: table => new
                {
                    Id_Quyen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quyens", x => x.Id_Quyen);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id_Quyen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameQuyen = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id_Quyen);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id_Student = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PhoneNo = table.Column<string>(type: "varchar(15)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    ParentName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    PassWord = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id_Student);
                });

            migrationBuilder.CreateTable(
                name: "SubjectDepartments",
                columns: table => new
                {
                    Id_SubjectDerpartment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject_DepartmentName = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectDepartments", x => x.Id_SubjectDerpartment);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id_Class = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code_Course = table.Column<string>(type: "varchar(100)", nullable: true),
                    Id_Khoa = table.Column<int>(type: "int", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    QtyStudent = table.Column<int>(type: "int", nullable: false),
                    QtyStudentExist = table.Column<int>(type: "int", nullable: true),
                    Fee = table.Column<float>(type: "real", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    IsChieuSinh = table.Column<bool>(type: "bit", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id_Class);
                    table.ForeignKey(
                        name: "FK_Classes_Courses_Code_Course",
                        column: x => x.Code_Course,
                        principalTable: "Courses",
                        principalColumn: "Code_Course",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_Khoa_Id_Khoa",
                        column: x => x.Id_Khoa,
                        principalTable: "Khoa",
                        principalColumn: "Id_Khoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role_Quyens",
                columns: table => new
                {
                    Id_Role = table.Column<int>(type: "int", nullable: false),
                    Id_Quyen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Quyens", x => new { x.Id_Role, x.Id_Quyen });
                    table.ForeignKey(
                        name: "FK_Role_Quyens_Quyens_Id_Quyen",
                        column: x => x.Id_Quyen,
                        principalTable: "Quyens",
                        principalColumn: "Id_Quyen",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_Quyens_Roles_Id_Role",
                        column: x => x.Id_Role,
                        principalTable: "Roles",
                        principalColumn: "Id_Quyen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id_User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    Id_Role = table.Column<int>(type: "int", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    PassWord = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id_User);
                    table.ForeignKey(
                        name: "FK_Users_Roles_Id_Role",
                        column: x => x.Id_Role,
                        principalTable: "Roles",
                        principalColumn: "Id_Quyen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Code_Subject = table.Column<string>(type: "varchar(20)", nullable: false),
                    NameSubject = table.Column<string>(type: "varchar(30)", nullable: false),
                    Id_SubjectDerpartment = table.Column<int>(type: "int", nullable: false),
                    Id_Khoa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Code_Subject);
                    table.ForeignKey(
                        name: "FK_Subjects_Khoa_Id_Khoa",
                        column: x => x.Id_Khoa,
                        principalTable: "Khoa",
                        principalColumn: "Id_Khoa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subjects_SubjectDepartments_Id_SubjectDerpartment",
                        column: x => x.Id_SubjectDerpartment,
                        principalTable: "SubjectDepartments",
                        principalColumn: "Id_SubjectDerpartment",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fees",
                columns: table => new
                {
                    Id_Fee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Class = table.Column<int>(type: "int", nullable: false),
                    TypeOfFee = table.Column<int>(type: "int", nullable: false),
                    FeeRate = table.Column<float>(type: "real", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Id_Student = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fees", x => x.Id_Fee);
                    table.ForeignKey(
                        name: "FK_Fees_Classes_Id_Class",
                        column: x => x.Id_Class,
                        principalTable: "Classes",
                        principalColumn: "Id_Class",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fees_Students_Id_Student",
                        column: x => x.Id_Student,
                        principalTable: "Students",
                        principalColumn: "Id_Student",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users_Quyen",
                columns: table => new
                {
                    Id_User = table.Column<int>(type: "int", nullable: false),
                    Id_Quyen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Quyen", x => new { x.Id_User, x.Id_Quyen });
                    table.ForeignKey(
                        name: "FK_Users_Quyen_Quyens_Id_Quyen",
                        column: x => x.Id_Quyen,
                        principalTable: "Quyens",
                        principalColumn: "Id_Quyen",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Quyen_Users_Id_User",
                        column: x => x.Id_User,
                        principalTable: "Users",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student_Points",
                columns: table => new
                {
                    Id_Point = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code_Subject = table.Column<string>(type: "varchar(20)", nullable: true),
                    Id_Student = table.Column<int>(type: "int", nullable: false),
                    Id_PointType = table.Column<int>(type: "int", nullable: false),
                    Point = table.Column<float>(type: "real", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Points", x => x.Id_Point);
                    table.ForeignKey(
                        name: "FK_Student_Points_PointTypes_Id_PointType",
                        column: x => x.Id_PointType,
                        principalTable: "PointTypes",
                        principalColumn: "Id_PointType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Points_Students_Id_Student",
                        column: x => x.Id_Student,
                        principalTable: "Students",
                        principalColumn: "Id_Student",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Points_Subjects_Code_Subject",
                        column: x => x.Code_Subject,
                        principalTable: "Subjects",
                        principalColumn: "Code_Subject",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subject_Pointypes",
                columns: table => new
                {
                    Code_Course = table.Column<string>(type: "varchar(20)", nullable: false),
                    Code_Subject = table.Column<string>(type: "varchar(20)", nullable: false),
                    Id_PointType = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    QtyRequied = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject_Pointypes", x => new { x.Code_Subject, x.Id_PointType });
                    table.ForeignKey(
                        name: "FK_Subject_Pointypes_PointTypes_Id_PointType",
                        column: x => x.Id_PointType,
                        principalTable: "PointTypes",
                        principalColumn: "Id_PointType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subject_Pointypes_Subjects_Code_Subject",
                        column: x => x.Code_Subject,
                        principalTable: "Subjects",
                        principalColumn: "Code_Subject",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id_Teacher = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PhoneNo = table.Column<string>(type: "varchar(15)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Code_MainSubject = table.Column<string>(type: "varchar(20)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    PassWord = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id_Teacher);
                    table.ForeignKey(
                        name: "FK_Teachers_Subjects_Code_MainSubject",
                        column: x => x.Code_MainSubject,
                        principalTable: "Subjects",
                        principalColumn: "Code_Subject",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salarys",
                columns: table => new
                {
                    Id_Salary = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Teacher = table.Column<int>(type: "int", nullable: false),
                    TeacherSalary = table.Column<int>(type: "int", nullable: false),
                    SalaryRecevied = table.Column<float>(type: "real", nullable: false),
                    TroCap = table.Column<float>(type: "real", nullable: false),
                    TotalSalary = table.Column<float>(type: "real", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salarys", x => x.Id_Salary);
                    table.ForeignKey(
                        name: "FK_Salarys_Teachers_Id_Teacher",
                        column: x => x.Id_Teacher,
                        principalTable: "Teachers",
                        principalColumn: "Id_Teacher",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSchedules",
                columns: table => new
                {
                    Id_Schedule = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Class = table.Column<int>(type: "int", nullable: false),
                    Code_Subject = table.Column<string>(type: "varchar(20)", nullable: true),
                    Id_Teacher = table.Column<int>(type: "int", nullable: false),
                    Classroom = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Mon = table.Column<bool>(type: "bit", nullable: false),
                    Tue = table.Column<bool>(type: "bit", nullable: false),
                    Wed = table.Column<bool>(type: "bit", nullable: false),
                    Thu = table.Column<bool>(type: "bit", nullable: false),
                    Fri = table.Column<bool>(type: "bit", nullable: false),
                    Sat = table.Column<bool>(type: "bit", nullable: false),
                    Sun = table.Column<bool>(type: "bit", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    StartDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDay = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSchedules", x => x.Id_Schedule);
                    table.ForeignKey(
                        name: "FK_TeacherSchedules_Classes_Id_Class",
                        column: x => x.Id_Class,
                        principalTable: "Classes",
                        principalColumn: "Id_Class",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSchedules_Subjects_Code_Subject",
                        column: x => x.Code_Subject,
                        principalTable: "Subjects",
                        principalColumn: "Code_Subject",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherSchedules_Teachers_Id_Teacher",
                        column: x => x.Id_Teacher,
                        principalTable: "Teachers",
                        principalColumn: "Id_Teacher",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Student_Classes",
                columns: table => new
                {
                    Id_Student = table.Column<int>(type: "int", nullable: false),
                    Id_Class = table.Column<int>(type: "int", nullable: false),
                    Id_ScheduleTeacher = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Classes", x => new { x.Id_Class, x.Id_Student, x.Id_ScheduleTeacher });
                    table.ForeignKey(
                        name: "FK_Student_Classes_Classes_Id_Class",
                        column: x => x.Id_Class,
                        principalTable: "Classes",
                        principalColumn: "Id_Class",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Classes_Students_Id_Student",
                        column: x => x.Id_Student,
                        principalTable: "Students",
                        principalColumn: "Id_Student",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Classes_TeacherSchedules_Id_ScheduleTeacher",
                        column: x => x.Id_ScheduleTeacher,
                        principalTable: "TeacherSchedules",
                        principalColumn: "Id_Schedule",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_Code_Course",
                table: "Classes",
                column: "Code_Course");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_Id_Khoa",
                table: "Classes",
                column: "Id_Khoa");

            migrationBuilder.CreateIndex(
                name: "IX_Fees_Id_Class",
                table: "Fees",
                column: "Id_Class");

            migrationBuilder.CreateIndex(
                name: "IX_Fees_Id_Student",
                table: "Fees",
                column: "Id_Student");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Quyens_Id_Quyen",
                table: "Role_Quyens",
                column: "Id_Quyen");

            migrationBuilder.CreateIndex(
                name: "IX_Salarys_Id_Teacher",
                table: "Salarys",
                column: "Id_Teacher");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Classes_Id_ScheduleTeacher",
                table: "Student_Classes",
                column: "Id_ScheduleTeacher");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Classes_Id_Student",
                table: "Student_Classes",
                column: "Id_Student");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Points_Code_Subject",
                table: "Student_Points",
                column: "Code_Subject");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Points_Id_PointType",
                table: "Student_Points",
                column: "Id_PointType");

            migrationBuilder.CreateIndex(
                name: "IX_Student_Points_Id_Student",
                table: "Student_Points",
                column: "Id_Student");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_Pointypes_Id_PointType",
                table: "Subject_Pointypes",
                column: "Id_PointType");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Id_Khoa",
                table: "Subjects",
                column: "Id_Khoa");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Id_SubjectDerpartment",
                table: "Subjects",
                column: "Id_SubjectDerpartment");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_Code_MainSubject",
                table: "Teachers",
                column: "Code_MainSubject");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSchedules_Code_Subject",
                table: "TeacherSchedules",
                column: "Code_Subject");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSchedules_Id_Class",
                table: "TeacherSchedules",
                column: "Id_Class");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSchedules_Id_Teacher",
                table: "TeacherSchedules",
                column: "Id_Teacher");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_Role",
                table: "Users",
                column: "Id_Role");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Quyen_Id_Quyen",
                table: "Users_Quyen",
                column: "Id_Quyen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fees");

            migrationBuilder.DropTable(
                name: "HolidaySchedules");

            migrationBuilder.DropTable(
                name: "Role_Quyens");

            migrationBuilder.DropTable(
                name: "Salarys");

            migrationBuilder.DropTable(
                name: "Student_Classes");

            migrationBuilder.DropTable(
                name: "Student_Points");

            migrationBuilder.DropTable(
                name: "Subject_Pointypes");

            migrationBuilder.DropTable(
                name: "Users_Quyen");

            migrationBuilder.DropTable(
                name: "TeacherSchedules");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "PointTypes");

            migrationBuilder.DropTable(
                name: "Quyens");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Khoa");

            migrationBuilder.DropTable(
                name: "SubjectDepartments");
        }
    }
}
