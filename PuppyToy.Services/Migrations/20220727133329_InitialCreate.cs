using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuppyToy.Services.Migrations {
    public partial class InitialCreate : Migration {
        protected override void Up( MigrationBuilder migrationBuilder ) {
            migrationBuilder.CreateTable(
                name: "EmlalockFeedItems",
                columns: table => new {
                    Id = table.Column<Guid>( type: "TEXT", nullable: false ),
                    Title = table.Column<string>( type: "TEXT", nullable: true ),
                    PupDate = table.Column<DateTime>( type: "TEXT", nullable: false ),
                    ExternalId = table.Column<string>( type: "TEXT", nullable: true ),
                    ActionType = table.Column<int>( type: "INTEGER", nullable: false ),
                    TimeDelta = table.Column<int>( type: "INTEGER", nullable: false ),
                    TimeUnit = table.Column<string>( type: "TEXT", nullable: true )
                },
                constraints: table => {
                    table.PrimaryKey( "PK_EmlalockFeedItems", x => x.Id );
                } );
        }

        protected override void Down( MigrationBuilder migrationBuilder ) {
            migrationBuilder.DropTable(
                name: "EmlalockFeedItems" );
        }
    }
}
