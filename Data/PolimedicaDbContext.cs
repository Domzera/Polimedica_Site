using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Polimedica.Models;

namespace Polimedica.Data
{
    public class PolimedicaDbContext : IdentityDbContext<Usuario>
    {
        public PolimedicaDbContext(DbContextOptions<PolimedicaDbContext> options) : base(options) { }

        public DbSet<Avaliacao> AvaliacaoDb {  get; set; }
        public DbSet<CarrinhoDeCompras> CarrinhoDeComprasDb { get; set; }
        public DbSet<Categoria> CategoriaDb { get; set; }
        public DbSet<CuponDesconto> CuponDescontoDb {  get; set; }
        public DbSet<Endereco> EnderecoDb { get; set; }
        public DbSet<ItensDoPedido> ItensDoPedidoDb {  get; set; }
        public DbSet<Marca>MarcaDb { get; set; }
        public DbSet<Pagamentos> PagamentosDb {  get; set; }
        public DbSet<Pedido> PedidoDb { get; set; }
        public DbSet<Produto> ProdutoDb { get; set; }
    }
}
