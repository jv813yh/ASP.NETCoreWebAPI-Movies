using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Movies.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonId", "Biography", "BirthDate", "Country", "Name", "Role" },
                values: new object[,]
                {
                    { 1L, "William Bradley \"Brad\" Pitt is an American film actor and producer, winner of two Golden Globe Awards for Best Supporting Actor for his performance in Twelve Monkeys and Once Upon a Time in Hollywood.", new DateTime(1963, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Brad Pitt", 0 },
                    { 2L, "Quentin Jerome Tarantino is an American film director, screenwriter and actor who became famous in the early 1990s as a fresh, harsh and darkly humorous storyteller who brought new life to traditional American archetypes.", new DateTime(1963, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Quentin Tarantino", 1 },
                    { 3L, "Leonardo Wilhelm DiCaprio is an American actor, film producer, and environmentalist. He has often played unconventional roles, particularly in biopics and period films.", new DateTime(1974, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Leonardo DiCaprio", 0 },
                    { 4L, "Christopher Edward Nolan is a British-American film director, producer, and screenwriter. His films have grossed over $5 billion worldwide, and he is one of the highest-grossing directors in history.", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "UK", "Christopher Nolan", 1 },
                    { 5L, "Christian Charles Philip Bale is an English actor. Known for his versatility and intensive method acting, he is the recipient of many awards, including an Academy Award and two Golden Globe Awards.", new DateTime(1974, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "UK", "Christian Bale", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
