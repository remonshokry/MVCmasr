using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCmasr.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenre_Albums_AlbumsId",
                table: "AlbumGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenre_Genre_GenresId",
                table: "AlbumGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistRole_Artists_ArtistsId",
                table: "ArtistRole");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistRole_Role_RolesId",
                table: "ArtistRole");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "ArtistRole",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "ArtistsId",
                table: "ArtistRole",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistRole_RolesId",
                table: "ArtistRole",
                newName: "IX_ArtistRole_RoleId");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "AlbumGenre",
                newName: "GenreId");

            migrationBuilder.RenameColumn(
                name: "AlbumsId",
                table: "AlbumGenre",
                newName: "AlbumId");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumGenre_GenresId",
                table: "AlbumGenre",
                newName: "IX_AlbumGenre_GenreId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistRole_Artists_ArtistId",
                table: "ArtistRole",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistRole_Role_RoleId",
                table: "ArtistRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenre_Albums_AlbumId",
                table: "AlbumGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumGenre_Genre_GenreId",
                table: "AlbumGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistRole_Artists_ArtistId",
                table: "ArtistRole");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistRole_Role_RoleId",
                table: "ArtistRole");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "ArtistRole",
                newName: "RolesId");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "ArtistRole",
                newName: "ArtistsId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistRole_RoleId",
                table: "ArtistRole",
                newName: "IX_ArtistRole_RolesId");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "AlbumGenre",
                newName: "GenresId");

            migrationBuilder.RenameColumn(
                name: "AlbumId",
                table: "AlbumGenre",
                newName: "AlbumsId");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumGenre_GenreId",
                table: "AlbumGenre",
                newName: "IX_AlbumGenre_GenresId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumGenre_Albums_AlbumsId",
                table: "AlbumGenre",
                column: "AlbumsId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumGenre_Genre_GenresId",
                table: "AlbumGenre",
                column: "GenresId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistRole_Artists_ArtistsId",
                table: "ArtistRole",
                column: "ArtistsId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistRole_Role_RolesId",
                table: "ArtistRole",
                column: "RolesId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
