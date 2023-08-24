using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodOrdering.Persistence.Migrations
{
    public partial class AddAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Address], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'229cea89-85ba-4bae-846d-4933f5096b35', N'minya', N'Admin', N'ADMIN', N'AdminRestaurant@gmail.com', N'ADMINRESTAURANT@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEEPktQQQB9g0EtaXWUcHgUjkGLj5JB9ksRHV+70ngszuNjICoHPKwTOdSehgSsQ3DA==', N'RM2S5TXJOPYON5LIN6DHPJVDPHDWPGDH', N'1a816640-0048-41f3-a9a3-65fc0c9ee7b7', NULL, 0, 0, NULL, 1, 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUsers] WHERE Id='229cea89-85ba-4bae-846d-4933f5096b35'");
        }
    }
}
