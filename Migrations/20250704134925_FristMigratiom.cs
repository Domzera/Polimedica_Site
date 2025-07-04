using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Polimedica.Migrations
{
    /// <inheritdoc />
    public partial class FristMigratiom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AvaliacaoDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nota = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataAvaliacao = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacaoDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoDeComprasDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    DataDaInclusao = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoDeComprasDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCategoria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescricaoCategoria = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaProdutoDb",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CuponDescontoDb",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    NomeLogradouro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoSigla = table.Column<int>(type: "int", nullable: false),
                    CEP = table.Column<long>(type: "bigint", nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    NomeMarca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescricaoMarca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoImagem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcaDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarcaProdutoDb",
                columns: table => new
                {
                    MarcaId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ProdutoDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescricaoProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantidadeEmEstoque = table.Column<int>(type: "int", nullable: false),
                    Imagem1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagem2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagem3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagem4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagem5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataAdicionado = table.Column<DateOnly>(type: "date", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrimeiroNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SobreNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoId = table.Column<int>(type: "int", nullable: true),
                    DataDeCadastro = table.Column<DateOnly>(type: "date", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_EnderecoDb_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "EnderecoDb",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataDoPedido = table.Column<DateOnly>(type: "date", nullable: true),
                    StatusDoPedido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorTotal = table.Column<long>(type: "bigint", nullable: false),
                    FormaDePagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoDb_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PedidoDb_EnderecoDb_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "EnderecoDb",
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
                    MetodoDePagamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusDoPagamento = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EnderecoId",
                table: "AspNetUsers",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AvaliacaoDb");

            migrationBuilder.DropTable(
                name: "CarrinhoDeComprasDb");

            migrationBuilder.DropTable(
                name: "CategoriaDb");

            migrationBuilder.DropTable(
                name: "CategoriaProdutoDb");

            migrationBuilder.DropTable(
                name: "CuponDescontoDb");

            migrationBuilder.DropTable(
                name: "ItensDoPedidoDb");

            migrationBuilder.DropTable(
                name: "MarcaDb");

            migrationBuilder.DropTable(
                name: "MarcaProdutoDb");

            migrationBuilder.DropTable(
                name: "PagamentosDb");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ProdutoDb");

            migrationBuilder.DropTable(
                name: "PedidoDb");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EnderecoDb");
        }
    }
}
