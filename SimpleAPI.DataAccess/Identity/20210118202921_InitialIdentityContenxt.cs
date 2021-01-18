using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SimpleAPI.DataAccess.Identity
{
    public partial class InitialIdentityContenxt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "_deviceflowcodes",
                schema: "public",
                columns: table => new
                {
                    user_code = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    device_code = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    subject_id = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    session_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    client_id = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    creation_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    expiration = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    data = table.Column<string>(type: "character varying(50000)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk__deviceflowcodes", x => x.user_code);
                });

            migrationBuilder.CreateTable(
                name: "_identityrole",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk__identityrole", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "_persistedgrant",
                schema: "public",
                columns: table => new
                {
                    key = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    subject_id = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    session_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    client_id = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    creation_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    expiration = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    consumed_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    data = table.Column<string>(type: "character varying(50000)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk__persistedgrant", x => x.key);
                });

            migrationBuilder.CreateTable(
                name: "_sccuser",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk__sccuser", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "_identityroleclaim",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk__identityroleclaim", x => x.id);
                    table.ForeignKey(
                        name: "fk__identityroleclaim__identityrole_role_id",
                        column: x => x.role_id,
                        principalSchema: "public",
                        principalTable: "_identityrole",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_identityuserclaim",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk__identityuserclaim", x => x.id);
                    table.ForeignKey(
                        name: "fk__identityuserclaim_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "_sccuser",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_identityuserlogin",
                schema: "public",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    provider_key = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk__identityuserlogin", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk__identityuserlogin_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "_sccuser",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_identityuserrole",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk__identityuserrole", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk__identityuserrole__identityrole_role_id",
                        column: x => x.role_id,
                        principalSchema: "public",
                        principalTable: "_identityrole",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk__identityuserrole_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "_sccuser",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_identityusertoken",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    login_provider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk__identityusertoken", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk__identityusertoken_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "_sccuser",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "_identityrole",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "0", "9f9cc62f-3e6a-4c5e-b108-f83150284f8f", "Anoynomous", "ANOYNOMOUS" },
                    { "1", "c582c939-b9bc-4b5e-9e44-741c54fd9f55", "Admin", "ADMIN" },
                    { "2", "ea4ce75e-4a78-4757-bc7b-d3d4a9e37f63", "Contributor", "CONTRIBUTOR" },
                    { "3", "8665b5e2-0b5f-40c5-9372-5b6b43d6e01c", "Reader", "READER" }
                });

            migrationBuilder.CreateIndex(
                name: "ix__deviceflowcodes_device_code",
                schema: "public",
                table: "_deviceflowcodes",
                column: "device_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix__deviceflowcodes_expiration",
                schema: "public",
                table: "_deviceflowcodes",
                column: "expiration");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "public",
                table: "_identityrole",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix__identityroleclaim_role_id",
                schema: "public",
                table: "_identityroleclaim",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix__identityuserclaim_user_id",
                schema: "public",
                table: "_identityuserclaim",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix__identityuserlogin_user_id",
                schema: "public",
                table: "_identityuserlogin",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix__identityuserrole_role_id",
                schema: "public",
                table: "_identityuserrole",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix__persistedgrant_expiration",
                schema: "public",
                table: "_persistedgrant",
                column: "expiration");

            migrationBuilder.CreateIndex(
                name: "ix__persistedgrant_subject_id_client_id_type",
                schema: "public",
                table: "_persistedgrant",
                columns: new[] { "subject_id", "client_id", "type" });

            migrationBuilder.CreateIndex(
                name: "ix__persistedgrant_subject_id_session_id_type",
                schema: "public",
                table: "_persistedgrant",
                columns: new[] { "subject_id", "session_id", "type" });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "public",
                table: "_sccuser",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "public",
                table: "_sccuser",
                column: "normalized_user_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_deviceflowcodes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "_identityroleclaim",
                schema: "public");

            migrationBuilder.DropTable(
                name: "_identityuserclaim",
                schema: "public");

            migrationBuilder.DropTable(
                name: "_identityuserlogin",
                schema: "public");

            migrationBuilder.DropTable(
                name: "_identityuserrole",
                schema: "public");

            migrationBuilder.DropTable(
                name: "_identityusertoken",
                schema: "public");

            migrationBuilder.DropTable(
                name: "_persistedgrant",
                schema: "public");

            migrationBuilder.DropTable(
                name: "_identityrole",
                schema: "public");

            migrationBuilder.DropTable(
                name: "_sccuser",
                schema: "public");
        }
    }
}
