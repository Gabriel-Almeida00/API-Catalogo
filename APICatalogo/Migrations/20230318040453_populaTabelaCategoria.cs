using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    public partial class populaTabelaCategoria : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("insert into Categorias(Nome, ImageUrl) values ('Bebidas', 'bebidas.jpg')");
            mb.Sql("insert into Categorias(Nome, ImageUrl) values ('Lanches', 'lanche.jpg')");
            mb.Sql("insert into Categorias(Nome, ImageUrl) values ('Sobremesas', 'sobremesa.jpg')");

        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categoria");
        }
    }
}
