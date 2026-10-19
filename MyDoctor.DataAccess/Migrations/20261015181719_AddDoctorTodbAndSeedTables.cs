using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyDoctor.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDoctorTodbAndSeedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "DepartmentId", "Description", "Email", "ImageUrl", "Name", "PhoneNumber", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Holder of the British Fellowship. Faculty of Medicine, University of Khartoum.", "rawia@example.com", "/images/doctors/rawia.jpg", "Dr. Rawia Abdelrahman", "+966500000000", "Specialist in Internal Medicine" },
                    { 2, 2, "Experienced pediatric consultant providing comprehensive healthcare for children.", "ahmed@example.com", "/images/doctors/ahmed.jpg", "Dr. Ahmed Mohamed", "+966511111111", "Consultant in Pediatrics" },
                    { 3, 3, "Specialist in diagnosing and treating skin conditions.", "sara@example.com", "/images/doctors/sara.jpg", "Dr. Sara Ali", "+966522222222", "Specialist in Dermatology" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentId",
                table: "Doctors",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
