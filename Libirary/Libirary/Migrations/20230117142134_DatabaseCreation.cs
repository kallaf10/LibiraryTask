using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Libirary.Migrations
{
    public partial class DatabaseCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BSN = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    bookTitle = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    autorName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    copies = table.Column<int>(type: "int", nullable: true),
                    borrowed = table.Column<int>(type: "int", nullable: true),
                    availableCopiesToborrow = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BSN);
                });

            migrationBuilder.Sql("insert into Books(autorName,bookTitle,borrowed,copies,availableCopiesToborrow)\r\nvalues('Carlos Zafon','The Labyrinth of the Spirits',0,8,8)");
            migrationBuilder.Sql("insert into Books(autorName,bookTitle,borrowed,copies,availableCopiesToborrow)\r\nvalues('Carlos Zafon','Shadow of The wind',0,7,7)");
            migrationBuilder.Sql("insert into Books(autorName,bookTitle,borrowed,copies,availableCopiesToborrow)\r\nvalues('Carlos Zafon','The Angel Game',0,5,5)");
            migrationBuilder.Sql("insert into Books(autorName,bookTitle,borrowed,copies,availableCopiesToborrow)\r\nvalues('Carlos Zafon','Prisoner Of The Sky',0,5,5)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
            migrationBuilder.Sql("Delete from Books where autorName='Carlos Zafon'");
        }
    }
}
