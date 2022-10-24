using Food_Restaurant.DAL.Constant;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food_Restaurant.DAL.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table:"AspNetRoles",
                columns: new[] {"Id","Name","NormalizedName", "ConcurrencyStamp"},
                values:new object[] {Guid.NewGuid().ToString(),Roles.userrole, Roles.userrole.ToUpper(), Guid.NewGuid().ToString() }
            );
            migrationBuilder.InsertData(
               table: "AspNetRoles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[] { Guid.NewGuid().ToString(), Roles.adminrole, Roles.adminrole.ToUpper(), Guid.NewGuid().ToString() }
           );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AspNetRoles");
        }
    }
}
