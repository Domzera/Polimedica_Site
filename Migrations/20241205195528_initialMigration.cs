using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Polimedica.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCategoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescricaoCategoria = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CuponDescontoDb",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescontoPercentual = table.Column<long>(type: "bigint", nullable: false),
                    DataDaExpiracao = table.Column<DateOnly>(type: "date", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuponDescontoDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnderecoDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeLogradouro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoSigla = table.Column<int>(type: "int", nullable: false),
                    CEP = table.Column<long>(type: "bigint", nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnderecoDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarcaDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeMarca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescricaoMarca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoImagem = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcaDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimeiroNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SobreNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    DataDeCadastro = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_EnderecoDb_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "EnderecoDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescricaoProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<long>(type: "bigint", nullable: false),
                    QuantidadeEmEstoque = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Imagem1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagem2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagem3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagem4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagem5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAdicionado = table.Column<DateOnly>(type: "date", nullable: false),
                    Ativo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoDb_CategoriaDb_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "CategoriaDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoDb_MarcaDb_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "MarcaDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    DataDoPedido = table.Column<DateOnly>(type: "date", nullable: false),
                    StatusDoPedido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorTotal = table.Column<long>(type: "bigint", nullable: false),
                    FormaDePagamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoDb_EnderecoDb_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "EnderecoDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoDb_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AvaliacaoDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    Nota = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAvaliacao = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacaoDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvaliacaoDb_ProdutoDb_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "ProdutoDb",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AvaliacaoDb_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoDeComprasDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    DataDaInclusao = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoDeComprasDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrinhoDeComprasDb_ProdutoDb_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "ProdutoDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrinhoDeComprasDb_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensDoPedidoDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitario = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensDoPedidoDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensDoPedidoDb_PedidoDb_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "PedidoDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItensDoPedidoDb_ProdutoDb_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "ProdutoDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PagamentosDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    DataDoPagamento = table.Column<DateOnly>(type: "date", nullable: false),
                    Valor = table.Column<long>(type: "bigint", nullable: false),
                    MetodoDePagamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDoPagamento = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagamentosDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagamentosDb_PedidoDb_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "PedidoDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoDb_ProdutoId",
                table: "AvaliacaoDb",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoDb_UsuarioId",
                table: "AvaliacaoDb",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoDeComprasDb_ProdutoId",
                table: "CarrinhoDeComprasDb",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoDeComprasDb_UsuarioId",
                table: "CarrinhoDeComprasDb",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensDoPedidoDb_PedidoId",
                table: "ItensDoPedidoDb",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensDoPedidoDb_ProdutoId",
                table: "ItensDoPedidoDb",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_PagamentosDb_PedidoId",
                table: "PagamentosDb",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoDb_EnderecoId",
                table: "PedidoDb",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoDb_UsuarioId",
                table: "PedidoDb",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoDb_CategoriaId",
                table: "ProdutoDb",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoDb_MarcaId",
                table: "ProdutoDb",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EnderecoId",
                table: "Usuario",
                column: "EnderecoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvaliacaoDb");

            migrationBuilder.DropTable(
                name: "CarrinhoDeComprasDb");

            migrationBuilder.DropTable(
                name: "CuponDescontoDb");

            migrationBuilder.DropTable(
                name: "ItensDoPedidoDb");

            migrationBuilder.DropTable(
                name: "PagamentosDb");

            migrationBuilder.DropTable(
                name: "ProdutoDb");

            migrationBuilder.DropTable(
                name: "PedidoDb");

            migrationBuilder.DropTable(
                name: "CategoriaDb");

            migrationBuilder.DropTable(
                name: "MarcaDb");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "EnderecoDb");
        }
    }
}
