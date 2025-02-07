using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeServiceApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordSaltToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "homeservice");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "homeservice",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    preferences = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workers",
                schema: "homeservice",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    photo = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    experience = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    skills = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ratings = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
                    languages = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    availability = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    mobile = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    identity_proof = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    gender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                schema: "homeservice",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    worker_id = table.Column<int>(type: "int", nullable: false),
                    time = table.Column<DateTime>(type: "datetime", nullable: false),
                    location = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    job_details = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.id);
                    table.ForeignKey(
                        name: "FK__bookings__user_i__5070F446",
                        column: x => x.user_id,
                        principalSchema: "homeservice",
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__bookings__worker__5165187F",
                        column: x => x.worker_id,
                        principalSchema: "homeservice",
                        principalTable: "workers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "system_administration",
                schema: "homeservice",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    worker_id = table.Column<int>(type: "int", nullable: true),
                    action_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    action_description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    action_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_administration", x => x.id);
                    table.ForeignKey(
                        name: "FK__system_ad__user___619B8048",
                        column: x => x.user_id,
                        principalSchema: "homeservice",
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__system_ad__worke__628FA481",
                        column: x => x.worker_id,
                        principalSchema: "homeservice",
                        principalTable: "workers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "feedbacks",
                schema: "homeservice",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
                    review = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedbacks", x => x.id);
                    table.ForeignKey(
                        name: "FK__feedbacks__booki__5629CD9C",
                        column: x => x.booking_id,
                        principalSchema: "homeservice",
                        principalTable: "bookings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "payments",
                schema: "homeservice",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    payment_method = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.id);
                    table.ForeignKey(
                        name: "FK__payments__bookin__59063A47",
                        column: x => x.booking_id,
                        principalSchema: "homeservice",
                        principalTable: "bookings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "supports",
                schema: "homeservice",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    booking_id = table.Column<int>(type: "int", nullable: false),
                    worker_id = table.Column<int>(type: "int", nullable: false),
                    issue_description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    resolution = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supports", x => x.id);
                    table.ForeignKey(
                        name: "FK__supports__bookin__5DCAEF64",
                        column: x => x.booking_id,
                        principalSchema: "homeservice",
                        principalTable: "bookings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__supports__user_i__5BE2A6F2",
                        column: x => x.user_id,
                        principalSchema: "homeservice",
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__supports__worker__5CD6CB2B",
                        column: x => x.worker_id,
                        principalSchema: "homeservice",
                        principalTable: "workers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookings_user_id",
                schema: "homeservice",
                table: "bookings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_worker_id",
                schema: "homeservice",
                table: "bookings",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_booking_id",
                schema: "homeservice",
                table: "feedbacks",
                column: "booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_payments_booking_id",
                schema: "homeservice",
                table: "payments",
                column: "booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_supports_booking_id",
                schema: "homeservice",
                table: "supports",
                column: "booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_supports_user_id",
                schema: "homeservice",
                table: "supports",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_supports_worker_id",
                schema: "homeservice",
                table: "supports",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "IX_system_administration_user_id",
                schema: "homeservice",
                table: "system_administration",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_system_administration_worker_id",
                schema: "homeservice",
                table: "system_administration",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "UC_Email",
                schema: "homeservice",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UC_Phone",
                schema: "homeservice",
                table: "users",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UC_Mobile",
                schema: "homeservice",
                table: "workers",
                column: "mobile",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "feedbacks",
                schema: "homeservice");

            migrationBuilder.DropTable(
                name: "payments",
                schema: "homeservice");

            migrationBuilder.DropTable(
                name: "supports",
                schema: "homeservice");

            migrationBuilder.DropTable(
                name: "system_administration",
                schema: "homeservice");

            migrationBuilder.DropTable(
                name: "bookings",
                schema: "homeservice");

            migrationBuilder.DropTable(
                name: "users",
                schema: "homeservice");

            migrationBuilder.DropTable(
                name: "workers",
                schema: "homeservice");
        }
    }
}
