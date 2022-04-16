using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCmasr.Migrations
{
    public partial class updateAlbumGebreV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenre_Albums_AlbumId",
                table: "AlbumGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenre_Genre_GenreId",
                table: "AlbumGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlbumGenre",
                table: "AlbumGenre");

            migrationBuilder.RenameTable(
                name: "AlbumGenre",
                newName: "AlbumGenres");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumGenre_GenreId",
                table: "AlbumGenres",
                newName: "IX_AlbumGenres_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlbumGenres",
                table: "AlbumGenres",
                columns: new[] { "AlbumId", "GenreId" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenres_Albums_AlbumId",
                table: "AlbumGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenres_Genre_GenreId",
                table: "AlbumGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlbumGenres",
                table: "AlbumGenres");

            migrationBuilder.RenameTable(
                name: "AlbumGenres",
                newName: "AlbumGenre");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumGenres_GenreId",
                table: "AlbumGenre",
                newName: "IX_AlbumGenre_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlbumGenre",
                table: "AlbumGenre",
                columns: new[] { "AlbumId", "GenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumGenre_Albums_AlbumId",
                table: "AlbumGenre",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumGenre_Genre_GenreId",
                table: "AlbumGenre",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
