using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCmasr.Migrations
{
    public partial class updateAlbumGebreV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenres_Albums_AlbumId",
                table: "AlbumGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenres_Genre_GenreId",
                table: "AlbumGenres");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumGenres_Albums_GenreId",
                table: "AlbumGenres",
                column: "GenreId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumGenres_Genre_AlbumId",
                table: "AlbumGenres",
                column: "AlbumId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenres_Albums_GenreId",
                table: "AlbumGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenres_Genre_AlbumId",
                table: "AlbumGenres");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumGenres_Albums_AlbumId",
                table: "AlbumGenres",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumGenres_Genre_GenreId",
                table: "AlbumGenres",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
