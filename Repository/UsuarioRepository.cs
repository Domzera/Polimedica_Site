using Microsoft.EntityFrameworkCore;
using Polimedica.Data;
using Polimedica.Interface;
using Polimedica.Models;

namespace Polimedica.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly PolimedicaDbContext _context;

        public UsuarioRepository(PolimedicaDbContext context)
        {
            _context = context;
        }
        public bool Add(Usuario usuario)
        {
            _context.Add(usuario);
            return Save();
        }

        public bool Delete(Usuario usuario)
        {
            _context.Remove(usuario);
            return Save();
        }

        public async Task<Usuario> GetById(int id)
        {
            //return await _context.UsuarioDb.FirstOrDefaultAsync(i => i.Id == id);
            throw new NotImplementedException();
        }

        public async Task<Usuario> GetByName(string name)
        {
            //ARRUMAR PARA PROCURAR PELO NOME
            //return await _context.UsuarioDb.FirstOrDefaultAsync(n => n.SobreNome == name);
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Usuario usuario)
        {
            _context.Update(usuario);
            return Save();
        }
    }
}
