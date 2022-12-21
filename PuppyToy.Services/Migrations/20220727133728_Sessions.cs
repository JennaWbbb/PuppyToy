using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuppyToy.Services.Migrations {
    public partial class Sessions : Migration {
        protected override void Up( MigrationBuilder migrationBuilder ) {
            migrationBuilder.AddColumn<Guid>(
                name: "EmlalockSessionId",
                table: "EmlalockFeedItems",
                type: "TEXT",
                nullable: true );

            migrationBuilder.CreateTable(
                name: "EmlalockSessions",
                columns: table => new {
                    Id = table.Column<Guid>( type: "TEXT", nullable: false ),
                    SessionId = table.Column<string>( type: "TEXT", nullable: false )
                },
                constraints: table => {
                    table.PrimaryKey( "PK_EmlalockSessions", x => x.Id );
                } );

            migrationBuilder.CreateIndex(
                name: "IX_EmlalockFeedItems_EmlalockSessionId",
                table: "EmlalockFeedItems",
                column: "EmlalockSessionId" );

            migrationBuilder.AddForeignKey(
                name: "FK_EmlalockFeedItems_EmlalockSessions_EmlalockSessionId",
                table: "EmlalockFeedItems",
                column: "EmlalockSessionId",
                principalTable: "EmlalockSessions",
                principalColumn: "Id" );
        }

        protected override void Down( MigrationBuilder migrationBuilder ) {
            migrationBuilder.DropForeignKey(
                name: "FK_EmlalockFeedItems_EmlalockSessions_EmlalockSessionId",
                table: "EmlalockFeedItems" );

            migrationBuilder.DropTable(
                name: "EmlalockSessions" );

            migrationBuilder.DropIndex(
                name: "IX_EmlalockFeedItems_EmlalockSessionId",
                table: "EmlalockFeedItems" );

            migrationBuilder.DropColumn(
                name: "EmlalockSessionId",
                table: "EmlalockFeedItems" );
        }
    }
}
