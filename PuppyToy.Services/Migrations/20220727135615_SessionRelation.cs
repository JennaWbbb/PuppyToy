using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuppyToy.Services.Migrations {
    public partial class SessionRelation : Migration {
        protected override void Up( MigrationBuilder migrationBuilder ) {
            migrationBuilder.DropForeignKey(
                name: "FK_EmlalockFeedItems_EmlalockSessions_EmlalockSessionId",
                table: "EmlalockFeedItems" );

            migrationBuilder.DropIndex(
                name: "IX_EmlalockFeedItems_EmlalockSessionId",
                table: "EmlalockFeedItems" );

            migrationBuilder.DropColumn(
                name: "EmlalockSessionId",
                table: "EmlalockFeedItems" );

            migrationBuilder.AddColumn<Guid>(
                name: "SessionId",
                table: "EmlalockFeedItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid( "00000000-0000-0000-0000-000000000000" ) );

            migrationBuilder.CreateIndex(
                name: "IX_EmlalockFeedItems_SessionId",
                table: "EmlalockFeedItems",
                column: "SessionId" );

            migrationBuilder.AddForeignKey(
                name: "FK_EmlalockFeedItems_EmlalockSessions_SessionId",
                table: "EmlalockFeedItems",
                column: "SessionId",
                principalTable: "EmlalockSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade );
        }

        protected override void Down( MigrationBuilder migrationBuilder ) {
            migrationBuilder.DropForeignKey(
                name: "FK_EmlalockFeedItems_EmlalockSessions_SessionId",
                table: "EmlalockFeedItems" );

            migrationBuilder.DropIndex(
                name: "IX_EmlalockFeedItems_SessionId",
                table: "EmlalockFeedItems" );

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "EmlalockFeedItems" );

            migrationBuilder.AddColumn<Guid>(
                name: "EmlalockSessionId",
                table: "EmlalockFeedItems",
                type: "TEXT",
                nullable: true );

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
    }
}
